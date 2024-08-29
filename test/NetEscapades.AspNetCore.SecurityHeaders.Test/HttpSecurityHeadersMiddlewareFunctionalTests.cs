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
    public HttpSecurityHeadersMiddlewareFunctionalTests(HttpSecurityHeadersTestFixture<SecurityHeadersMiddlewareWebSite.Startup> fixture)
    {
        Client = fixture.Client;
    }

    public HttpClient Client { get; }

    [Theory]
    [InlineData("GET")]
    [InlineData("HEAD")]
    [InlineData("POST")]
    [InlineData("PUT")]
    public async Task AllMethods_AddSecurityHeaders_ExceptStrict(string method)
    {
        // Arrange
        var path = "/SecurityHeadersMiddleware/EC6AA70D-BA3E-4B71-A87F-18625ADDB2BD";
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

    [Fact]
    public async Task WhenUsingEndpoint_Overrides_Default()
    {
        // Arrange
        // Act
        var response = await Client.GetAsync("/custom");

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.OK);
        var content = await response.Content.ReadAsStringAsync();
        content.Should().Be("Hello World!");

        // no security headers
        response.Headers.Should().NotContain("X-Frame-Options");
        response.Headers.TryGetValues("Custom-Header", out var customHeader).Should().BeTrue();
        customHeader.Should().ContainSingle("MyValue");
    }
}