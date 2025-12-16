using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;

namespace ClockNest.Endpoints;

    public static class LogoutEndpoint
    {
        public static void MapLogoutEndpoint(this WebApplication app)
        {
            app.MapPost("/logout", async (HttpContext httpContext) =>
            {
                await httpContext.SignOutAsync(
                    CookieAuthenticationDefaults.AuthenticationScheme
                );

                return Results.Ok();
            });
        }
    }
