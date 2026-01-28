
using ClockNest.Models.Employee_Model;
using ClockNest.Models.Tag_Modal;
using ClockNest.Models.User_Model;
using ClockNest.Models.WorkRecordNotes_Model;
using ClockNest.Services.CommonService;
using ClockNest.ViewModels.Parameter_List;
using Microsoft.AspNetCore.Components.Authorization;
using System.Net.Http;


namespace ClockNest.Services.WorkRecordNotes_Service
{
    public class WorkRecordNotesService
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly UserContext _userContext;
        private readonly AuthenticationStateProvider _auth;

        public WorkRecordNotesService(IHttpClientFactory httpClientFactory, UserContext userContext, AuthenticationStateProvider auth)
        {
            _httpClientFactory = httpClientFactory;
            _userContext = userContext;
            _auth = auth;
        }

        public async Task<List<WorkRecordNoteDetail>> GetWorkRecordNotesAsync(ParameterList parameterList)
        {
            var client = _httpClientFactory.CreateClient("ClockNestClient").AddDefaultHeader(_userContext);

            var response = await client.PostAsJsonAsync("chronicle/timeattendance/workrecordnotedetailsWithPhoto/get", parameterList).ConfigureAwait(false);

            if (response.IsSuccessStatusCode)
            {
                var data = await response.Content.ReadFromJsonAsync<List<WorkRecordNoteDetail>>().ConfigureAwait(false);
                if (data == null)
                    throw new Exception("Work record note details not found.");
                return data;
            }
            return new List<WorkRecordNoteDetail>();

        }

        //Get company
        public async Task<Company?> GetCompanyAsync(int companyId)
        {
            var client = _httpClientFactory.CreateClient("ClockNestClient").AddDefaultHeader(_userContext);
            var response = await client.PostAsJsonAsync("chronicle/account/company/get", companyId).ConfigureAwait(false);

            if (response.IsSuccessStatusCode)
            {
                var data = await response.Content.ReadFromJsonAsync<Company>().ConfigureAwait(false);
                return data;
            }
            return null;
        }

        //get employee tag2
        public async Task<List<Employee>> GetEmployeeByTag2Async(ParameterList parameterList)
        {

            var client = _httpClientFactory.CreateClient("ClockNestClient").AddDefaultHeader(_userContext);
            var response = await client.PostAsJsonAsync("chronicle/setup/employees/employeesbytag2/get", parameterList);

            if (response.IsSuccessStatusCode)
            {
                var employees = await response.Content.ReadFromJsonAsync<List<Employee>>() ?? new();
                employees = employees.Where(e => e.PersonTypeId != 3).ToList();

                var responseA = await client.PostAsJsonAsync("chronicle/timeattendance/absenceperiods/get", parameterList.CompanyId);

                if (responseA.IsSuccessStatusCode)
                {
                    var absencePeriods = await responseA.Content.ReadFromJsonAsync<List<AbsencePeriod>>();
                    foreach (Employee employee in employees)
                    {
                        var ap = absencePeriods.Find(a => a.EmployeeId == employee.Id);
                        if (ap != null)
                        {
                            employee.HolidayEntitlement1 = ap.HolidayEntitlement1;
                            employee.HolidayCarried1 = ap.HolidayCarried1;
                        }
                    }
                }
                return employees;
            }
            return new List<Employee>();
        }

        //Get work record all details
        public async Task<WorkRecordAllDetails> GetWorkRecordAllDetailsAsync(int employeeId, int? employeeShiftId, DateTime date)
        {
            var client = _httpClientFactory.CreateClient("ClockNestClient").AddDefaultHeader(_userContext);

            var parameterList = new ParameterList
            {
                EmployeeId = employeeId,
                EmployeeShiftId = employeeShiftId.GetValueOrDefault(0),
                Date = date
            };
            var response = await client.PostAsJsonAsync("chronicle/timeattendance/workrecordalldetails/get", parameterList).ConfigureAwait(false);
            if (response.IsSuccessStatusCode)
            {
                var data = await response.Content.ReadFromJsonAsync<WorkRecordAllDetails>().ConfigureAwait(false);
                if (data == null)
                    throw new Exception("Work record all details not found.");
                return data;
            }
            return null;
        }

