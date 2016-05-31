using System;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Xunit;
using NetEscapades.AspNetCore.SecurityHeaders.Infrastructure.Constants;

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

            //http so no Strict transport
            Assert.DoesNotContain(responseHeaders, x => x.Key == StrictTransportSecurityConstants.Header);
            Assert.DoesNotContain(responseHeaders, x => x.Key == ServerConstants.Header);
        }
    }
}