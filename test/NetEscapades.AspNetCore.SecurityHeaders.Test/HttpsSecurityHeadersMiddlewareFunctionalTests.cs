using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Xunit;


namespace NetEscapades.AspNetCore.SecurityHeaders.Infrastructure
{
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

            var header = response.Headers.GetValues("X-Content-Type-Options").FirstOrDefault();
            Assert.Equal(header, "nosniff");
            header = response.Headers.GetValues("X-Frame-Options").FirstOrDefault();
            Assert.Equal(header, "DENY");
            header = response.Headers.GetValues("X-XSS-Protection").FirstOrDefault();
            Assert.Equal(header, "1; mode=block");
            header = response.Headers.GetValues("Strict-Transport-Security").FirstOrDefault();
            Assert.Equal(header, $"max-age={StrictTransportSecurityHeader.OneYearInSeconds}");

            //http so no Strict transport
            Assert.DoesNotContain(responseHeaders, x => x.Key == "Server");
        }
    }
}