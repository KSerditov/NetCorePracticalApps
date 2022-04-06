using Microsoft.AspNetCore.Builder;
using Packt.Shared;
using static System.Console;

namespace Northwind.Web
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddRazorPages();
            services.AddNorthwindContext();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (!env.IsDevelopment())
            {
                app.UseHsts();
            }
            else
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();
            app.Use(async (HttpContext context, Func<Task> next) =>
            {
                RouteEndpoint? rep = context.GetEndpoint() as RouteEndpoint;
                if (rep is not null)
                {
                    WriteLine($"Endpoint name: {rep.DisplayName}");
                    WriteLine($"Endpoint route pattern: {rep.RoutePattern.RawText}");
                }
                if (context.Request.Path == "/bonjour")
                {
                    await context.Response.WriteAsync("Bonjour");
                    return;
                }
                await next();
            });
            app.UseHttpsRedirection();
            app.UseDefaultFiles();
            app.UseStaticFiles();
            app.UseEndpoints(endpointBuilder =>
            {
                endpointBuilder.MapRazorPages();
                endpointBuilder.MapGet("/hello", () => "Hello World!");
            }
            );
        }
    }
}