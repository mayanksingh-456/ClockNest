using ClockNest.Models.UserClaimModel;
using Microsoft.AspNetCore.Components.Authorization;
using System.Security.Claims;

namespace ClockNest.Services.Auth
{
    public static class UserContextService
    {
        public static async Task<ClaimsPrincipal> GetUserAsync(AuthenticationStateProvider authStateProvider)
        {
            var authState = await authStateProvider.GetAuthenticationStateAsync();
            return authState.User;
        }

        public static async Task<int> GetUserIdAsync(AuthenticationStateProvider authStateProvider)
        {
            var user = await GetUserAsync(authStateProvider);
            var claim = user.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            return int.TryParse(claim, out var id) ? id : 0;
        }

        public static async Task<int> GetCompanyIdAsync(AuthenticationStateProvider authStateProvider)
        {
            var user = await GetUserAsync(authStateProvider);
            var claim = user.FindFirst("CompanyId")?.Value;

            return int.TryParse(claim, out var id) ? id : 0;
        }

        public static async Task<int> GetEmployeeIdAsync(AuthenticationStateProvider authStateProvider)
        {
            var user = await GetUserAsync(authStateProvider);
            var claim = user.FindFirst("EmployeeId")?.Value;
            return int.TryParse(claim, out var Id) ? Id : 0;
        }

        public static async Task<List<string>> GetRolesAsync(AuthenticationStateProvider authStateProvider)
        {
            var user = await GetUserAsync(authStateProvider);

            return user.FindAll(ClaimTypes.Role).Select(r => r.Value).ToList();
        }

        //public static async Task<UserClaimsModel> GetUserClaimsAsync(AuthenticationStateProvider authProvider)
        //{
        //    var user = await GetUserAsync(authProvider);

        //    return new UserClaimsModel
        //    {
        //        UserId = int.TryParse(
        //            user.FindFirst(ClaimTypes.NameIdentifier)?.Value, out var uid) ? uid : 0,

        //        CompanyId = int.TryParse(
        //            user.FindFirst("CompanyId")?.Value, out var cid) ? cid : 0,

        //        EmployeeId = int.TryParse(
        //            user.FindFirst("EmployeeId")?.Value, out var eid) ? eid : 0,

        //        UserName = user.Identity?.Name,
        //        Email = user.FindFirst(ClaimTypes.Email)?.Value,

        //        Roles = user.FindAll(ClaimTypes.Role)
        //                    .Select(r => r.Value)
        //                    .ToList()
        //    };
        //}

        public static async Task<UserClaimsModel> GetUserClaimsAsync(AuthenticationStateProvider authStateProvider)
        {
            var user = await GetUserAsync(authStateProvider);
            var roles = user?.FindAll(ClaimTypes.Role)
        ?.Select(r => r.Value)
        ?.ToList() ?? new List<string>();

            // If you suspect roles are stored as comma-separated in a single claim:
            if (roles.Count == 1 && roles[0].Contains(","))
            {
                roles = roles[0].Split(',').Select(r => r.Trim()).ToList();
            }

            return new UserClaimsModel
            {
                NameIdentifier = user?.FindFirst(ClaimTypes.NameIdentifier)?.Value,
                Name = user?.Identity?.Name,
                UserEmail = user?.FindFirst(ClaimTypes.Email)?.Value,
                CompanyId = user?.FindFirst("CompanyId")?.Value,
                RoleTypeId = user?.FindFirst("RoleTypeId")?.Value,
                EmployeeId = user?.FindFirst("EmployeeId")?.Value,
                Region = user?.FindFirst("Region")?.Value,
                AdditionalRoles = roles,
                StaffologyPayroll = user?.FindFirst("StaffologyPayrollEnabled")?.Value
            };
        }
    }
}
