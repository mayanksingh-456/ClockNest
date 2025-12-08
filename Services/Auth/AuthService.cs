using ClockNest.Helpers;
using ClockNest.Models.Login;
using ClockNest.Models.User;
using ClockNest.ViewModels;
using System.Text.Json;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;
using static System.Net.WebRequestMethods;

namespace ClockNest.Services.Auth
{
    public class AuthService
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly Security _Security;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public AuthService(IHttpClientFactory httpClientFactory, Security Security, IHttpContextAccessor httpContextAccessor)
        {
            _httpClientFactory = httpClientFactory;
            _Security = Security;
            _httpContextAccessor = httpContextAccessor;
        }
        //public async Task<LoginResult> LoginAsync(string username, string password)
        //{
        //public async Task<LoginResult> LoginAsync(LoginViewModel model, string ReturnUrl)
        //{
        //    var client = _httpClientFactory.CreateClient("ChronicleClient");
        //    try
        //    {
        //        //var saltResponse = await client.PostAsJsonAsync("chronicle/account/usersaltbyusername/get", username);
        //        HttpResponseMessage saltResponse = await client.PostAsJsonAsync("chronicle/account/usersaltbyusername/get", model.Username);


        //        if (!saltResponse.IsSuccessStatusCode)
        //        {
        //            var error = await saltResponse.Content.ReadAsStringAsync();
        //            return new LoginResult { Success = false, ErrorMessage = error };
        //        }

        //        string userSalt = await saltResponse.Content.ReadFromJsonAsync<string>();

        //        string hashedPassword = _Security.HashPassword(model.Password, userSalt);

        //        var userObj = new
        //        {
        //            UserName = model.Username,
        //            Password = hashedPassword
        //        };

        //        var loginResponse = await client.PostAsJsonAsync("chronicle/account/login", userObj);

        //        if (!loginResponse.IsSuccessStatusCode)
        //        {
        //            var error = await loginResponse.Content.ReadAsStringAsync();
        //            return new LoginResult
        //            {
        //                Success = false,
        //                ErrorMessage = string.IsNullOrWhiteSpace(error) ? "Login failed"  : error
        //            };
        //        }

        //        var user = await loginResponse.Content.ReadFromJsonAsync<User>();

        //        return new LoginResult
        //        {
        //            Success = true,
        //            RedirectAction = "/dashboard"
        //        };
        //    }
        //    catch (Exception ex)
        //    {
        //        return new LoginResult
        //        {
        //            Success = false,
        //            ErrorMessage = ex.Message
        //        };
        //    }
        //}


        public async Task<LoginResult> LoginAsync(LoginViewModel model, string ReturnUrl)
        {

            var companyRequiresTwoFactorAuth = false;
            int? companySystemDaysRemaining = null;
            var systemLicenceDueToExpire = false;

            var client = _httpClientFactory.CreateClient("ChronicleClient");

            HttpResponseMessage getUserSaltResponse = await client.PostAsJsonAsync("chronicle/account/usersaltbyusername/get", model.Username);      
            if (!getUserSaltResponse.IsSuccessStatusCode)
            {
                var content = await getUserSaltResponse.Content.ReadAsStringAsync();
                var error = JsonSerializer.Deserialize<string>(content);
                return new LoginResult
                {
                    Success = false,
                    ErrorMessage = string.IsNullOrWhiteSpace(error)
                        ? "An error occurred while retrieving the user salt."
                        : error
                };
            }
            var userSaltJson = await getUserSaltResponse.Content.ReadAsStringAsync();
            var userSalt = JsonSerializer.Deserialize<string>(userSaltJson);
            model.Password = _Security.HashPassword(model.Password, userSalt);

            User user = new User
            {
                UserName = model.Username,
                Password = model.Password

            };
            HttpResponseMessage response = await client.PostAsJsonAsync("chronicle/account/login", user);
            try
            {
                if (_httpContextAccessor.HttpContext != null)
                {
                    _httpContextAccessor.HttpContext.Response.StatusCode = (int)response.StatusCode;
                }
            }
            catch (Exception ex)
            {
                var content = await response.Content.ReadAsStringAsync();
            }

            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                var usersData = JsonSerializer.Deserialize<User>(content, new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                });


                HttpResponseMessage responseCompany = await client.PostAsJsonAsync("chronicle/account/company/get", usersData.CompanyId);
                
                if (companySystemDaysRemaining <= 0 && !user.UserName.Contains("chronicle@"))
                {
                    return new LoginResult
                    {
                        RedirectAction = "SystemLicenceExpired",
                        Success = false,
                        ErrorMessage = "System licence has expired. Please contact support."
                    };
                }
                if (companySystemDaysRemaining > 0 && companySystemDaysRemaining <= 30)
                {
                    systemLicenceDueToExpire = true;

                }
                

                HttpResponseMessage responseUserCompanies = await client.PostAsJsonAsync("chronicle/account/companies/get", usersData.Id);

                var redirectDetails = new Dictionary<string, string>();

                bool isSuperUser = false;
                bool isSelfService = false;
                bool isDashboard = false;
            }
           

            if (response.IsSuccessStatusCode)
            {
                return new LoginResult
                {
                    Success = true,
                    RedirectAction = "/Dashboard"
                };
            }
            else
            {
                var result = await response.Content.ReadAsStringAsync();
                var errorMessage = JsonSerializer.Deserialize<string>(result);
                return new LoginResult
                {
                    Success = response.IsSuccessStatusCode,
                    ErrorMessage = string.IsNullOrWhiteSpace(errorMessage)
                            ? "Authentication failed. No response received from the server"
                            : errorMessage
                };
            }

        }
    }

}
