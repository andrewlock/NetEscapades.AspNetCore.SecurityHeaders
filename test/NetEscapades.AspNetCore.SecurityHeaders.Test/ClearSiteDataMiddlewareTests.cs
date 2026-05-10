using System;
using System.Linq;
using System.Threading.Tasks;
using FluentAssertions;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace NetEscapades.AspNetCore.SecurityHeaders.Test;
public class ClearSiteDataMiddlewareTests
{

    [Test]
    public async Task HttpRequest_WithClearSiteData_DoesNotSetHeader()
    {
        using var host = new HostBuilder()
            .ConfigureWebHost(b => b
                .UseTestServer()
                .Configure(app =>
                {
                    app.UseSecurityHeaders(p => p.AddClearSiteDataForLogout());
                    app.Run(async context =>
                    {
                        context.Response.ContentType = "text/html";
                        await context.Response.WriteAsync("Test response");
                    });
                }))
            .Build();
        await host.StartAsync();

        using var server = host.GetTestServer();
        var response = await server.CreateRequest("/").SendAsync("GET");

        response.EnsureSuccessStatusCode();
        response.Headers.Contains("Clear-Site-Data").Should()
            .BeFalse("Clear-Site-Data is only honoured by browsers in secure contexts");
    }

    [Test]
    public async Task SecureRequest_WithNamedLogoutPolicy_OnlySetsHeaderOnNamedEndpoint()
    {
        const string policyName = "Logout";
        using var host = new HostBuilder()
            .ConfigureWebHost(b => b
                .UseTestServer()
                .UseUrls("https://example.com:5001")
                .ConfigureServices(s =>
                {
                    s.AddRouting();
                    s.AddSecurityHeaderPolicies()
                        .SetDefaultPolicy(p => p.AddDefaultSecurityHeaders())
                        .AddPolicy(policyName, p => p.AddClearSiteDataForLogout());
                })
                .Configure(app =>
                {
                    app.UseSecurityHeaders();
                    app.UseRouting();
                    app.UseEndpoints(endpoints =>
                    {
                        endpoints.MapGet("/", async context =>
                        {
                            await context.Response.WriteAsync("home");
                        });
                        endpoints.MapGet("/logout", async context =>
                        {
                            await context.Response.WriteAsync("logged out");
                        }).WithSecurityHeadersPolicy(policyName);
                    });
                }))
            .Build();
        await host.StartAsync();

        using var server = host.GetTestServer();
        server.BaseAddress = new Uri("https://example.com:5001");

        var logoutResponse = await server.CreateRequest("/logout").SendAsync("GET");
        logoutResponse.EnsureSuccessStatusCode();
        var logoutHeader = logoutResponse.Headers.GetValues("Clear-Site-Data").FirstOrDefault();
        logoutHeader.Should().Be("\"cache\", \"cookies\", \"storage\", \"executionContexts\"");

        var homeResponse = await server.CreateRequest("/").SendAsync("GET");
        homeResponse.EnsureSuccessStatusCode();
        homeResponse.Headers.Contains("Clear-Site-Data").Should()
            .BeFalse("the default policy does not add Clear-Site-Data");
    }
}
