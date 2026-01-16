using ClockNest.Models.Employee_Model;
using ClockNest.Models.User_Model;
using ClockNest.Services.CommonService;
using ClockNest.ViewModels.Parameter_List;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.Net;

namespace ClockNest.Services.User_Service
{
    public class UserService
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly UserContext _userContext;
        private readonly AuthenticationStateProvider _auth;
        private readonly IConfiguration _configuration;

        public UserService(IHttpClientFactory httpClientFactory, UserContext userContext, AuthenticationStateProvider auth, IConfiguration configuration)
        {
            _httpClientFactory = httpClientFactory;
            _userContext = userContext;
            _auth = auth;
            _configuration = configuration;
        }

        public async Task<List<User>> GetUserAsync(int companyId)
        {
            var client = _httpClientFactory.CreateClient("ClockNestClient").AddDefaultHeader(_userContext);

            var response = await client.PostAsJsonAsync("chronicle/account/users/get", companyId);

            if (!response.IsSuccessStatusCode)
                throw new Exception($"Error in fetching user: {response.StatusCode}");

            var employees = await response.Content.ReadFromJsonAsync<List<User>>();

            return employees ?? new List<User>();
        }

        //Get user bu user Id
        public async Task<User> GetuserByIdAsync(int userId)
        {
            var client = _httpClientFactory.CreateClient("ClockNestClient").AddDefaultHeader(_userContext);

            var response = await client.PostAsJsonAsync("chronicle/setup/organisation/user/get", userId);

            if (!response.IsSuccessStatusCode)
                throw new Exception($"Error in fetching user: {response.StatusCode}");

            var user = await response.Content.ReadFromJsonAsync<User>();
            return user ?? new User();

        }

        //Get Company Licence Details
        public async Task<CompanyLicenceDetails> GetCompanyLicenceDetailsAsync(int companyId)
        {
            var client = _httpClientFactory.CreateClient("ClockNestClient").AddDefaultHeader(_userContext);
            var response = await client.PostAsJsonAsync("chronicle/account/companylicencedetails/get", companyId);
            if (!response.IsSuccessStatusCode)
                throw new Exception($"Error in fetching company licence details: {response.StatusCode}");
            var licenceDetails = await response.Content.ReadFromJsonAsync<CompanyLicenceDetails>();
            return licenceDetails ?? new CompanyLicenceDetails();
        }

        //Get employees
        public async Task<List<Employee>> GetEmployeeAsync(int companyId)
        {
            var client = _httpClientFactory.CreateClient("ClockNestClient").AddDefaultHeader(_userContext);
       
            var response = await client.PostAsJsonAsync("chronicle/setup/employees/employees/get", companyId);

            if (!response.IsSuccessStatusCode)
                throw new Exception($"Error fetching employee: {response.StatusCode}");

            var employee = await response.Content.ReadFromJsonAsync<List<Employee>>();
            return employee ?? new  List<Employee>();
        }

        //Employee All Detail
        public async Task<EmployeeAllDetails> GetEmployeeAllDetailsAsync(int employeeId, int companyId)
        {
            var client = _httpClientFactory.CreateClient("ClockNestClient").AddDefaultHeader(_userContext);
            var parameterList = new ParameterList
            {
                Id = employeeId,
                CompanyId = companyId
            };

            var response = await client.PostAsJsonAsync("chronicle/setup/employees/employeealldetails/get", parameterList);

            if (response.IsSuccessStatusCode)
            {
                var data = await response.Content.ReadFromJsonAsync<EmployeeAllDetails>();

                if (data == null) throw new Exception("Employee details not found.");
                return data;
            }
            else
            {
                var errorContent = await response.Content.ReadAsStringAsync();
                throw new Exception($"Failed to fetch employee details: {response.StatusCode} - {errorContent}");
            }
        }

        //get all employee
        public async Task<List<Employee>> GetAllEmployeesAsync(int companyId)
        {
            var client = _httpClientFactory.CreateClient("ClockNestClient").AddDefaultHeader(_userContext);

            var response = await client.PostAsJsonAsync("chronicle/setup/employees/employees/get", companyId);

            if (!response.IsSuccessStatusCode)
                throw new Exception($"Error fetching employees: {response.StatusCode}");

            var employees = await response.Content.ReadFromJsonAsync<List<Employee>>();

            return employees ?? new List<Employee>();
        }

        //get acess
        public async Task<AccessDetails> GetAccessDetailsAsync(int userId, int companyId)
        {
            var client = _httpClientFactory.CreateClient("ClockNestClient").AddDefaultHeader(_userContext);

            var parameterList = new ParameterList
            {
                UserId = userId,
                CompanyId = companyId
            };

            var response = await client.PostAsJsonAsync("chronicle/account/accessdetails/get", parameterList).ConfigureAwait(false);

            if (response.IsSuccessStatusCode)
            {
                var data = await response.Content.ReadFromJsonAsync<AccessDetails>().ConfigureAwait(false);

                if (data == null)
                    throw new Exception("Access details not found.");

                return data;
            }
            else
            {
                var errorContent = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
                throw new Exception($"Failed to fetch access details: {response.StatusCode} - {errorContent}");
            }
        }

        //Save user
        public async Task<User> SaveUserAsync(User user)
        {
                       var client = _httpClientFactory.CreateClient("ClockNestClient").AddDefaultHeader(_userContext);
            var response = await client.PostAsJsonAsync("chronicle/account/user/post", user);
            //if (!response.IsSuccessStatusCode)
            //    throw new Exception($"Error saving user: {response.StatusCode}");

            if (!response.IsSuccessStatusCode)
            {
                var errorContent = await response.Content.ReadAsStringAsync();

                Console.WriteLine("❌ Error saving user");
                Console.WriteLine($"Status Code: {(int)response.StatusCode} ({response.StatusCode})");
                Console.WriteLine($"Response Body: {errorContent}");

                throw new Exception(
                    $"Error saving user: {response.StatusCode} - {errorContent}");
            }

            //return true;
            var savedUser = await response.Content.ReadFromJsonAsync<User>();
            return savedUser;
        }


        //Save access
        public async Task<bool> SaveAccessDetailAsync(AccessDetails accessDetails)
        {
            var client = _httpClientFactory.CreateClient("ClockNestClient").AddDefaultHeader(_userContext);
            var response = await client.PostAsJsonAsync("chronicle/account/accessdetails/post", accessDetails);
            if (!response.IsSuccessStatusCode)
                throw new Exception($"Error saving access details: {response.StatusCode}");
            return true;
        }

        //Get user by user salt
        public async Task<string?> GetUserSaltByUserIdAsync(int userId)
        {
            try
            {
                var client = _httpClientFactory.CreateClient("ClockNestClient").AddDefaultHeader(_userContext);
                var response = await client.PostAsJsonAsync("chronicle/account/usersaltbyuserid/get", userId);

                if (response.IsSuccessStatusCode)
                {
                    var salt = await response.Content.ReadAsStringAsync();
                    return salt.Trim('"');
                }

                if (response.StatusCode == HttpStatusCode.NotFound)
                {
                    return null;
                }

                var error = await response.Content.ReadAsStringAsync();
                throw new Exception($"Failed to get user salt: {response.StatusCode} - {error}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error fetching salt: {ex.Message}");
                throw;
            }
        }

    }
}