        //hide cost
        public async Task<bool> HideCosts(int userId)
        {
            var client = _httpClientFactory.CreateClient("ClockNestClient").AddDefaultHeader(_userContext);

            var response = await client.PostAsJsonAsync("chronicle/setup/organisation/user/get", userId).ConfigureAwait(false);

            if (response.IsSuccessStatusCode)
            {
                var savedUser = await response.Content.ReadFromJsonAsync<User>().ConfigureAwait(false);

                if (savedUser?.UserValueAccess.Where(x => x.ValueAccessTypeId == 1).Count() > 0)
                {
                    return true;
                }
            }

            return false; 
        }

        //Get Access Movement
        public async Task<List<EmployeeAccessControlSwipes>> GetEmployeeAccessControlSwipesAsync(ParameterList parameterList)
        {
            var client = _httpClientFactory.CreateClient("ClockNestClient").AddDefaultHeader(_userContext);

            var response = await client.PostAsJsonAsync("chronicle/accesscontrol/employeeaccesscontrolswipes/get", parameterList).ConfigureAwait(false);
            if (response.IsSuccessStatusCode)
            {
                var data = await response.Content.ReadFromJsonAsync<List<EmployeeAccessControlSwipes>>().ConfigureAwait(false);
                if (data == null)
                    throw new Exception("Employee access control swipes not found.");
                return data;
            }
            return new List<EmployeeAccessControlSwipes>();
        }

        public async Task<List<Activity>> GetActivitiesAsync(int companyId)
        {
            var client = _httpClientFactory.CreateClient("ClockNestClient").AddDefaultHeader(_userContext);

            var response = await client.PostAsJsonAsync("chronicle/setup/organisation/nonarchivedactivitiesandbreaks/get", companyId).ConfigureAwait(false);

            if (response.IsSuccessStatusCode)
            {
                return await response.Content
                    .ReadFromJsonAsync<List<Activity>>()
                    .ConfigureAwait(false) ?? new List<Activity>();
            }

            return new List<Activity>();
        }

        public async Task<List<CostCentre>> GetCostCentresAsync(int companyId)
        {
            var client = _httpClientFactory.CreateClient("ClockNestClient").AddDefaultHeader(_userContext);

            var response = await client.PostAsJsonAsync("chronicle/setup/organisation/nonarchivedcostcentres/get", companyId).ConfigureAwait(false);

            if (response.IsSuccessStatusCode)
            {
                return await response.Content
                    .ReadFromJsonAsync<List<CostCentre>>()
                    .ConfigureAwait(false) ?? new List<CostCentre>();
            }

            return new List<CostCentre>();
        }

        //work record active
        public async Task<bool> IsWorkRecordActive(int workRecordId)
        {
            var client = _httpClientFactory.CreateClient("ClockNestClient").AddDefaultHeader(_userContext);
            var response = await client.PostAsJsonAsync("chronicle/timeattendance/isworkrecordactive/get", workRecordId);

            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadFromJsonAsync<bool>();
                return result;
            }

