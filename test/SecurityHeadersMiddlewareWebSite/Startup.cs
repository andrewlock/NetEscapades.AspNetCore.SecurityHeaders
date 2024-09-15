using System.Text;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using NetEscapades.AspNetCore.SecurityHeaders;

namespace SecurityHeadersMiddlewareWebSite;

public class Startup
{
    public void ConfigureServices(IServiceCollection services)
    {
        services.AddControllers();
        services.AddSecurityHeaderPolicies()
            .AddPolicy("CustomHeader", policy =>
            {
                policy.AddCustomHeader("Custom-Header", "MyValue");
            });
    }

    public void Configure(IApplicationBuilder app)
    {
        app.UseSecurityHeaders();
        app.UseRouting();
        app.UseEndpoints(endpoints =>
        {
            endpoints.MapGet("/custom", context => context.Response.WriteAsync("Hello World!"))
                .WithSecurityHeadersPolicy("CustomHeader");

            endpoints.MapFallback(context =>
            {
                context.Response.ContentType = "text/html; charset=utf-8";
                var path = context.Request.PathBase + context.Request.Path + context.Request.QueryString;
                return context.Response.WriteAsync(path, Encoding.UTF8);
            });

            endpoints.MapControllers();
        });
    }

    public static void Main(string[] args)
    {
        var host = new WebHostBuilder()
            .UseKestrel()
            .UseIISIntegration()
            .UseStartup<Startup>()
            .Build();

        host.Run();
    }
}