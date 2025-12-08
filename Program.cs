using ClockNest.Components;
using ClockNest.Handler;
using ClockNest.Helpers;
using ClockNest.Services.Auth;
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

            builder.Services.AddHttpClient("ChronicleClient", client =>
            {
                client.BaseAddress = new Uri(builder.Configuration["ChronicleWebApiConnection"]);
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            }).AddHttpMessageHandler<SecurityMessageHandler>();


            //builder.Services.AddScoped(sp =>
            //  new HttpClient { BaseAddress = new Uri("https://localhost:7209/") });

            builder.Services.AddHttpContextAccessor();
            builder.Services.AddScoped<Security>();
            builder.Services.AddTransient<SecurityMessageHandler>();
            builder.Services.AddScoped<AuthService>();

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
            app.UseAntiforgery();

            app.MapRazorComponents<App>()
                .AddInteractiveServerRenderMode();

            app.Run();
        }
    }
}
