using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using FluentAssertions;
using NetEscapades.AspNetCore.SecurityHeaders.Test;

namespace NetEscapades.AspNetCore.SecurityHeaders.Infrastructure;
[ClassDataSource<NetEscapades.AspNetCore.SecurityHeaders.Infrastructure.HttpSecurityHeadersTestFixture<SecurityHeadersMiddlewareWebSite.Startup>>(Shared = SharedType.PerClass)]
public class HttpSecurityHeadersMiddlewareFunctionalTests
{
    private const string EchoPath = "/SecurityHeadersMiddleware/EC6AA70D-BA3E-4B71-A87F-18625ADDB2BD";
    public HttpSecurityHeadersMiddlewareFunctionalTests(HttpSecurityHeadersTestFixture<SecurityHeadersMiddlewareWebSite.Startup> fixture)
    {
        Client = fixture.Client;
    }

    public HttpClient Client { get; }

    [Test]
    [Arguments("GET", EchoPath)]
    [Arguments("HEAD", EchoPath)]
    [Arguments("POST", EchoPath)]
    [Arguments("PUT", EchoPath)]
    [Arguments("GET", "/api/index")]
    public async Task AllMethods_AddSecurityHeaders_ExceptStrict(string method, string path)
    {
        // Arrange
        var request = new HttpRequestMessage(new HttpMethod(method), path);
        // Act
        var response = await Client.SendAsync(request);
        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.OK);
        var content = await response.Content.ReadAsStringAsync();
        content.Should().Be(path);
        response.Headers.AssertHttpRequestDefaultSecurityHeaders();
        //http so no Strict transport
        response.Headers.Should().NotContain(x => x.Key == "Strict-Transport-Security");
        response.Headers.Should().NotContain(x => x.Key == "Server");
    }

    [Test]
    [Arguments("/custom", "Hello World!")]
    [Arguments("/api/custom", "Hello Controller!")]
    public async Task WhenUsingEndpoint_Overrides_Default(string path, string expected)
    {
        // Arrange
        // Act
        var response = await Client.GetAsync(path);
        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.OK);
        var content = await response.Content.ReadAsStringAsync();
        content.Should().Be(expected);
        // no security headers
        response.Headers.Should().NotContainKey("X-Frame-Options");
        response.Headers.TryGetValues("Custom-Header", out var customHeader).Should().BeTrue();
        customHeader.Should().ContainSingle("MyValue");
    }
}