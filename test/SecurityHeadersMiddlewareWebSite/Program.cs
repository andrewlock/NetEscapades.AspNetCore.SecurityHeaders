using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using SecurityHeadersMiddlewareWebSite;

using var host = new HostBuilder()
    .ConfigureWebHost(webHostBuilder =>
    {
        webHostBuilder
            .UseKestrel()
            .UseIISIntegration()
            .UseStartup<Startup>();
    })
    .Build();
await host.StartAsync();