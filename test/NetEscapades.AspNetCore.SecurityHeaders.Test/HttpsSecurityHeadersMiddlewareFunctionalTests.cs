using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using FluentAssertions;
using NetEscapades.AspNetCore.SecurityHeaders.Headers;
using NetEscapades.AspNetCore.SecurityHeaders.Test;
using Xunit;


namespace NetEscapades.AspNetCore.SecurityHeaders.Infrastructure;

public class HttpsSecurityHeadersMiddlewareFunctionalTests : IClassFixture<HttpsSecurityHeadersTestFixture<SecurityHeadersMiddlewareWebSite.Startup>>
{
    public HttpsSecurityHeadersMiddlewareFunctionalTests(HttpsSecurityHeadersTestFixture<SecurityHeadersMiddlewareWebSite.Startup> fixture)
    {
        Client = fixture.Client;
    }

    public HttpClient Client { get; }

    [Theory]
    [InlineData("GET")]
    [InlineData("HEAD")]
    [InlineData("POST")]
    [InlineData("PUT")]
    public async Task AllMethods_AddSecurityHeaders_IncludingStrict(string method)
    {
        // Arrange
        var path = "/SecurityHeadersMiddleware/BG36A632-C4D2-4B71-B2BD-18625ADDA87F";
        var request = new HttpRequestMessage(new HttpMethod(method), path);

        // Act
        var response = await Client.SendAsync(request);

        // Assert
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        var content = await response.Content.ReadAsStringAsync();
        Assert.Equal(path, content);
        var responseHeaders = response.Headers;

        responseHeaders.AssertSecureRequestDefaultSecurityHeaders();
        responseHeaders.Should().NotContain(x => x.Key == "Server");
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