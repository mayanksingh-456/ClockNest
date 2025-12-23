using ClockNest.Models.Employee_Model;
using ClockNest.Models.User_Model;
using ClockNest.Services.CommonService;
using ClockNest.ViewModels.Parameter_List;
using Microsoft.AspNetCore.Components.Authorization;
using System.ComponentModel.Design;
using System.Net.Http;

namespace ClockNest.Services.Employee_Service
{
    public class EmployeeService
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly UserContext _userContext;
        private readonly AuthenticationStateProvider _auth;

        public EmployeeService(IHttpClientFactory httpClientFactory, UserContext userContext, AuthenticationStateProvider auth)
        {
            _httpClientFactory = httpClientFactory;
            _userContext = userContext;
            _auth = auth;
        }

        public async Task <List<Employee>> GetEmployeeAsync(int companyId)
        {
            var client = _httpClientFactory.CreateClient("ClockNestClient").AddDefaultHeader(_userContext);

            var response = await client.PostAsJsonAsync("chronicle/setup/employees/employees/get", companyId);

            if (!response.IsSuccessStatusCode)
                throw new Exception($"Error in fetching employee: {response.StatusCode}");

            var employees = await response.Content.ReadFromJsonAsync<List<Employee>>();

            return employees ?? new List<Employee>();
        }

        public async Task<Company> GetCompanyAsync(int companyId)
        {
            var client = _httpClientFactory.CreateClient("ClockNestClient").AddDefaultHeader(_userContext);
            var response = await client.PostAsJsonAsync("chronicle/generic/company/get", companyId);
            if (!response.IsSuccessStatusCode)
                throw new Exception($"Error in fetching company: {response.StatusCode}");

            var company = await response.Content.ReadFromJsonAsync<Company>();

            if (company == null)
                throw new Exception("Company not found.");

            return company;
        }

        public async Task<CompanyLicenceDetails> GetCompanyLicenceDetailsAsync(int companyId)
        {
            var client = _httpClientFactory.CreateClient("ClockNestClient").AddDefaultHeader(_userContext);

            var response = await client.PostAsJsonAsync("chronicle/account/companylicencedetails/get", companyId);

            if (!response.IsSuccessStatusCode)
                throw new Exception($"Error fetching company licence details: {response.StatusCode}");

            var companyLicenceDetails = await response.Content.ReadFromJsonAsync<CompanyLicenceDetails>();

            if (companyLicenceDetails == null)
                throw new Exception("Company licence details not found.");

            return companyLicenceDetails;
        }

        public async Task<EmployeeAllDetails> GetEmployeeAllDetailsAsync(ParameterList parameterList)
        {
            var client = _httpClientFactory.CreateClient("ClockNestClient").AddDefaultHeader(_userContext);

            var response = await client.PostAsJsonAsync("chronicle/setup/employees/employeealldetails/get", parameterList);

            if (!response.IsSuccessStatusCode)
                throw new Exception($"Error fetching employee details: {response.StatusCode}");

            var details = await response.Content.ReadFromJsonAsync<EmployeeAllDetails>();

            if (details == null)
                throw new Exception("Employee details not found.");

            return details;
        }

    }
}
