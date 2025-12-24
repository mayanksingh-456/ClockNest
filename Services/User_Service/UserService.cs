using ClockNest.Models.Employee_Model;
using ClockNest.Models.User;
using ClockNest.Models.User_Model;
using ClockNest.Services.CommonService;
using Microsoft.AspNetCore.Components.Authorization;

namespace ClockNest.Services.User_Service
{
    public class UserService
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly UserContext _userContext;
        private readonly AuthenticationStateProvider _auth;

        public UserService(IHttpClientFactory httpClientFactory, UserContext userContext, AuthenticationStateProvider auth)
        {
            _httpClientFactory = httpClientFactory;
            _userContext = userContext;
            _auth = auth;
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
    }
}
