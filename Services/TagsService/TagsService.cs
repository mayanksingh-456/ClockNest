using ClockNest.Components.Pages.Settings.Organisations.User_Tab;
using ClockNest.Models.Employee_Model;
using ClockNest.Models.SelfService_Model;
using ClockNest.Models.Tag_Modal;
using ClockNest.Models.User_Model;
using ClockNest.Services.CommonService;
using ClockNest.ViewModels.Parameter_List;
using ClockNest.ViewModels.TagViewModel;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Security.Claims;

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

        public async Task<CreateEditTagViewModel> GetTagsAsync(int companyId)
        {
            var client = _httpClientFactory.CreateClient("ClockNestClient").AddDefaultHeader(_userContext);
            var model = new CreateEditTagViewModel();

            // Budgets
            var budgetResponse = await client.PostAsJsonAsync("chronicle/setup/organisation/budgets/get", companyId);

            if (budgetResponse.IsSuccessStatusCode)
            {
                var budgets = await budgetResponse.Content.ReadFromJsonAsync<List<Budget>>();
                model.BudgetPlans = budgets?
                    .Select(b => new SelectListItem
                    {
                        Value = b.Id.ToString(),
                        Text = b.Name
                    })
                    .ToList() ?? new List<SelectListItem>();

                model.BudgetPlans.Insert(0, new SelectListItem { Value = "", Text = "" });
            }

            // Holiday Thresholds
            var holidayResponse = await client.PostAsJsonAsync("chronicle/setup/hsa/holidaythresholds/get", companyId);

            if (holidayResponse.IsSuccessStatusCode)
            {
                var thresholds = await holidayResponse.Content.ReadFromJsonAsync<List<HolidayThreshold>>();
                model.HolidayThresholds = thresholds?
                    .Select(h => new SelectListItem
                    {
                        Value = h.Id.ToString(),
                        Text = h.Name
                    })
                    .ToList() ?? new List<SelectListItem>();

                model.HolidayThresholds.Insert(0, new SelectListItem { Value = "", Text = "" });
                model.AssignShiftsToTags = false;
            }

            //Company
            var companyResponse = await client.PostAsJsonAsync("chronicle/account/company/get", companyId);
            if (companyResponse.IsSuccessStatusCode)
            {
                var company = await companyResponse.Content.ReadFromJsonAsync<Company>();
                model.AssignShiftsToTags = company?.AssignShiftsToTags ?? false;
            }
            return model;
        }

        //Save tag
        public async Task<bool> SaveTagAsync(Tag tag)
        {
            var client = _httpClientFactory.CreateClient("ClockNestClient").AddDefaultHeader(_userContext);
            var response = await client.PostAsJsonAsync("chronicle/setup/organisation/tag/post", tag);
            if (response.IsSuccessStatusCode)
            {
                return true;
            }
            else
            {
                var errorContent = await response.Content.ReadAsStringAsync();
                throw new Exception($"Failed to save tag: {response.StatusCode} - {errorContent}");
            }
        }

        //Delete tag
        //public async Task<bool> DeleteTagAsync(List<Tag> selectedTag)
        //{
        //    var client = _httpClientFactory.CreateClient("ClockNestClient").AddDefaultHeader(_userContext);
        //    var response = await client.PostAsJsonAsync("chronicle/setup/organisation/tagsdelete/post", selectedTag);
        //    if (response.IsSuccessStatusCode)
        //    {
        //        return true;
        //    }
        //    else
        //    {
        //        var errorContent = await response.Content.ReadAsStringAsync();
        //        throw new Exception($"Failed to delete tag: {response.StatusCode} - {errorContent}");
        //    }
        //}

        public async Task<bool> DeleteTagAsync(List<Tag> selectedTags)
        {
            if (selectedTags == null || selectedTags.Count == 0)
                throw new ArgumentException("tags cannot be empty");

            var client = _httpClientFactory.CreateClient("ClockNestClient").AddDefaultHeader(_userContext);
            var response = await client.PostAsJsonAsync("chronicle/setup/organisation/tagsdelete/post", selectedTags);

            if (response.IsSuccessStatusCode)
                return true;

            var errorContent = await response.Content.ReadAsStringAsync();
            throw new Exception($"Failed to delete tag: {response.StatusCode} - {errorContent}");
        }

        //Get access tag list
        public async Task<List<Tag>> GetAccessTagsAsync(bool isSelfService = false)
        {
            var authState = await _auth.GetAuthenticationStateAsync();
            var user = authState.User;

            if (!user.Identity.IsAuthenticated)
                return new List<Tag>();

            int companyId = int.Parse(user.Claims.First(c => c.Type == "CompanyId").Value);
            int userId = int.Parse(user.Claims.First(c => c.Type == ClaimTypes.NameIdentifier).Value);

            int roleTypeId = isSelfService
                ? 1
                : int.Parse(user.Claims.First(c => c.Type == "RoleTypeId").Value);

            var parameterList = new ParameterList
            {
                CompanyId = companyId,
                UserId = userId,
                RoleTypeId = roleTypeId
            };

            var client = _httpClientFactory.CreateClient("ClockNestClient").AddDefaultHeader(_userContext);
            var response = await client.PostAsJsonAsync("chronicle/setup/organisation/tags/get", parameterList);
            if (!response.IsSuccessStatusCode)
                return new List<Tag>();

            var tags = await response.Content.ReadFromJsonAsync<List<Tag>>();

            return tags;
        }

    }
}
