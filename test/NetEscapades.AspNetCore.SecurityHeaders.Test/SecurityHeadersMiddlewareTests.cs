using System;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.DependencyInjection;
using NetEscapades.AspNetCore.SecurityHeaders.Infrastructure;
using NetEscapades.AspNetCore.SecurityHeaders.Infrastructure.Constants;
using Moq;
using Xunit;

namespace NetEscapades.AspNetCore.SecurityHeaders
{
    public class SecurityHeadersMiddlewareTests
    {
        [Fact]
        public async Task HttpRequest_WithDefaultSecurityPolicy_SetsSecurityHeaders()
        {
            // Arrange
            var hostBuilder = new WebHostBuilder()
                .ConfigureServices(services => services.AddSecurityHeaders())
                .Configure(app =>
                           {
                               app.UseSecurityHeadersMiddleware(
                                   new SecurityHeadersPolicyBuilder()
                                       .AddDefaultSecurePolicy());
                               app.Run(async context =>
                                             {
                                                 await context.Response.WriteAsync("Test response");
                                             });
                           });

            using (var server = new TestServer(hostBuilder))
            {
                // Act
                // Actual request.
                var response = await server.CreateRequest("/")
                    .SendAsync("PUT");

                // Assert
                response.EnsureSuccessStatusCode();

                Assert.Equal("Test response", await response.Content.ReadAsStringAsync());
                var header = response.Headers.GetValues(ContentTypeOptionsConstants.Header).FirstOrDefault();
                Assert.Equal(header, ContentTypeOptionsConstants.NoSniff);
                header = response.Headers.GetValues(FrameOptionsConstants.Header).FirstOrDefault();
                Assert.Equal(header, FrameOptionsConstants.Deny);
                header = response.Headers.GetValues(XssProtectionConstants.Header).FirstOrDefault();
                Assert.Equal(header, XssProtectionConstants.Block);

                Assert.False(response.Headers.Contains(ServerConstants.Header), 
                    "Should not contain server header");
                Assert.False(response.Headers.Contains(StrictTransportSecurityConstants.Header), 
                    "Should not contain Strict-Transport-Security header over http");
            }
        }

        [Fact]
        public async Task SecureRequest_WithDefaultSecurityPolicy_SetsSecurityHeadersIncludingStrictTransport()
        {
            // Arrange
            var hostBuilder = new WebHostBuilder()
                .UseUrls("https://localhost:5001")
                .ConfigureServices(services => services.AddSecurityHeaders())
                .Configure(app =>
                           {
                               app.UseSecurityHeadersMiddleware(
                                   new SecurityHeadersPolicyBuilder()
                                       .AddDefaultSecurePolicy());
                               app.Run(async context =>
                                             {
                                                 await context.Response.WriteAsync("Test response");
                                             });
                           });

            using (var server = new TestServer(hostBuilder))
            {
                // Act
                // Actual request.
                server.BaseAddress = new Uri("https://localhost:5001");
                var response = await server.CreateRequest("/")
                    .SendAsync("PUT");

                // Assert
                response.EnsureSuccessStatusCode();

                Assert.Equal("Test response", await response.Content.ReadAsStringAsync());
                var header = response.Headers.GetValues(ContentTypeOptionsConstants.Header).FirstOrDefault();
                Assert.Equal(header, ContentTypeOptionsConstants.NoSniff);
                header = response.Headers.GetValues(FrameOptionsConstants.Header).FirstOrDefault();
                Assert.Equal(header, FrameOptionsConstants.Deny);
                header = response.Headers.GetValues(XssProtectionConstants.Header).FirstOrDefault();
                Assert.Equal(header, XssProtectionConstants.Block);
                header = response.Headers.GetValues(StrictTransportSecurityConstants.Header).FirstOrDefault();
                Assert.Equal(header, string.Format(StrictTransportSecurityConstants.MaxAge,
                    SecurityHeadersPolicyBuilder.OneYearInSeconds));

                Assert.False(response.Headers.Contains(ServerConstants.Header),
                    "Should not contain server header");
            }
        }
    }
}
