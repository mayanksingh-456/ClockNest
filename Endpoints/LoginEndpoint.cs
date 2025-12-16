using ClockNest.Models.UserClaimModel;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using System.Security.Claims;

namespace ClockNest.Endpoints;

public static class LoginEndpoint
{
    public static void MapLoginEndpoint(this WebApplication app)
    {
        app.MapPost("/login", async (
            UserClaimsModel model,
            HttpContext httpContext) =>
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, model.NameIdentifier),
                new Claim(ClaimTypes.Name, model.Name),
                new Claim(ClaimTypes.Email, model.UserEmail),
                new Claim("CompanyId", model.CompanyId),
                new Claim(ClaimTypes.Role, model.Role)
            };

            foreach (var role in model.AdditionalRoles)
            {
                claims.Add(new Claim(ClaimTypes.Role, role));
            }

            var identity = new ClaimsIdentity(
                claims,
                CookieAuthenticationDefaults.AuthenticationScheme
            );

            var principal = new ClaimsPrincipal(identity);

            await httpContext.SignInAsync(
                CookieAuthenticationDefaults.AuthenticationScheme,
                principal
            );

            return Results.Ok();
        });
    }
}