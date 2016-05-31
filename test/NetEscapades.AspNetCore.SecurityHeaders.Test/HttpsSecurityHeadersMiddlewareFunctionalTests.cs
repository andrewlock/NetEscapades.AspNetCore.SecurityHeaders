using System;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Xunit;
using NetEscapades.AspNetCore.SecurityHeaders.Infrastructure.Constants;


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
            var header = Assert.Single(responseHeaders, x => x.Key == ContentTypeOptionsConstants.Header);
            Assert.Equal(new[] {ContentTypeOptionsConstants.NoSniff}, header.Value.ToArray());
            header = Assert.Single(responseHeaders, x => x.Key == FrameOptionsConstants.Header);
            Assert.Equal(new[] { FrameOptionsConstants.Deny}, header.Value.ToArray());
            header = Assert.Single(responseHeaders, x => x.Key == XssProtectionConstants.Header);
            Assert.Equal(new[] { XssProtectionConstants.Block }, header.Value.ToArray());
            header = Assert.Single(responseHeaders, x => x.Key == StrictTransportSecurityConstants.Header);
            var maxAgeString = string.Format(
                StrictTransportSecurityConstants.MaxAge,
                SecurityHeadersPolicyBuilder.OneYearInSeconds);

            //Assert.Equal(new[] { maxAgeString }, header.Value.ToArray());

            Assert.DoesNotContain(responseHeaders, x => x.Key == ServerConstants.Header);
        }
    }
}