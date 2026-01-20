using ClockNest.Components;
using ClockNest.Endpoints;
using ClockNest.Handler;
using ClockNest.Helpers;
using ClockNest.Models.User_Model;
using ClockNest.Services.Auth;
using ClockNest.Services.Employee_Service;
using ClockNest.Services.SelfService_Service;
using ClockNest.Services.Spinner_Service;
using ClockNest.Services.TagsService;
using ClockNest.Services.User_Service;
using Microsoft.AspNetCore.Authentication.Cookies;
using System.Net.Http.Headers;

namespace ClockNest
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddRazorComponents()
                .AddInteractiveServerComponents();

            builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
     .AddCookie(options =>
     {
         options.Cookie.Name = "clocknest-auth";
         options.LoginPath = "/login";
         options.LogoutPath = "/logout";
         options.ExpireTimeSpan = TimeSpan.FromMinutes(60);
         options.SlidingExpiration = true;

         options.Cookie.HttpOnly = true;
         options.Cookie.SameSite = SameSiteMode.Lax;
         options.Cookie.SecurePolicy = builder.Environment.IsDevelopment()
             ? CookieSecurePolicy.SameAsRequest
             : CookieSecurePolicy.Always;
     });

            builder.Services.AddAuthorization();
            builder.Services.AddCascadingAuthenticationState();


            builder.Services.AddHttpClient("ClockNestClient", client =>
            {
                client.BaseAddress = new Uri(builder.Configuration["ChronicleWebApiConnection"]);
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            }).AddHttpMessageHandler<SecurityMessageHandler>();


            //       builder.Services.AddScoped(sp =>
            //new HttpClient { BaseAddress = new Uri("https://localhost:5124/") });


            builder.Services.AddHttpContextAccessor();
            builder.Services.AddScoped<Security>();
            builder.Services.AddTransient<SecurityMessageHandler>();
            builder.Services.AddScoped<AuthService>();
            builder.Services.AddScoped<SpinnerService>();

            builder.Services.AddScoped<UserContext>();
            builder.Services.AddScoped<EmployeeService>();
            builder.Services.AddScoped<UserService>();
            builder.Services.AddScoped<SelfServices>();
            builder.Services.AddScoped<TagsService>();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();

            app.UseStaticFiles();

            app.UseRouting();

            //Add middleware
            app.UseAuthentication();
            app.UseAuthorization();

            app.UseAntiforgery();

            app.MapLoginEndpoint();
            app.MapLogoutEndpoint();

            app.MapRazorComponents<App>()
                .AddInteractiveServerRenderMode();

            app.Run();
        }
    }
}
