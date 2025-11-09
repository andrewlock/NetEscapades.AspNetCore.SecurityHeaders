using System;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.Hosting;

namespace NetEscapades.AspNetCore.SecurityHeaders.Infrastructure;

public class SecurityHeadersTestFixtureBase<TStartup> : IAsyncDisposable
    where TStartup : class
{
    private readonly string _baseUri;
    private readonly IHost _host;
    private readonly Lazy<Task<(TestServer, HttpClient)>> _initialize;

    public SecurityHeadersTestFixtureBase(string baseUri)
    {
        _baseUri = baseUri;
        _host = new HostBuilder()
            .ConfigureWebHost(b => b
                .UseTestServer()
                .UseStartup<TStartup>()
            ).Build();
        _initialize = new(CreateClient);
    }

    public async Task<HttpClient> GetClient()
        => (await _initialize.Value.ConfigureAwait(false)).Item2;

    public async ValueTask DisposeAsync()
    {
        var result = await _initialize.Value.ConfigureAwait(false);
        result.Item2.Dispose();
        result.Item1.Dispose();
        _host.Dispose();
    }

    private async Task<(TestServer, HttpClient)> CreateClient()
    {
        await _host.StartAsync();
        var server = _host.GetTestServer();

        var client = server.CreateClient();
        client.BaseAddress = new Uri(_baseUri);
        return (server, client);
    }
}