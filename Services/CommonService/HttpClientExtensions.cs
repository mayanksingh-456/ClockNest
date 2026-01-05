using ClockNest.Models.User_Model;
using ClockNest.Services.Auth;
using Microsoft.AspNetCore.Components.Authorization;

namespace ClockNest.Services.CommonService
{
    public static class HttpClientExtensions
    {
        public static HttpClient AddDefaultHeader(this HttpClient client, UserContext userContext)
        {
            var userId = userContext.UserId ?? "0";
          //  var userId = await UserContextService.GetUserIdAsync(AuthenticationStateProvider);

            if (client.DefaultRequestHeaders.Contains("X-User-Id"))
                client.DefaultRequestHeaders.Remove("X-User-Id");

            client.DefaultRequestHeaders.Add("X-User-Id", userId);

            return client;
        }
    }
}
