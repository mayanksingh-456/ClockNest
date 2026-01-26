
using ClockNest.Models.Employee_Model;
using ClockNest.Models.User_Model;
using ClockNest.Models.WorkRecordNotes_Model;
using ClockNest.Services.CommonService;
using ClockNest.ViewModels.Parameter_List;
using Microsoft.AspNetCore.Components.Authorization;


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

            return false; // default
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
    }
}
