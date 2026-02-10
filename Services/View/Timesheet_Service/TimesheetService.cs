using ClockNest.Common;
using ClockNest.Enum;
using ClockNest.Helpers;
using ClockNest.Models.Employee_Model;
using ClockNest.Models.SelfService_Model;
using ClockNest.Models.Timesheet_Model;
using ClockNest.Models.User_Model;
using ClockNest.Models.WorkRecordNotes_Model;
using ClockNest.Services.CommonService;
using ClockNest.ViewModels.Parameter_List;
using Microsoft.AspNetCore.Components.Authorization;
using System.Globalization;
using System.Text.Json;

namespace ClockNest.Services.View.Timesheet_Service
{
    public class TimesheetService
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly UserContext _userContext;
        private readonly AuthenticationStateProvider _auth;

        public TimesheetService(IHttpClientFactory httpClientFactory, UserContext userContext, AuthenticationStateProvider auth)
        {
            _httpClientFactory = httpClientFactory;
            _userContext = userContext;
            _auth = auth;
        }


        public async Task<List<EmployeeClockingStatus>> GetEmployeeClockingDetailsAsync(int tagId, DateTime date, EmployeeFilterStatus filterStatus)
        {
            var client = _httpClientFactory.CreateClient("ClockNestClient").AddDefaultHeader(_userContext);
            var parameterList = new
            {
                TagId = tagId,
                Date = date,
                FilterStatus = (int)filterStatus
            };

            var response = await client.PostAsJsonAsync("chronicle/timeattendance/employeeclockingstatus/get", parameterList);

            if (response.IsSuccessStatusCode)
            {
                var data = await response.Content.ReadFromJsonAsync<List<EmployeeClockingStatus>>();
                return data ?? new List<EmployeeClockingStatus>();
            }

            var errorContent = await response.Content.ReadAsStringAsync();
            throw new Exception($"Failed to get employee clocking details: {response.StatusCode} - {errorContent}");
        }

        public async Task<List<WeeklySummary>> GetLiveEmployeeWeeklySummaryAsync(int employeeId, DateTime? date, int companyId)
        {
            var result = await GetEmployeeWeeklySummaryAsync(employeeId, date, companyId);
            return result;
        }

