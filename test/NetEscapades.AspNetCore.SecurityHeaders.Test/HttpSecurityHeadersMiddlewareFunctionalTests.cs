using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using FluentAssertions;
using Xunit;

namespace NetEscapades.AspNetCore.SecurityHeaders.Infrastructure
{
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
            var responseHeaders = response.Headers;
            var header = response.Headers.GetValues("X-Content-Type-Options").FirstOrDefault();
            header.Should().Be("nosniff");
            header = response.Headers.GetValues("X-Frame-Options").FirstOrDefault();
            header.Should().Be("DENY");
            header = response.Headers.GetValues("X-XSS-Protection").FirstOrDefault();
            header.Should().Be("1; mode=block");
            header = response.Headers.GetValues("Referrer-Policy").FirstOrDefault();
            header.Should().Be("origin-when-cross-origin");
            header = response.Headers.GetValues("Content-Security-Policy").FirstOrDefault();
            header.Should().Be("object-src 'none'; form-action 'self'; frame-ancestors 'none'");

            //http so no Strict transport
            responseHeaders.Should().NotContain(x => x.Key == "Strict-Transport-Security");
            responseHeaders.Should().NotContain(x => x.Key == "Server");
        }
    }
}