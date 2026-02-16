using ClockNest.Models.Dashboard_Model;
using ClockNest.Models.SelfService_Model;
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


        public async Task<List<DashboardTopActivities>> GetDashboardTopActivitiesAsync(int tagId, DateTime? startDate, DateTime? endDate)
        {
            startDate ??= DateTime.Now.AddMonths(-1);
            endDate ??= DateTime.Now;

            var filterDetails = new ParameterList
            {
                TagId = tagId,
                StartDate = startDate.Value,
                EndDate = endDate.Value
            };
            var client = _httpClientFactory.CreateClient("ClockNestClient").AddDefaultHeader(_userContext);
            var response = await client.PostAsJsonAsync("chronicle/home/dashboardtopactivities/get", filterDetails);

            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadFromJsonAsync<List<DashboardTopActivities>>();
                return result ?? new List<DashboardTopActivities>();
            }

            return new List<DashboardTopActivities>();
        }

        public async Task<List<DashboardTopCostCentres>> GetDashboardTopCostCentreAsync(int tagId, DateTime? startDate, DateTime? endDate)
        {
            startDate ??= DateTime.Now.AddMonths(-1);
            endDate ??= DateTime.Now;

            var filterDetails = new ParameterList
            {
                TagId = tagId,
                StartDate = startDate.Value,
                EndDate = endDate.Value
            };
            var client = _httpClientFactory.CreateClient("ClockNestClient").AddDefaultHeader(_userContext);
            var response = await client.PostAsJsonAsync("chronicle/home/dashboardtopcostcentres/get", filterDetails);

            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadFromJsonAsync<List<DashboardTopCostCentres>>();
                return result ?? new List<DashboardTopCostCentres>();
            }

            return new List<DashboardTopCostCentres>();
        }

        public async Task<List<DashboardTopLateEmployees>> GetDashboardTopLateEmployeesAsync(int tagId, DateTime? startDate, DateTime? endDate)
        {
            startDate ??= DateTime.Now.AddMonths(-1);
            endDate ??= DateTime.Now;

            var filterDetails = new ParameterList
            {
                TagId = tagId,
                StartDate = startDate.Value,
                EndDate = endDate.Value
            };
            var client = _httpClientFactory.CreateClient("ClockNestClient").AddDefaultHeader(_userContext);
            var response = await client.PostAsJsonAsync("chronicle/home/dashboardTopLateEmployees/get", filterDetails);

            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadFromJsonAsync<List<DashboardTopLateEmployees>>();
                return result ?? new List<DashboardTopLateEmployees>();
            }

            return new List<DashboardTopLateEmployees>();
        }

        public async Task<List<DashboardTopJobAndFinish>> GetDashboardTopJobAndFinishAsync(int tagId, DateTime? startDate, DateTime? endDate)
        {
            startDate ??= DateTime.Now.AddMonths(-1);
            endDate ??= DateTime.Now;

            var filterDetails = new ParameterList
            {
                TagId = tagId,
                StartDate = startDate.Value,
                EndDate = endDate.Value
            };
            var client = _httpClientFactory.CreateClient("ClockNestClient").AddDefaultHeader(_userContext);
            var response = await client.PostAsJsonAsync("chronicle/home/dashboardTopJobAndFinish/get", filterDetails);

            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadFromJsonAsync<List<DashboardTopJobAndFinish>>();
                return result ?? new List<DashboardTopJobAndFinish>();
            }

            return new List<DashboardTopJobAndFinish>();
        }

        public async Task<List<DashboardTopExceptionalItems>> GetDashboardTopExceptionalItemsAsync(int tagId, DateTime? startDate, DateTime? endDate)
        {
            startDate ??= DateTime.Now.AddMonths(-1);
            endDate ??= DateTime.Now;

            var filterDetails = new ParameterList
            {
                TagId = tagId,
                StartDate = startDate.Value,
                EndDate = endDate.Value
            };
            var client = _httpClientFactory.CreateClient("ClockNestClient").AddDefaultHeader(_userContext);
            var response = await client.PostAsJsonAsync("chronicle/home/DashboardTopExceptionalItems/get", filterDetails);

            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadFromJsonAsync<List<DashboardTopExceptionalItems>>();
                return result ?? new List<DashboardTopExceptionalItems>();
            }

            return new List<DashboardTopExceptionalItems>();
        }

        public async Task<List<DashboardTopOvertime>> GetDashboardTopOvertimeAsync(int tagId, DateTime? startDate, DateTime? endDate)
        {
            startDate ??= DateTime.Now.AddMonths(-1);
            endDate ??= DateTime.Now;

            var filterDetails = new ParameterList
            {
                TagId = tagId,
                StartDate = startDate.Value,
                EndDate = endDate.Value
            };
            var client = _httpClientFactory.CreateClient("ClockNestClient").AddDefaultHeader(_userContext);
            var response = await client.PostAsJsonAsync("chronicle/home/dashboardTopOvertime/get", filterDetails);

            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadFromJsonAsync<List<DashboardTopOvertime>>();
                return result ?? new List<DashboardTopOvertime>();
            }

            return new List<DashboardTopOvertime>();
        }

        public async Task<DashboardChartData?> GetDashboardAbsenceAsync( int tagId, DateTime? startDate, DateTime? endDate)
        {
            startDate ??= DateTime.Now.AddMonths(-1);
            endDate ??= DateTime.Now;

            var filterDetails = new ParameterList
            {
                TagId = tagId,
                StartDate = startDate.Value,
                EndDate = endDate.Value
            };
            var client = _httpClientFactory.CreateClient("ClockNestClient").AddDefaultHeader(_userContext);

            var response = await client.PostAsJsonAsync("chronicle/home/dashboardabsence/get", filterDetails);

            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadFromJsonAsync<DashboardChartData>();
            }

            return null;
        }

        public async Task<DashboardChartData?> GetDashboardOvertimeAsync(int tagId, DateTime? startDate, DateTime? endDate)
        {
            startDate ??= DateTime.Now.AddMonths(-1);
            endDate ??= DateTime.Now;

            var filterDetails = new ParameterList
            {
                TagId = tagId,
                StartDate = startDate.Value,
                EndDate = endDate.Value
            };
            var client = _httpClientFactory.CreateClient("ClockNestClient").AddDefaultHeader(_userContext);

            var response = await client.PostAsJsonAsync("chronicle/home/dashboardtopovertimereasons/get", filterDetails);

            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadFromJsonAsync<DashboardChartData>();
            }

            return null;
        }

        public async Task<DashboardChartData?> GetDashboardHoursWorkedVsContracted(int tagId, DateTime? startDate, DateTime? endDate)
        {
            var filterDetails = new ParameterList
            {
                TagId = tagId,
                StartDate = startDate.Value,
                EndDate = endDate.Value
            };
            var client = _httpClientFactory.CreateClient("ClockNestClient").AddDefaultHeader(_userContext);
            var response = await client.PostAsJsonAsync("chronicle/home/dashboardhoursworkedvscontracted/get", filterDetails);

            if (response.IsSuccessStatusCode)
            {
                var dashboardData = await response.Content.ReadFromJsonAsync<DashboardChartData>();
                dashboardData.Datasets[0].Label = "Hours Worked";
                dashboardData.Datasets[1].Label = "Holiday";
                dashboardData.Datasets[2].Label = "Sickness";
                dashboardData.Datasets[3].Label = "Absence";
                dashboardData.Datasets[4].Label = "Contracted";
                return dashboardData;
            }

            return null;
        }
    }
}
