using ClockNest.Models.Dashboard_Model;
using ClockNest.Models.User_Model;
using ClockNest.Services.CommonService;
using ClockNest.ViewModels.Parameter_List;
using Microsoft.AspNetCore.Components.Authorization;


namespace ClockNest.Services.Home.Dashboard
{
    public class DashboardService
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly UserContext _userContext;
        private readonly AuthenticationStateProvider _auth;

        public DashboardService(IHttpClientFactory httpClientFactory, UserContext userContext, AuthenticationStateProvider auth)
        {
            _httpClientFactory = httpClientFactory;
            _userContext = userContext;
            _auth = auth;
        }

        public async Task<DashboardTotals?> GetDashboardTotalsAsync(int tagId, DateTime? startDate, DateTime? endDate, int employeeId)
        {
            startDate ??= DateTime.Now.AddMonths(-1);
            endDate ??= DateTime.Now;
            var client = _httpClientFactory.CreateClient("ClockNestClient").AddDefaultHeader(_userContext);
            var filterDetails = new ParameterList
            {
                EmployeeId = employeeId,
                TagId = tagId,
                StartDate = startDate.Value,
                EndDate = endDate.Value
            };
           
            var response = await client.PostAsJsonAsync("chronicle/home/dashboardtotals/get", filterDetails);

            if (response.IsSuccessStatusCode)
            {
                var totals = await response.Content.ReadFromJsonAsync<DashboardTotals>();
                return totals;
            }

            return null;
        }

    }
}
