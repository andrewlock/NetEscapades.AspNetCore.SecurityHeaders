using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using FluentAssertions;
using NetEscapades.AspNetCore.SecurityHeaders.Infrastructure;
using RazorWebSite;

namespace NetEscapades.AspNetCore.SecurityHeaders.Test;

[ClassDataSource<HttpsSecurityHeadersTestFixture<RazorWebSite.Startup>>(Shared = SharedType.PerClass)]
public class RazorWebSiteFunctionalTests
{
    private readonly HttpsSecurityHeadersTestFixture<Startup> _fixture;
    public RazorWebSiteFunctionalTests(HttpsSecurityHeadersTestFixture<RazorWebSite.Startup> fixture)
    {
        _fixture = fixture;
    }

    [Test]
    [Arguments("GET")]
    [Arguments("HEAD")]
    public async Task AllMethods_AddSecurityHeaders_IncludesCsp(string method)
    {
        // Arrange
        var client = await _fixture.GetClient();
        var request = new HttpRequestMessage(new HttpMethod(method), "/");
        // Act
        var response = await client.SendAsync(request);
        
        
        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.OK);
        var content = await response.Content.ReadAsStringAsync();
        content.Should().Contain("Haha, malicious javascript blocked");
        var responseHeaders = response.Headers;

        response.Headers.GetValues("Content-Security-Policy")
            .Should().ContainSingle().Which.Should()
            .Contain("WuuOVwpUdf7Fb0r2WZxkqiv5V457zV2zpgSjN0Jy63Q=");
        //http so no Strict transport
        response.Headers.Should().NotContain(x => x.Key == "Server");
    }

    [Test]
    [Arguments("/api", "ping-pong")]
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
        // no CSP
        response.Headers.Should().NotContainKey("Content-Security-Policy");
        response.Headers.TryGetValues("Custom-Header", out var customHeader).Should().BeTrue();
        customHeader.Should().ContainSingle("MyValue");
    }

    [Test]
    [Arguments("/blah")]
    [Arguments("/Fallback")]
    public async Task WhenUsingFallback_Overrides_Default(string path)
    {
        // Arrange
        var client = await _fixture.GetClient();
        // Act
        var response = await client.GetAsync(path);
        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.OK);
        var content = await response.Content.ReadAsStringAsync();
        content.Should().Contain("This is the Fallback page");
        // no CSP
        response.Headers.Should().NotContainKey("Content-Security-Policy");
        response.Headers.TryGetValues("Fallback-Header", out var customHeader).Should().BeTrue();
        customHeader.Should().ContainSingle("FallbackPage");
    }
}