using Azure;
using ClockNest.Components.Pages.Settings.Employees.Employee_Tab;
using ClockNest.Models.Employee_Model;
using ClockNest.Models.User_Model;
using ClockNest.Models.WorkRecordNotes_Model;
using ClockNest.Services.CommonService;
using ClockNest.ViewModels.Parameter_List;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApplicationModels;
using System.ComponentModel.Design;

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
    }
}