        public async Task<List<WeeklySummary>> GetEmployeeWeeklySummaryAsync(int employeeId, DateTime? date, int companyId)
        {
            if (employeeId <= 0)
            {
                throw new ArgumentException("Employee ID must be greater than zero.", nameof(employeeId));
            }

            var disableVerifyCheckbox = false;
            string dayOfWeek = "Monday";

            var client = _httpClientFactory.CreateClient("ClockNestClient").AddDefaultHeader(_userContext);
            var response = await client.PostAsJsonAsync("chronicle/account/company/get", companyId);
            if (response.IsSuccessStatusCode)
            {
                var company = await response.Content.ReadFromJsonAsync<Company>();
                if (company.StartOfWeek == "Monday" || company.StartOfWeek == "Tuesday" || company.StartOfWeek == "Wednesday" || company.StartOfWeek == "Thursday" || company.StartOfWeek == "Friday" || company.StartOfWeek == "Saturday" || company.StartOfWeek == "Sunday")
                {
                    dayOfWeek = company.StartOfWeek;
                }
            }
            var CurrentCulture = CultureInfo.CurrentCulture;
            Thread.CurrentThread.CurrentCulture = new CultureInfo("en-GB");
            while (date.Value.ToString("dddd") != dayOfWeek)
            {
                date = date.Value.AddDays(-1);
            }

            var filterStartDate = date.Value;
            var filterEndDate = date.Value.AddDays(6);
            var holidayThresholdData = new HolidayThresholdCheckDetails();

            ParameterList filterDetails = new ParameterList { EmployeeId = employeeId, StartDate = filterStartDate, EndDate = filterEndDate };
            response = await client.PostAsJsonAsync("chronicle/timeattendance/weeklysummary/get", filterDetails);
            if (response.IsSuccessStatusCode)
            {
                var options = new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true,
                    Converters = { new BoolNullToFalseConverter() }
                };

                var employeeWeeklySummary = await response.Content
                    .ReadFromJsonAsync<List<WeeklySummary>>(options);

                int contractedMins = 0;
                int totalMins = 0;
                int overtimeMins = 0;
                foreach (WeeklySummary daySummary in employeeWeeklySummary)
                {
                    if (daySummary.ContractedMins != null)
                        contractedMins += (int)daySummary.ContractedMins;

                    if (daySummary.TotalMins != null)
                        totalMins += (int)daySummary.TotalMins;

                    if (daySummary.OvertimeMins != null)
                        overtimeMins += (int)daySummary.OvertimeMins;

                    daySummary.VerifyDisabled = disableVerifyCheckbox;
                }
                WeeklySummary weeklySummary = new WeeklySummary();
                if (companyId != 610) // CY Berry
                {
                    response = await client.PostAsJsonAsync("chronicle/timeattendance/flexitimetotals/get", employeeId);

                    if (response.IsSuccessStatusCode)
                    {
                        var flexitimeTotals = await response.Content.ReadFromJsonAsync<FlexitimeTotals>();

                        weeklySummary.RosterShiftCode = flexitimeTotals.Scheduled;
                        weeklySummary.ScheduledShiftCode = flexitimeTotals.Worked;
                        weeklySummary.ShiftCode = flexitimeTotals.Balance;
                    }

                }
                weeklySummary.ContractedMins = contractedMins;
                weeklySummary.TotalMins = totalMins;
                weeklySummary.OvertimeMins = overtimeMins;

                employeeWeeklySummary.Add(weeklySummary);
                if (companyId != 610)
                {
                    List<AnnualisedHours> annualisedHours = new List<AnnualisedHours>();
                    response = await client.PostAsJsonAsync("chronicle/setup/employees/annualisedhours/get", employeeId);
                    if (response.IsSuccessStatusCode)
                    {
                        annualisedHours = await response.Content.ReadFromJsonAsync<List<AnnualisedHours>>();
                        foreach (var annualisedHour in annualisedHours)
                        {
                            weeklySummary = new WeeklySummary();
                            if (annualisedHour.TargetHours > 0)
                            {
                                weeklySummary.RosterShiftCode = "Pot " + annualisedHour.Pot.ToString();

                                if (annualisedHour.TargetHours > annualisedHour.ActualHours)
                                {
                                    weeklySummary.ShiftCode = "-";
                                }

                                weeklySummary.ShiftCode = weeklySummary.ShiftCode + annualisedHour.Balance.ToString();

                                employeeWeeklySummary.Add(weeklySummary);
                            }
                        }
                    }
                }
                return employeeWeeklySummary;
            }
            return new List<WeeklySummary>();
        }

        //Clockin
        public async Task<bool> SubmitClocking(int employeeId, string clockingType, int changeId)
        {
            ClockingInfo clockingInfo = new ClockingInfo();
            clockingInfo.ClockingDevice = enumClockingDevice.Manual;
            clockingInfo.EmployeeId = employeeId;
            switch (clockingType)
            {
                case "clockIn":
                    clockingInfo.ClockingType = enumClockingType.ClockIn;
                    break;
                case "clockOut":
                    clockingInfo.ClockingType = enumClockingType.ClockOut;
                    break;
                
            }
            clockingInfo.ChangeId = changeId;
            var client = _httpClientFactory.CreateClient("ClockNestClient").AddDefaultHeader(_userContext);
            var response = await client.PostAsJsonAsync("chronicle/timeattendance/clockingbychangeId/post", clockingInfo);

            if (response.IsSuccessStatusCode)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        //public async Task<bool> SetWorkRecordVerifiedAsync(int employeeId, DateTime date, int changeId)
        //{
        //    if (verified == null) throw new ArgumentException("Verified payload cannot be null.", nameof(verified));

        //    var client = _httpClientFactory.CreateClient("ClockNestClient").AddDefaultHeader(_userContext);

        //    var response = await client.PostAsJsonAsync("chronicle/timeattendance/workrecordverified/post", verified);

        //    if (response.IsSuccessStatusCode)
        //    {
        //        return await response.Content.ReadFromJsonAsync<bool>();
        //    }
        //    else
        //    {
        //        var errorContent = await response.Content.ReadAsStringAsync();
        //        throw new HttpRequestException($"Failed to verify work record: {response.StatusCode} - {response.ReasonPhrase}\n{errorContent}");
        //    }
        //}


    }
}
