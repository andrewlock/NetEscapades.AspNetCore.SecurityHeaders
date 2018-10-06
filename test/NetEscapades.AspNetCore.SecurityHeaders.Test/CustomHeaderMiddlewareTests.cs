using System;
using System.Linq;
using System.Threading.Tasks;
using FluentAssertions;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.DependencyInjection;
using NetEscapades.AspNetCore.SecurityHeaders.Infrastructure;
using Xunit;

namespace NetEscapades.AspNetCore.SecurityHeaders
{
    [Obsolete]
    public class CustomHeaderMiddlewareTests
    {
        [Fact]
        public async Task HttpRequest_WithDefaultSecurityPolicy_SetsSecurityHeaders()
        {
            // Arrange
            var hostBuilder = new WebHostBuilder()
                .ConfigureServices(services => services.AddCustomHeaders())
                .Configure(app =>
                           {
                               app.UseCustomHeadersMiddleware(
                                   new HeaderPolicyCollection()
                                       .AddDefaultSecurityHeaders());
                               app.Run(async context =>
                                             {
                                                 context.Response.ContentType = "text/html";
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

                (await response.Content.ReadAsStringAsync()).Should().Be("Test response");
                var header = response.Headers.GetValues("X-Content-Type-Options").FirstOrDefault();
                header.Should().Be("nosniff");
                header = response.Headers.GetValues("X-Frame-Options").FirstOrDefault();
                header.Should().Be("DENY");
                header = response.Headers.GetValues("X-XSS-Protection").FirstOrDefault();
                header.Should().Be("1; mode=block");

                Assert.False(response.Headers.Contains("Server"),
                    "Should not contain server header");
                Assert.False(response.Headers.Contains("Strict-Transport-Security"),
                    "Should not contain Strict-Transport-Security header over http");
            }
        }

        [Fact]
        public async Task SecureRequest_WithDefaultSecurityPolicy_SetsSecurityHeadersIncludingStrictTransport()
        {
            // Arrange
            var hostBuilder = new WebHostBuilder()
                .ConfigureServices(services => services.AddCustomHeaders())
                .UseUrls("https://localhost:5001")
                .Configure(app =>
                           {
                               app.UseCustomHeadersMiddleware(
                                   new HeaderPolicyCollection()
                                       .AddDefaultSecurityHeaders());
                               app.Run(async context =>
                                             {
                                                 context.Response.ContentType = "text/html";
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

                (await response.Content.ReadAsStringAsync()).Should().Be("Test response");
                var header = response.Headers.GetValues("X-Content-Type-Options").FirstOrDefault();
                header.Should().Be("nosniff");
                header = response.Headers.GetValues("X-Frame-Options").FirstOrDefault();
                header.Should().Be("DENY");
                header = response.Headers.GetValues("X-XSS-Protection").FirstOrDefault();
                header.Should().Be("1; mode=block");
                header = response.Headers.GetValues("Strict-Transport-Security").FirstOrDefault();
                header.Should().Be($"max-age={StrictTransportSecurityHeader.OneYearInSeconds}");

                Assert.False(response.Headers.Contains("Server"),
                    "Should not contain server header");
            }
        }

        [Fact]
        public async Task HttpRequest_WithCustomHeaderPolicy_SetsCustomHeader()
        {
            // Arrange
            var hostBuilder = new WebHostBuilder()
                .ConfigureServices(services => services.AddCustomHeaders())
                .Configure(app =>
                           {
                               app.UseCustomHeadersMiddleware(
                                   new HeaderPolicyCollection()
                                       .AddCustomHeader("X-My-Test-Header", "Header value"));
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

                (await response.Content.ReadAsStringAsync()).Should().Be("Test response");
                var header = response.Headers.GetValues("X-My-Test-Header").FirstOrDefault();
                header.Should().Be("Header value");
            }
        }

        [Fact]
        public async Task HttpRequest_WithRemoveCustomHeaderPolicy_RemovesHeader()
        {
            // Arrange
            var hostBuilder = new WebHostBuilder()
                .ConfigureServices(services => services.AddCustomHeaders())
                .Configure(app =>
                {
                               app.UseCustomHeadersMiddleware(
                                   new HeaderPolicyCollection()
                                       .AddCustomHeader("X-My-Test-Header", "Header value")
                                       .RemoveCustomHeader("X-My-Test-Header"));
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

                (await response.Content.ReadAsStringAsync()).Should().Be("Test response");
                response.Headers.Should().BeEmpty();
            }
        }

        [Fact]
        public async Task HttpRequest_WithCspHeader_SetsCsp()
        {
            // Arrange
            var hostBuilder = new WebHostBuilder()
                .ConfigureServices(services => services.AddCustomHeaders())
                .Configure(app =>
                {
                    app.UseCustomHeadersMiddleware(
                        new HeaderPolicyCollection()
                            .AddContentSecurityPolicy(builder =>
                            {
                                builder.AddDefaultSrc().Self();
                                builder.AddObjectSrc().None();
                            }));
                    app.Run(async context =>
                    {
                        context.Response.ContentType = "text/html";
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

                (await response.Content.ReadAsStringAsync()).Should().Be("Test response");
                var header = response.Headers.GetValues("Content-Security-Policy").FirstOrDefault();
                header.Should().NotBeNull();
                header.Should().Be("default-src 'self'; object-src 'none'");
            }
        }

        [Fact]
        public async Task HttpRequest_WithCspHeaderUsingReportOnly_SetsCspReportOnly()
        {
            // Arrange
            var hostBuilder = new WebHostBuilder()
                .ConfigureServices(services => services.AddCustomHeaders())
                .Configure(app =>
                {
                    app.UseCustomHeadersMiddleware(
                        new HeaderPolicyCollection()
                            .AddContentSecurityPolicy(builder =>
                            {
                                builder.AddDefaultSrc().Self();
                                builder.AddObjectSrc().None();
                            }, asReportOnly: true));
                    app.Run(async context =>
                    {
                        context.Response.ContentType = "text/html";
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

                (await response.Content.ReadAsStringAsync()).Should().Be("Test response");
                var header = response.Headers.GetValues("Content-Security-Policy-Report-Only").FirstOrDefault();
                header.Should().NotBeNull();
                header.Should().Be("default-src 'self'; object-src 'none'");
                response.Headers.Contains("Content-Security-Policy").Should().BeFalse();
            }
        }

        [Fact]
        public async Task HttpRequest_WithCspHeaderReportOnly_SetsCspReportOnly()
        {
            // Arrange
            var hostBuilder = new WebHostBuilder()
                .ConfigureServices(services => services.AddCustomHeaders())
                .Configure(app =>
                {
                    app.UseCustomHeadersMiddleware(
                        new HeaderPolicyCollection()
                            .AddContentSecurityPolicyReportOnly(builder =>
                            {
                                builder.AddDefaultSrc().Self();
                                builder.AddObjectSrc().None();
                            }));
                    app.Run(async context =>
                    {
                        context.Response.ContentType = "text/html";
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

                (await response.Content.ReadAsStringAsync()).Should().Be("Test response");
                var header = response.Headers.GetValues("Content-Security-Policy-Report-Only").FirstOrDefault();
                header.Should().NotBeNull();
                header.Should().Be("default-src 'self'; object-src 'none'");
                response.Headers.Contains("Content-Security-Policy").Should().BeFalse();
            }
        }

        // [Fact] Doesn't work with obsolete middleware
        public async Task HttpRequest_WithCspHeaderAndNonHtmlContentType_DoesNotSetCspHeader()
        {
            // Arrange
            var hostBuilder = new WebHostBuilder()
                .ConfigureServices(services => services.AddCustomHeaders())
                .Configure(app =>
                {
                    app.UseCustomHeadersMiddleware(
                        new HeaderPolicyCollection()
                            .AddContentSecurityPolicy(builder =>
                            {
                                builder.AddDefaultSrc().Self();
                                builder.AddObjectSrc().None();
                            }));
                    app.Run(async context =>
                    {
                        context.Response.ContentType = "text/plain";
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

                (await response.Content.ReadAsStringAsync()).Should().Be("Test response");
                response.Headers.Contains("Content-Security-Policy").Should().BeFalse();
            }
        }

        [Fact]
        public async Task HttpRequest_WithCspHeaderAndUnknonwnContentType_SetsCspHeader()
        {
            // Arrange
            var hostBuilder = new WebHostBuilder()
                .ConfigureServices(services => services.AddCustomHeaders())
                .Configure(app =>
                {
                    app.UseCustomHeadersMiddleware(
                        new HeaderPolicyCollection()
                            .AddContentSecurityPolicy(builder =>
                            {
                                builder.AddDefaultSrc().Self();
                                builder.AddObjectSrc().None();
                            }));
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

                (await response.Content.ReadAsStringAsync()).Should().Be("Test response");
                var header = response.Headers.GetValues("Content-Security-Policy").FirstOrDefault();
                header.Should().NotBeNull();
                header.Should().Be("default-src 'self'; object-src 'none'");
            }
        }

        [Fact]
        public async Task HttpRequest_WithFeaturePolicyHeader_SetsFeaturePolicy()
        {
            // Arrange
            var hostBuilder = new WebHostBuilder()
                .ConfigureServices(services => services.AddCustomHeaders())
                .Configure(app =>
                {
                    app.UseCustomHeadersMiddleware(
                        new HeaderPolicyCollection()
                            .AddFeaturePolicy(builder =>
                            {
                                builder.AddFullscreen().Self();
                                builder.AddGeolocation().None();
                            }));
                    app.Run(async context =>
                    {
                        context.Response.ContentType = "text/html";
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

                (await response.Content.ReadAsStringAsync()).Should().Be("Test response");
                var header = response.Headers.GetValues("Feature-Policy").FirstOrDefault();
                header.Should().NotBeNull();
                header.Should().Be("fullscreen 'self'; geolocation 'none'");
            }
        }

        [Fact]
        public async Task HttpRequest_WithFeaturePolicyHeaderAndUnknonwnContentType_SetsFeaturePolicyHeader()
        {
            // Arrange
            var hostBuilder = new WebHostBuilder()
                .ConfigureServices(services => services.AddCustomHeaders())
                .Configure(app =>
                {
                    app.UseCustomHeadersMiddleware(
                        new HeaderPolicyCollection()
                            .AddFeaturePolicy(builder =>
                            {
                                builder.AddFullscreen().Self();
                                builder.AddGeolocation().None();
                            }));
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

                (await response.Content.ReadAsStringAsync()).Should().Be("Test response");
                var header = response.Headers.GetValues("Feature-Policy").FirstOrDefault();
                header.Should().NotBeNull();
                header.Should().Be("fullscreen 'self'; geolocation 'none'");
            }
        }
    }
}
