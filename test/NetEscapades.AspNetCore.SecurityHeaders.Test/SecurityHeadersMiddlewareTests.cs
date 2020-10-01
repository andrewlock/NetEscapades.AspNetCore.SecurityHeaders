using System;
using System.Linq;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using FluentAssertions;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.TestHost;
using NetEscapades.AspNetCore.SecurityHeaders.Headers;
using Xunit;

namespace NetEscapades.AspNetCore.SecurityHeaders.Test
{
    public class SecurityHeadersMiddlewareTests
    {
        [Fact]
        public async Task HttpRequest_WithDefaultSecurityHeaders_SetsSecurityHeaders()
        {
            // Arrange
            var hostBuilder = new WebHostBuilder()
                .Configure(app =>
                           {
                               app.UseSecurityHeaders(
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
                AssertHttpRequestDefaultSecurityHeaders(response.Headers);
            }
        }

        [Fact]
        public async Task HttpRequest_WithDefaultSecurityHeadersUsingConfigureAction_SetsSecurityHeaders()
        {
            // Arrange
            var hostBuilder = new WebHostBuilder()
                .Configure(app =>
                           {
                               app.UseSecurityHeaders(policies => policies.AddDefaultSecurityHeaders());
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
                AssertHttpRequestDefaultSecurityHeaders(response.Headers);
            }
        }

        [Fact]
        public async Task SecureRequest_WithDefaultSecurityHeaders_WhenNotOnLocalhost_SetsSecurityHeadersIncludingStrictTransport()
        {
            // Arrange
            var hostBuilder = new WebHostBuilder()
                .UseUrls("https://example.com:5001")
                .Configure(app =>
                {
                    app.UseSecurityHeaders(
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
                server.BaseAddress = new Uri("https://example.com:5001");
                var response = await server.CreateRequest("/")
                    .SendAsync("PUT");

                // Assert
                response.EnsureSuccessStatusCode();

                (await response.Content.ReadAsStringAsync()).Should().Be("Test response");
                AssertSecureRequestDefaultSecurityHeaders(response.Headers);
            }
        }

        [Fact]
        public async Task SecureRequest_WithDefaultSecurityHeaders_WhenOnLocalhost_DoesNotSetStrictTransportSecurityHeader()
        {
            // Arrange
            var hostBuilder = new WebHostBuilder()
                .UseUrls("https://localhost:5001")
                .Configure(app =>
                           {
                               app.UseSecurityHeaders(
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
                Assert.False(response.Headers.Contains("Strict-Transport-Security"),
                    "Should not contain Strict-Transport-Security header on localhost");
            }
        }

        [Fact]
        public async Task HttpRequest_WithCustomHeaderPolicy_SetsCustomHeader()
        {
            // Arrange
            var hostBuilder = new WebHostBuilder()
                .Configure(app =>
                           {
                               app.UseSecurityHeaders(
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
        public async Task HttpRequest_WithCustomHeaderPolicyUsingConfigureAction_SetsCustomHeader()
        {
            // Arrange
            var hostBuilder = new WebHostBuilder()
                .Configure(app =>
                           {
                               app.UseSecurityHeaders(policies => policies.AddCustomHeader("X-My-Test-Header", "Header value"));
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
                .Configure(app =>
                           {
                               app.UseSecurityHeaders(
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
                .Configure(app =>
                {
                    app.UseSecurityHeaders(
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
        public async Task HttpRequest_WithCspHeaderWithNonce_ReturnsNonce()
        {
            // Arrange
            var hostBuilder = new WebHostBuilder()
                .Configure(app =>
                {
                    app.UseSecurityHeaders(
                        new HeaderPolicyCollection()
                            .AddContentSecurityPolicy(builder =>
                            {
                                builder.AddScriptSrc().WithNonce();
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
                header.Should().StartWith("script-src 'nonce-");
            }
        }

        [Fact]
        public async Task HttpRequest_WithCspHeaderWithNonce_ReturnsDifferentNonceEachRequest()
        {
            // Arrange
            var hostBuilder = new WebHostBuilder()
                .Configure(app =>
                {
                    app.UseSecurityHeaders(
                        new HeaderPolicyCollection()
                            .AddContentSecurityPolicy(builder =>
                            {
                                builder.AddScriptSrc().WithNonce();
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
                var response1 = await server.CreateRequest("/").SendAsync("PUT");
                var response2 = await server.CreateRequest("/").SendAsync("PUT");

                // Assert
                response1.EnsureSuccessStatusCode();
                response2.EnsureSuccessStatusCode();


                (await response1.Content.ReadAsStringAsync()).Should().Be("Test response");
                (await response2.Content.ReadAsStringAsync()).Should().Be("Test response");
                var header1 = response1.Headers.GetValues("Content-Security-Policy").FirstOrDefault();
                var header2 = response2.Headers.GetValues("Content-Security-Policy").FirstOrDefault();
                header1.Should().NotBeNull();
                header2.Should().NotBeNull();

                header1.Should().NotBe(header2);
            }
        }

        [Fact]
        public async Task HttpRequest_WithCspHeaderUsingReportOnly_SetsCspReportOnly()
        {
            // Arrange
            var hostBuilder = new WebHostBuilder()
                .Configure(app =>
                {
                    app.UseSecurityHeaders(
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
                .Configure(app =>
                {
                    app.UseSecurityHeaders(
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

        [Fact]
        public async Task HttpRequest_WithCspHeaderAndNonHtmlContentType_DoesNotSetCspHeader()
        {
            // Arrange
            var hostBuilder = new WebHostBuilder()
                .Configure(app =>
                {
                    app.UseSecurityHeaders(
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
                .Configure(app =>
                {
                    app.UseSecurityHeaders(
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
        public async Task HttpRequest_UsingConfigExtensionMethod_SetsCustomHeader()
        {
            // Arrange
            var hostBuilder = new WebHostBuilder()
                .Configure(app =>
                {
                    app.UseSecurityHeaders(policies => policies.AddCustomHeader("X-My-Test-Header", "Header value"));
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
        public async Task HttpRequest_WithFeaturePolicyHeader_SetsFeaturePolicy()
        {
            // Arrange
            var hostBuilder = new WebHostBuilder()
                .Configure(app =>
                {
                    app.UseSecurityHeaders(
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
        public async Task HttpRequest_WithFeaturePolicyHeaderAndNotHtml_DoesNotSetFeaturePolicy()
        {
            // Arrange
            var hostBuilder = new WebHostBuilder()
                .Configure(app =>
                {
                    app.UseSecurityHeaders(
                        new HeaderPolicyCollection()
                            .AddFeaturePolicy(builder =>
                            {
                                builder.AddFullscreen().Self();
                                builder.AddGeolocation().None();
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
                response.Headers.Contains("Feature-Policy").Should().BeFalse();
            }
        }

        [Fact]
        public async Task HttpRequest_WithFeaturePolicyHeaderAndUnknonwnContentType_SetsFeaturePolicyHeader()
        {
            // Arrange
            var hostBuilder = new WebHostBuilder()
                .Configure(app =>
                {
                    app.UseSecurityHeaders(
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

        [Fact]
        public async Task HttpRequest_WithPermissionsPolicyHeader_SetsPermissionsPolicy()
        {
            // Arrange
            var hostBuilder = new WebHostBuilder()
                .Configure(app =>
                {
                    app.UseSecurityHeaders(
                        new HeaderPolicyCollection()
                            .AddPermissionsPolicy(builder =>
                            {
                                builder.AddAccelerometer().Self().For("https://testurl1.com").For("https://testurl2.com").For("https://testurl3.com").For("https://testurl4.com");
                                builder.AddFullscreen().Self();
                                builder.AddAmbientLightSensor().For("https://testurl.com");
                                builder.AddGeolocation().None();
                                builder.AddCamera().All();
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
                var header = response.Headers.GetValues("Permissions-Policy").FirstOrDefault();
                header.Should().NotBeNull();
                header.Should().Be("accelerometer=(self \"https://testurl1.com\" \"https://testurl2.com\" \"https://testurl3.com\" \"https://testurl4.com\"), fullscreen=self, ambient-light-sensor=\"https://testurl.com\", geolocation=(), camera=*");
            }
        }

        [Fact]
        public async Task HttpRequest_WithPermissionsPolicyHeaderAndNotHtml_DoesNotSetPermissionsPolicy()
        {
            // Arrange
            var hostBuilder = new WebHostBuilder()
                .Configure(app =>
                {
                    app.UseSecurityHeaders(
                        new HeaderPolicyCollection()
                            .AddPermissionsPolicy(builder =>
                            {
                                builder.AddFullscreen().Self();
                                builder.AddGeolocation().None();
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
                response.Headers.Contains("Permissions-Policy").Should().BeFalse();
            }
        }


        [Fact]
        public async Task HttpRequest_WithPermissionsPolicyHeaderAndUnknonwnContentType_SetsPermissionsPolicyHeader()
        {
            // Arrange
            var hostBuilder = new WebHostBuilder()
                .Configure(app =>
                {
                    app.UseSecurityHeaders(
                        new HeaderPolicyCollection()
                            .AddPermissionsPolicy(builder =>
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
                var header = response.Headers.GetValues("Permissions-Policy").FirstOrDefault();
                header.Should().NotBeNull();
                header.Should().Be("fullscreen=self, geolocation=()");
            }
        }

        [Fact]
        public async Task SecureRequest_WithStrictTransportSecurity_WithNoSubdomains_IsCorrect()
        {
            const int maxAge = 123;
            // Arrange
            var hostBuilder = new WebHostBuilder()
                .UseUrls("https://example.com:5001")
                .Configure(app =>
                {
                    app.UseSecurityHeaders(p => p.AddStrictTransportSecurityMaxAge(maxAge));
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
                server.BaseAddress = new Uri("https://example.com:5001");
                var response = await server.CreateRequest("/").SendAsync("PUT");

                // Assert
                response.EnsureSuccessStatusCode();

                (await response.Content.ReadAsStringAsync()).Should().Be("Test response");
                var header = response.Headers.GetValues("Strict-Transport-Security").FirstOrDefault();
                header.Should().Be($"max-age={maxAge}");
            }
        }

        [Fact]
        public async Task SecureRequest_WithStrictTransportSecurity_AndSubdomains_IsCorrect()
        {
            const int maxAge = 123;
            // Arrange
            var hostBuilder = new WebHostBuilder()
                .UseUrls("https://example.com:5001")
                .Configure(app =>
                {
                    app.UseSecurityHeaders(p => p.AddStrictTransportSecurityMaxAgeIncludeSubDomains(maxAge));
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
                server.BaseAddress = new Uri("https://example.com:5001");
                var response = await server.CreateRequest("/").SendAsync("PUT");

                // Assert
                response.EnsureSuccessStatusCode();

                (await response.Content.ReadAsStringAsync()).Should().Be("Test response");
                var header = response.Headers.GetValues("Strict-Transport-Security").FirstOrDefault();
                header.Should().Be($"max-age={maxAge}; includeSubDomains");
            }
        }

        [Fact]
        public async Task SecureRequest_WithStrictTransportSecurity_AndPreload_IsCorrect()
        {
            const int maxAge = 123;
            // Arrange
            var hostBuilder = new WebHostBuilder()
                .UseUrls("https://example.com:5001")
                .Configure(app =>
                {
                    app.UseSecurityHeaders(p => p.AddStrictTransportSecurityMaxAgeIncludeSubDomainsAndPreload(maxAge));
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
                server.BaseAddress = new Uri("https://example.com:5001");
                var response = await server.CreateRequest("/").SendAsync("PUT");

                // Assert
                response.EnsureSuccessStatusCode();

                (await response.Content.ReadAsStringAsync()).Should().Be("Test response");
                var header = response.Headers.GetValues("Strict-Transport-Security").FirstOrDefault();
                header.Should().Be($"max-age={maxAge}; includeSubDomains; preload");
            }
        }

        [Fact]
        public async Task SecureRequest_WithStrictTransportSecurity_WhenUsingTheManualValues_IsCorrect()
        {
            const int maxAge = 123;
            // Arrange
            var hostBuilder = new WebHostBuilder()
                .UseUrls("https://example.com:5001")
                .Configure(app =>
                {
                    app.UseSecurityHeaders(p => p.AddStrictTransportSecurity(maxAge, includeSubdomains: true, preload: false));
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
                server.BaseAddress = new Uri("https://example.com:5001");
                var response = await server.CreateRequest("/").SendAsync("PUT");

                // Assert
                response.EnsureSuccessStatusCode();

                (await response.Content.ReadAsStringAsync()).Should().Be("Test response");
                var header = response.Headers.GetValues("Strict-Transport-Security").FirstOrDefault();
                header.Should().Be($"max-age={maxAge}; includeSubDomains");
            }
        }

        [Fact]
        public async Task HttpRequest_WithExpectCt_DoesNotSetExpectCT()
        {
            // Arrange
            const int maxAge = 123;
            var hostBuilder = new WebHostBuilder()
                .Configure(app =>
                {
                    app.UseSecurityHeaders(p => p.AddExpectCTEnforceOnly(maxAge));
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
                response.Headers.Contains("Expect-CT").Should().BeFalse();
            }
        }

        [Fact]
        public async Task SecureRequest_WhenOnLocalhost_DoesNotSetExpectCtHeader()
        {
            // Arrange
            const int maxAge = 123;
            var hostBuilder = new WebHostBuilder()
                .UseUrls("https://localhost:5001")
                .Configure(app =>
                {
                    app.UseSecurityHeaders(p => p.AddExpectCTEnforceOnly(maxAge));
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
                Assert.False(response.Headers.Contains("Expect-CT"),
                    "Should not contain Expect-CT header on localhost");
            }
        }

        [Fact]
        public async Task SecureRequest_WithExpectCt_EnforceOnly_IsCorrect()
        {
            const int maxAge = 123;
            // Arrange
            var hostBuilder = new WebHostBuilder()
                .UseUrls("https://example.com:5001")
                .Configure(app =>
                {
                    app.UseSecurityHeaders(p => p.AddExpectCTEnforceOnly(maxAge));
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
                server.BaseAddress = new Uri("https://example.com:5001");
                var response = await server.CreateRequest("/").SendAsync("PUT");

                // Assert
                response.EnsureSuccessStatusCode();

                (await response.Content.ReadAsStringAsync()).Should().Be("Test response");
                var header = response.Headers.GetValues("Expect-CT").FirstOrDefault();
                header.Should().Be($"max-age={maxAge}, enforce");
            }
        }

        [Fact]
        public async Task SecureRequest_WithExpectCt_IsCorrect()
        {
            const int maxAge = 123;
            // Arrange
            var hostBuilder = new WebHostBuilder()
                .UseUrls("https://example.com:5001")
                .Configure(app =>
                {
                    app.UseSecurityHeaders(p => p.AddExpectCTNoEnforceOrReport(maxAge));
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
                server.BaseAddress = new Uri("https://example.com:5001");
                var response = await server.CreateRequest("/").SendAsync("PUT");

                // Assert
                response.EnsureSuccessStatusCode();

                (await response.Content.ReadAsStringAsync()).Should().Be("Test response");
                var header = response.Headers.GetValues("Expect-CT").FirstOrDefault();
                header.Should().Be($"max-age={maxAge}");
            }
        }

        [Fact]
        public async Task SecureRequest_WithExpectCt_ReportOnly_IsCorrect()
        {
            const int maxAge = 123;
            const string reportUri = "http://test.com";
            // Arrange
            var hostBuilder = new WebHostBuilder()
                .UseUrls("https://example.com:5001")
                .Configure(app =>
                {
                    app.UseSecurityHeaders(p => p.AddExpectCTReportOnly(maxAge, reportUri));
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
                server.BaseAddress = new Uri("https://example.com:5001");
                var response = await server.CreateRequest("/").SendAsync("PUT");

                // Assert
                response.EnsureSuccessStatusCode();

                (await response.Content.ReadAsStringAsync()).Should().Be("Test response");
                var header = response.Headers.GetValues("Expect-CT").FirstOrDefault();
                header.Should().Be($"max-age={maxAge}, report-uri=\"{reportUri}\"");
            }
        }

        [Fact]
        public async Task SecureRequest_WithExpectCt_EnforceAndReport_IsCorrect()
        {
            const int maxAge = 123;
            const string reportUri = "http://test.com";
            // Arrange
            var hostBuilder = new WebHostBuilder()
                .UseUrls("https://example.com:5001")
                .Configure(app =>
                {
                    app.UseSecurityHeaders(p => p.AddExpectCTEnforceAndReport(maxAge, reportUri));
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
                server.BaseAddress = new Uri("https://example.com:5001");
                var response = await server.CreateRequest("/").SendAsync("PUT");

                // Assert
                response.EnsureSuccessStatusCode();

                (await response.Content.ReadAsStringAsync()).Should().Be("Test response");
                var header = response.Headers.GetValues("Expect-CT").FirstOrDefault();
                header.Should().Be($"max-age={maxAge}, enforce, report-uri=\"{reportUri}\"");
            }
        }

        [Fact]
        public async Task SecureRequest_WithExpectCt_WhenUsingTheManualValues_IsCorrect()
        {
            const int maxAge = 123;
            const string reportUri = "http://test.com";
            // Arrange
            var hostBuilder = new WebHostBuilder()
                .UseUrls("https://example.com:5001")
                .Configure(app =>
                {
                    app.UseSecurityHeaders(p => p.AddExpectCT(maxAge, enforce: true, reportUri: reportUri));
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
                server.BaseAddress = new Uri("https://example.com:5001");
                var response = await server.CreateRequest("/").SendAsync("PUT");

                // Assert
                response.EnsureSuccessStatusCode();

                (await response.Content.ReadAsStringAsync()).Should().Be("Test response");
                var header = response.Headers.GetValues("Expect-CT").FirstOrDefault();
                header.Should().Be($"max-age={maxAge}, enforce, report-uri=\"{reportUri}\"");
            }
        }

        private static void AssertHttpRequestDefaultSecurityHeaders(HttpResponseHeaders headers)
        {
            string header = headers.GetValues("X-Content-Type-Options").FirstOrDefault();
            header.Should().Be("nosniff");
            header = headers.GetValues("X-Frame-Options").FirstOrDefault();
            header.Should().Be("DENY");
            header = headers.GetValues("X-XSS-Protection").FirstOrDefault();
            header.Should().Be("1; mode=block");
            header = headers.GetValues("Referrer-Policy").FirstOrDefault();
            header.Should().Be("strict-origin-when-cross-origin");
            header = headers.GetValues("Content-Security-Policy").FirstOrDefault();
            header.Should().Be("object-src 'none'; form-action 'self'; frame-ancestors 'none'");

            Assert.False(headers.Contains("Server"),
                "Should not contain server header");
            Assert.False(headers.Contains("Strict-Transport-Security"),
                "Should not contain Strict-Transport-Security header over http");
        }

        private static void AssertSecureRequestDefaultSecurityHeaders(HttpResponseHeaders headers)
        {
            string header = headers.GetValues("X-Content-Type-Options").FirstOrDefault();
            header.Should().Be("nosniff");
            header = headers.GetValues("X-Frame-Options").FirstOrDefault();
            header.Should().Be("DENY");
            header = headers.GetValues("X-XSS-Protection").FirstOrDefault();
            header.Should().Be("1; mode=block");
            header = headers.GetValues("Strict-Transport-Security").FirstOrDefault();
            header.Should().Be($"max-age={StrictTransportSecurityHeader.OneYearInSeconds}");
            header = headers.GetValues("Referrer-Policy").FirstOrDefault();
            header.Should().Be("strict-origin-when-cross-origin");
            header = headers.GetValues("Content-Security-Policy").FirstOrDefault();
            header.Should().Be("object-src 'none'; form-action 'self'; frame-ancestors 'none'");

            Assert.False(headers.Contains("Server"),
                "Should not contain server header");
        }
    }
}
