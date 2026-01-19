using ClockNest.Common;
using ClockNest.Enum;
using ClockNest.Models.Employee_Model;
using ClockNest.Models.SelfService_Model;
using ClockNest.Models.User_Model;
using ClockNest.Services.CommonService;
using ClockNest.ViewModels.Parameter_List;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Globalization;
using System.Security.Claims;
using System.Text.Json;

namespace ClockNest.Services.SelfService_Service
{
    public class SelfServices
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly UserContext _userContext;
        private readonly AuthenticationStateProvider _auth;
        private readonly IConfiguration _configuration;

        public SelfServices(IHttpClientFactory httpClientFactory, UserContext userContext, AuthenticationStateProvider auth, IConfiguration configuration)
        {
            _httpClientFactory = httpClientFactory;
            _userContext = userContext;
            _auth = auth;
            _configuration = configuration;
        }

        public async Task<List<UserSelfServiceAccess>> GetUserSelfServiceAccessAsync(int userId)
        {
            var client = _httpClientFactory .CreateClient("ClockNestClient").AddDefaultHeader(_userContext);

            var response = await client.PostAsJsonAsync("chronicle/account/userselfserviceaccess/get", userId);

            if (!response.IsSuccessStatusCode)
                throw new Exception($"Error in fetching self service access: {response.StatusCode}");

            var accessList = await response.Content.ReadFromJsonAsync<List<UserSelfServiceAccess>>();

            return accessList ?? new List<UserSelfServiceAccess>();
        }

        public async Task<Employee> GetEmployeeByIdAsync(ParameterList parameterList)
        {
            if (parameterList.CompanyId == 0) throw new ArgumentException("CompanyId can not be 0");

            if (parameterList.Id == 0)
                throw new ArgumentException("EmployeeId can not be 0");

            var client = _httpClientFactory.CreateClient("ClockNestClient").AddDefaultHeader(_userContext);

            var response = await client.PostAsJsonAsync("chronicle/setup/employees/employeebyid/get", parameterList);

            if (!response.IsSuccessStatusCode)
                throw new Exception($"Error fetching employee: {response.StatusCode}");

            var employee = await response.Content.ReadFromJsonAsync<Employee>();

            return employee ?? throw new Exception("Employee not found");
        }

        public async Task<List<Activity>> GetActivitiesAsync(int companyId)
        {
            var client = _httpClientFactory.CreateClient("ClockNestClient").AddDefaultHeader(_userContext);

            var response = await client.PostAsJsonAsync("chronicle/setup/organisation/nonarchivedactivities/get", companyId);
            if (response.IsSuccessStatusCode)
            {
                var tags = await response.Content.ReadFromJsonAsync<List<Activity>>();
                return tags ?? new List<Activity>();
            }

            return new List<Activity>();
        }

        public async Task<List<CostCentre>> GetCostCentresAsync(int companyId)
        {
            var client = _httpClientFactory.CreateClient("ClockNestClient").AddDefaultHeader(_userContext);

            var response = await client.PostAsJsonAsync("chronicle/setup/organisation/nonarchivedcostcentres/get", companyId);
            if (response.IsSuccessStatusCode)
            {
                var tags = await response.Content.ReadFromJsonAsync<List<CostCentre>>();
                return tags ?? new List<CostCentre>();
            }

            return new List<CostCentre>();
        }

        //Get employee workflowprogress
        public async Task<EmployeeWorkflowProgress?> GetEmployeeWorkflowProgress(int employeeId)
        {
            var client = _httpClientFactory.CreateClient("ClockNestClient").AddDefaultHeader(_userContext);

            var response = await client.PostAsJsonAsync("chronicle/home/employeeworkflowprogress/get", employeeId);

            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadFromJsonAsync<EmployeeWorkflowProgress>();
            }

