using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using FluentAssertions;
using NetEscapades.AspNetCore.SecurityHeaders.Test;
using Xunit;

namespace NetEscapades.AspNetCore.SecurityHeaders.Infrastructure;

public class HttpSecurityHeadersMiddlewareFunctionalTests : IClassFixture<HttpSecurityHeadersTestFixture<SecurityHeadersMiddlewareWebSite.Startup>>
{
    private const string EchoPath = "/SecurityHeadersMiddleware/EC6AA70D-BA3E-4B71-A87F-18625ADDB2BD";
    public HttpSecurityHeadersMiddlewareFunctionalTests(HttpSecurityHeadersTestFixture<SecurityHeadersMiddlewareWebSite.Startup> fixture)
    {
        Client = fixture.Client;
    }

    public HttpClient Client { get; }

    [Theory]
    [InlineData("GET", EchoPath)]
    [InlineData("HEAD", EchoPath)]
    [InlineData("POST", EchoPath)]
    [InlineData("PUT", EchoPath)]
    [InlineData("GET", "/api/index")]
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

    [Theory]
    [InlineData("/custom", "Hello World!")]
    [InlineData("/api/custom", "Hello Controller!")]
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
        response.Headers.Should().NotContain("X-Frame-Options");
        response.Headers.TryGetValues("Custom-Header", out var customHeader).Should().BeTrue();
        customHeader.Should().ContainSingle("MyValue");
    }
}