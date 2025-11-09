using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using FluentAssertions;
using NetEscapades.AspNetCore.SecurityHeaders.Test;
using SecurityHeadersMiddlewareWebSite;

namespace NetEscapades.AspNetCore.SecurityHeaders.Infrastructure;
[ClassDataSource<HttpSecurityHeadersTestFixture<SecurityHeadersMiddlewareWebSite.Startup>>(Shared = SharedType.PerClass)]
public class HttpSecurityHeadersMiddlewareFunctionalTests
{
    private readonly HttpSecurityHeadersTestFixture<Startup> _fixture;
    private const string EchoPath = "/SecurityHeadersMiddleware/EC6AA70D-BA3E-4B71-A87F-18625ADDB2BD";
    public HttpSecurityHeadersMiddlewareFunctionalTests(HttpSecurityHeadersTestFixture<SecurityHeadersMiddlewareWebSite.Startup> fixture)
    {
        _fixture = fixture;
    }

    [Test]
    [Arguments("GET", EchoPath)]
    [Arguments("HEAD", EchoPath)]
    [Arguments("POST", EchoPath)]
    [Arguments("PUT", EchoPath)]
    [Arguments("GET", "/api/index")]
    public async Task AllMethods_AddSecurityHeaders_ExceptStrict(string method, string path)
    {
        // Arrange
        var client = await _fixture.GetClient();
        var request = new HttpRequestMessage(new HttpMethod(method), path);
        // Act
        var response = await client.SendAsync(request);
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
        var client = await _fixture.GetClient();
        // Act
        var response = await client.GetAsync(path);
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