            return false;
        }

        //save ork record
        public async Task<List<WorkRecord>?> SaveWorkRecordAsync(WorkRecord workRecord)
        {
            if (workRecord == null) throw new ArgumentNullException(nameof(workRecord), "WorkRecord cannot be null");

            var client = _httpClientFactory.CreateClient("ClockNestClient").AddDefaultHeader(_userContext);
            var response = await client.PostAsJsonAsync("chronicle/timeattendance/workrecord/post", workRecord);

            if (response.IsSuccessStatusCode)
            {
                var savedWorkRecords = await response.Content.ReadFromJsonAsync<List<WorkRecord>>();
                return savedWorkRecords;
            }

            var errorMessage = await response.Content.ReadAsStringAsync();          
            return null;
        }

        //Recalculate Overtime
        public async Task<List<Overtime>?> RecalculateOvertimeAsync(ParameterList parameterList)
        {
            var client = _httpClientFactory.CreateClient("ClockNestClient").AddDefaultHeader(_userContext);
            var refreshResponse = await client.PostAsJsonAsync("chronicle/timeattendance/refreshovertime/post", parameterList);
            if (!refreshResponse.IsSuccessStatusCode)
            {
                var error = await refreshResponse.Content.ReadAsStringAsync();
                return null;
            }

            var overtimeResponse = await client.PostAsJsonAsync("chronicle/timeattendance/overtime/get", parameterList);
            if (!overtimeResponse.IsSuccessStatusCode)
            {
                var error = await overtimeResponse.Content.ReadAsStringAsync();
                return null;
            }

            var overtimeList = await overtimeResponse.Content.ReadFromJsonAsync<List<Overtime>>();
            return overtimeList;
        }

        //delete work record
        public async Task<bool> DeleteWorkRecordAsync(List<WorkRecord> selectedWorkRecord)
        {
            if (selectedWorkRecord == null || selectedWorkRecord.Count == 0)
                throw new ArgumentException("workRecord cannot be empty");

            var client = _httpClientFactory.CreateClient("ClockNestClient").AddDefaultHeader(_userContext);
            var response = await client.PostAsJsonAsync("chronicle/timeattendance/workrecordsdelete/post", selectedWorkRecord);

            if (response.IsSuccessStatusCode)
                return true;

            var errorContent = await response.Content.ReadAsStringAsync();
            throw new Exception($"Failed to delete work record: {response.StatusCode} - {errorContent}");
        }

        //Create shift
        public async Task<List<Shift>> GetWorkRecordShiftAsync(int companyId)
        {
            try
            {
                var client = _httpClientFactory.CreateClient("ClockNestClient").AddDefaultHeader(_userContext);
                var response = await client.PostAsJsonAsync("chronicle/setup/shiftpatterns/nonarchivedshifts/get", companyId);

                if (!response.IsSuccessStatusCode)
                {
                    return new List<Shift>();
                }

                var result = await response.Content.ReadFromJsonAsync<List<Shift>>();
                return result ?? new List<Shift>();
            }
            catch
            {
                return new();
            }
        }

        //get roster shift
        public async Task<List<RosterScheduledShift>> GetRosterScheduledShiftsAsync(ParameterList parameterList)
        {
            var client = _httpClientFactory.CreateClient("ClockNestClient").AddDefaultHeader(_userContext);
            var response = await client.PostAsJsonAsync("chronicle/timeattendance/rosterscheduledshifts/get", parameterList);

            if (!response.IsSuccessStatusCode)
            {
                var error = await response.Content.ReadAsStringAsync();
                throw new HttpRequestException($"Failed to get roster scheduled shifts: {response.StatusCode} - {error}");
            }

            var shifts = await response.Content.ReadFromJsonAsync<List<RosterScheduledShift>>();
            return shifts ?? new List<RosterScheduledShift>();
        }

        //create default day
        public async Task<bool> CreateDefaultDayAsync(ParameterList parameters)
        {
            var client = _httpClientFactory.CreateClient("ClockNestClient").AddDefaultHeader(_userContext);
            var response = await client.PostAsJsonAsync("chronicle/timeattendance/createdefaultday/post", parameters);
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<bool>();
        }

        public async Task<EmployeeShift> SetEmployeeShiftAsync(EmployeeShift employeeShift, int userId)
        {
            var client = _httpClientFactory.CreateClient("ChronicleClient").AddDefaultHeader(_userContext);
            var parameterList = new ParameterList
            {
                EmployeeShift = employeeShift,
                UserId = userId
            };

            var response = await client.PostAsJsonAsync("chronicle/timeattendance/employeeshift/post", parameterList);

            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadFromJsonAsync<EmployeeShift>();
            }
            else
            {
                throw new HttpRequestException($"Error posting employee shift: {response.ReasonPhrase}");
            }
        }

        //delete shift
        public async Task<bool> DeleteEmployeeShiftAsync(List<EmployeeShift> selectedShift, int userId)
        {
            if (selectedShift == null || selectedShift.Count == 0)
                throw new ArgumentException("workRecord cannot be empty");

            var client = _httpClientFactory.CreateClient("ClockNestClient").AddDefaultHeader(_userContext);

            var parameterList = new ParameterList
            {
                EmployeeShifts = selectedShift,
                UserId = userId
            };
            var response = await client.PostAsJsonAsync("chronicle/timeattendance/employeeshiftsdelete/post", parameterList);

            if (response.IsSuccessStatusCode)
                return true;

            var errorContent = await response.Content.ReadAsStringAsync();
            throw new Exception($"Failed to delete shift: {response.StatusCode} - {errorContent}");
        }
    }
}