            return null;
        }

        //Weekly summary
        public async Task<List<WeeklySummary>> GetSelfServiceEmployeeWeeklySummary(int employeeId, int companyId, DateTime? date, ClaimsPrincipal user)
        {
            var result = await GetEmployeeWeeklySummaryAsync(employeeId, companyId, date, user);
            return result;
        }

        public async Task<List<WeeklySummary>> GetEmployeeWeeklySummaryAsync(int employeeId, int companyId, DateTime? date, ClaimsPrincipal user)
        {
            if (companyId == 0)
                throw new ArgumentException("CompanyId cannot be 0");
            if (employeeId == 0)
                throw new ArgumentException("EmployeeId cannot be 0");

            var disableVerifyCheckbox = true;

            if (user.IsInRole("LiveReadOnly") && !user.IsInRole("SuperUser"))
            {
                disableVerifyCheckbox = true;
            }

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
            Thread.CurrentThread.CurrentCulture = CurrentCulture;
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

                var employeeWeeklySummary = await response.Content.ReadFromJsonAsync<List<WeeklySummary>>(options);

                int contractedMins = 0;
                int totalMins = 0;
                int overtimeMins = 0;
                foreach (WeeklySummary daySummary in employeeWeeklySummary)
                {
                    if (daySummary.ContractedMins != null) contractedMins += (int)daySummary.ContractedMins;

                    if (daySummary.TotalMins != null) totalMins += (int)daySummary.TotalMins;

                    if (daySummary.OvertimeMins != null) overtimeMins += (int)daySummary.OvertimeMins;

                    daySummary.VerifyDisabled = disableVerifyCheckbox;
                }

                WeeklySummary weeklySummary = new WeeklySummary();

                if (companyId != 610)
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

        //chart data
        public async Task<List<DashboardChartData>> GetSelfServiceEntitlementAsync(int employeeId, int companyId)
        {
            var client = _httpClientFactory.CreateClient("ClockNestClient").AddDefaultHeader(_userContext);

            bool entitlementsInDecimal = false;
            bool entitlementInHours = false;

            var responseCompany = await client.PostAsJsonAsync("chronicle/generic/company/get", companyId);
            if (responseCompany.IsSuccessStatusCode)
            {
                var company = await responseCompany.Content.ReadFromJsonAsync<Company>();
                entitlementsInDecimal = company != null && !company.EntitlementsInHrsMins;
            }

            var filterDetails = new ParameterList { EmployeeId = employeeId, Id = employeeId, CompanyId = companyId };
            var responseEmployee = await client.PostAsJsonAsync("chronicle/setup/employees/employee/get", filterDetails);
            if (responseEmployee.IsSuccessStatusCode)
            {
                var employee = await responseEmployee.Content.ReadFromJsonAsync<Employee>();
                entitlementInHours = employee?.EntitlementInHours ?? false;
            }

            var response = await client.PostAsJsonAsync("chronicle/timeattendance/selfserviceentitlement/get", filterDetails);
            if (!response.IsSuccessStatusCode)
                return new List<DashboardChartData>();

            var selfServiceEntitlement = await response.Content.ReadFromJsonAsync<List<DashboardChartData>>();
            if (selfServiceEntitlement == null || !selfServiceEntitlement.Any())
                return new List<DashboardChartData>();

            if (entitlementInHours && !entitlementsInDecimal)
            {
                for (int i = 0; i < selfServiceEntitlement.Count; i++)
                {
                    var booked = new ConvertHoursDecimalToHoursMinutes(selfServiceEntitlement[i].Datasets[0].Data[0]);
                    var remaining = new ConvertHoursDecimalToHoursMinutes(selfServiceEntitlement[i].Datasets[0].Data[1]);

                    selfServiceEntitlement[i].Labels.Add($"Booked({booked.Hours}:{booked.Minutes:00})");
                    selfServiceEntitlement[i].Labels.Add($"Remaining({remaining.Hours}:{remaining.Minutes:00})");
                }
            }
            else
            {
                for (int i = 0; i < selfServiceEntitlement.Count; i++)
                {
                    selfServiceEntitlement[i].Labels.Add($"Booked({selfServiceEntitlement[i].Datasets[0].Data[0]})");
                    selfServiceEntitlement[i].Labels.Add($"Remaining({selfServiceEntitlement[i].Datasets[0].Data[1]})");
                }
            }

            return selfServiceEntitlement;
        }

        //Submit clocking
        public async Task<bool> SubmitClocking(int employeeId, string clockingType, int changeId)
        {
            ClockingInfo clockingInfo = new ClockingInfo();
            clockingInfo.ClockingDevice = enumClockingDevice.SelfService;
            clockingInfo.EmployeeId = employeeId;
            switch (clockingType)
            {
                case "clockIn":
                    clockingInfo.ClockingType = enumClockingType.ClockIn;
                    break;
                case "clockOut":
                    clockingInfo.ClockingType = enumClockingType.ClockOut;
                    break;
                case "startBreak":
                    clockingInfo.ClockingType = enumClockingType.BreakStart;
                    break;
                case "endBreak":
                    clockingInfo.ClockingType = enumClockingType.BreakEnd;
                    break;
                case "changeActivity":
                    clockingInfo.ClockingType = enumClockingType.ChangeActivity;
                    break;
                case "changeCostCentre":
                    clockingInfo.ClockingType = enumClockingType.ChangeCostCentre;
                    break;

                case "contactless":
                    clockingInfo.ClockingType = enumClockingType.Contactless;
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

        //Get EmployeeHoliday

        public async Task<List<AbsenteeRecord>> GetEmployeeHolidayAsync(int employeeId, int companyId)
        {
            DateTime startDate = DateTime.Now.Date;
            DateTime endDate = DateTime.Now.Date.AddYears(1);

            var parameterList = new ParameterList
            {
                EmployeeId = employeeId,
                StartDate = startDate,
                EndDate = endDate,
            };

            var client = _httpClientFactory.CreateClient("ClockNestClient").AddDefaultHeader(_userContext);
            var companyResponse = await client.PostAsJsonAsync("chronicle/generic/company/get", companyId);
            if (companyResponse.IsSuccessStatusCode)
            {
                var company = await companyResponse.Content.ReadFromJsonAsync<Company>();
                var startOfYear = company.StartOfYear == null ? "0101" : company.StartOfYear;

                startDate = new DateTime(DateTime.Now.Year, Convert.ToInt32(startOfYear.Substring(0, 2)), Convert.ToInt32(startOfYear.Substring(2, 2)));

                if (startDate >= DateTime.Now)
                {
                    startDate = startDate.AddYears(-1);
                }
                endDate = startDate.AddYears(1).AddDays(-1);

                parameterList.StartDate = startDate;
                parameterList.EndDate = endDate;
            }
            var response = await client.PostAsJsonAsync("chronicle/timeattendance/absenteerecords/get", parameterList);
            if (response.IsSuccessStatusCode)
            {
                var absenteeRecords = await response.Content.ReadFromJsonAsync<List<AbsenteeRecord>>();
                return absenteeRecords?
                    .Where(x => x.AbsenceTypeId == 1 || x.AbsenceTypeId == 3)
                    .OrderBy(x => x.AbsenceDate)
                    .ToList() ?? new List<AbsenteeRecord>();
            }

            return new List<AbsenteeRecord>();
        }

        //Get Employee Announcement
        public async Task<List<EmployeeAnnouncement>> GetEmployeeAnnouncements(int employeeId)
        {
            var client = _httpClientFactory.CreateClient("ClockNestClient").AddDefaultHeader(_userContext);

            var response = await client.PostAsJsonAsync("chronicle/timeattendance/employeeannouncements/get", employeeId);

            if (response.IsSuccessStatusCode)
            {
                var announcement = await response.Content.ReadFromJsonAsync<List<EmployeeAnnouncement>>();
                return announcement?.OrderByDescending(x => x.CreatedDate).ToList() ?? new List<EmployeeAnnouncement>();
            }
            return new List<EmployeeAnnouncement>();
        }


        //get shift notification
        public async Task<List<EmployeeShiftNotification>> GetEmployeeShiftNotificationsAsync(int employeeId)
        {
            var client = _httpClientFactory.CreateClient("ClockNestClient").AddDefaultHeader(_userContext);

            var response = await client.PostAsJsonAsync("chronicle/timeattendance/employeeshiftnotifications/get", employeeId);

            if (response.IsSuccessStatusCode)
            {
                var shiftNotifications = await response.Content.ReadFromJsonAsync<List<EmployeeShiftNotification>>()
                           ?? new List<EmployeeShiftNotification>();

                var employeeshiftNotifications = shiftNotifications.Where(x => x.Status == 0).OrderByDescending(x => x.CreatedDate).ToList();

                return employeeshiftNotifications;
            }

            return new List<EmployeeShiftNotification>();
        }

        // get employee payrollpayment
        public async Task<List<ShapePayment>> GetEmployeePayrollPaymentsAsync(int employeeId, int companyId)
        {
            try
            {
                var client = _httpClientFactory.CreateClient("ClockNestClient").AddDefaultHeader(_userContext);

                var response = await client.PostAsJsonAsync("chronicle/payroll/shapeemployeepayments/get", employeeId);

                if (response.IsSuccessStatusCode)
                {
                    var employeeShapePayments = await response.Content.ReadFromJsonAsync<List<ShapePayment>>() ?? new List<ShapePayment>();
                    if (companyId == 795)
                    {
                        foreach (var employeeShapePayment in employeeShapePayments)
                        {
                            employeeShapePayment.Amount = 0;
                        }
                    }
                    return employeeShapePayments;
                }
                return new List<ShapePayment>();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
                return new List<ShapePayment>();
            }
        }

        // get employee documents
        public Task<List<SelfServiceEmployeePayrollDocuments>> GetEmployeePayrollDocumentsAsync(int companyId)
        {
            var currentYear = DateTime.Now.Year;
            if (DateTime.Now > new DateTime(currentYear, 4, 1))
            {
                currentYear--;
            }

            var dataList = new List<SelfServiceEmployeePayrollDocuments>
        {
            new SelfServiceEmployeePayrollDocuments { DocumentDisplay = "P60", MaxYear = currentYear, DocumentType = "P60" },
            new SelfServiceEmployeePayrollDocuments { DocumentDisplay = "PBIK", MaxYear = currentYear, DocumentType = "PBIK" }
        };

            return Task.FromResult(dataList);
        }

        //get payroll p60
        public async Task<bool?> CheckPayrollP60Async(int companyId, int employeeId, int payrollDocumentYear)
        {
            var parameterList = new ParameterList
            {
                CompanyId = companyId,
                EmployeeId = employeeId,
                Date = new DateTime(payrollDocumentYear, 1, 1)
            };
            var client = _httpClientFactory.CreateClient("ClockNestClient").AddDefaultHeader(_userContext);

            var response = await client.PostAsJsonAsync("chronicle/payroll/shapep60check/get", parameterList);

            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadFromJsonAsync<bool>();
                return result;
            }

            return null;
        }

        //get payroll p60
        public async Task<byte[]> GetPayrollP60(int companyId, int employeeId, int payrollDocumentYear)
        {
            var parameterList = new ParameterList
            {
                CompanyId = companyId,
                EmployeeId = employeeId,
                Date = new DateTime(payrollDocumentYear, 1, 1)
            };
            var client = _httpClientFactory.CreateClient("ClockNestClient").AddDefaultHeader(_userContext);

            var response = await client.PostAsJsonAsync("chronicle/payroll/shapep60/get", parameterList);
            if (response.IsSuccessStatusCode)
            {
                var bytes = await response.Content.ReadFromJsonAsync<byte[]>();
                return bytes;
            }
            return null;
        }

        //get payroll pbik
        public async Task<bool?> CheckPayrollPBIKAsync(int companyId, int employeeId, int payrollDocumentYear)
        {
            var parameterList = new ParameterList
            {
                CompanyId = companyId,
                EmployeeId = employeeId,
                Date = new DateTime(payrollDocumentYear, 1, 1)
            };
            var client = _httpClientFactory.CreateClient("ClockNestClient").AddDefaultHeader(_userContext);
            var response = await client.PostAsJsonAsync("chronicle/payroll/shapepbikcheck/get", parameterList);
            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadFromJsonAsync<bool>();
                return result;
            }
            return null;
        }

        public async Task<byte[]> GetPayrollPBIK(int companyId, int employeeId, int payrollDocumentYear)
        {
            var parameterList = new ParameterList
            {
                CompanyId = companyId,
                EmployeeId = employeeId,
                Date = new DateTime(payrollDocumentYear, 1, 1)
            };
            var client = _httpClientFactory.CreateClient("ClockNestClient").AddDefaultHeader(_userContext);

            var response = await client.PostAsJsonAsync("chronicle/payroll/shapepbik/get", parameterList);
            if (response.IsSuccessStatusCode)
            {
                var bytes = await response.Content.ReadFromJsonAsync<byte[]>();
                return bytes;
            }
            return null;
        }
    }
}
