using ClockNest.Components.Pages.Settings.Organisations.User_Tab;
using ClockNest.Models.Tag_Modal;
using ClockNest.Models.User_Model;
using ClockNest.Services.CommonService;
using ClockNest.ViewModels.Parameter_List;
using Microsoft.AspNetCore.Components.Authorization;

namespace ClockNest.Services.TagsService
{
    public class TagsService
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly UserContext _userContext;
        private readonly AuthenticationStateProvider _auth;

        public TagsService(IHttpClientFactory httpClientFactory, UserContext userContext, AuthenticationStateProvider auth)
        {
            _httpClientFactory = httpClientFactory;
            _userContext = userContext;
            _auth = auth;
        }

        public async Task<List<Tag>> GetTagsWithArchived(int companyId, int userId, int roleTypeId)
        {
            var client = _httpClientFactory.CreateClient("ClockNestClient").AddDefaultHeader(_userContext);
            var parameterList = new ParameterList
            {
                CompanyId = companyId,
                UserId = userId,
                RoleTypeId = roleTypeId
            };
            var response = await client.PostAsJsonAsync("chronicle/setup/organisation/tagswitharchived/get", parameterList);
            if (response.IsSuccessStatusCode)
            {
                var tags = await response.Content.ReadFromJsonAsync<List<Tag>>();
                return tags ?? new List<Tag>();
            }
            else
            {
                var errorContent = await response.Content.ReadAsStringAsync();
                throw new Exception($"Failed to fetch tags: {response.StatusCode} - {errorContent}");
            }
        }
    }
}
