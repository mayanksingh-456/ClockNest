using ClockNest.Helpers;
using ClockNest.Models.Login;
using ClockNest.Models.User;
using ClockNest.ViewModels;
using Microsoft.JSInterop;
using System.Text.Json;

namespace ClockNest.Services.Auth
{
    public class AuthService
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly Security _Security;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IJSRuntime _js;

        public AuthService(IHttpClientFactory httpClientFactory, Security Security, IHttpContextAccessor httpContextAccessor, IJSRuntime js)
        {
            _httpClientFactory = httpClientFactory;
            _Security = Security;
            _httpContextAccessor = httpContextAccessor;
            _js = js;
        }

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
