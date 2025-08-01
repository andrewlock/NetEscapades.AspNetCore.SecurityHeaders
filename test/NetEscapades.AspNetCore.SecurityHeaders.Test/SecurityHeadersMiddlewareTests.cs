﻿using System;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using FluentAssertions;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.DependencyInjection;
using Xunit;

namespace NetEscapades.AspNetCore.SecurityHeaders.Test;

#pragma warning disable CS0618 // Type or member is obsolete
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
            response.Headers.AssertHttpRequestDefaultSecurityHeaders();
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
            response.Headers.AssertHttpRequestDefaultSecurityHeaders();
        }
    }

    [Fact]
    public async Task HttpRequest_WithDefaultSecurityHeaders_WithNoExtraConfiguration_SetsSecurityHeaders()
    {
        // Arrange
        var hostBuilder = new WebHostBuilder()
            .Configure(app =>
            {
                app.UseSecurityHeaders();
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
            response.Headers.AssertHttpRequestDefaultSecurityHeaders();
        }
    }
    
    [Fact]
    public async Task HttpRequest_WithDefaultSecurityHeaders_WithNamedPolicy_SetsSecurityHeaders()
    {
        var policyName = "default";

        // Arrange
        var hostBuilder = new WebHostBuilder()
            .ConfigureServices(s => s.AddSecurityHeaderPolicies()
                .AddPolicy(policyName, p => p.AddCustomHeader("Custom-Header", "MyValue")))
            .Configure(app =>
            {
                app.UseSecurityHeaders(policyName);
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
            response.Headers.Should().NotContainKey("X-Frame-Options");
            response.Headers.Should().ContainKey("Custom-Header").WhoseValue.Should().ContainSingle("MyValue");
        }
    }
    
    [Fact]
    public void HttpRequest_WithDefaultSecurityHeaders_WithUnknownNamedPolicy_ThrowsException()
    {
        // Arrange
        var hostBuilder = new WebHostBuilder()
            .Configure(app =>
            {
                app.UseSecurityHeaders("Unknown name");
                app.Run(async context =>
                {
                    context.Response.ContentType = "text/html";
                    await context.Response.WriteAsync("Test response");
                });
            });

        Func<TestServer> act = () => new TestServer(hostBuilder); 
        act.Should().Throw<InvalidOperationException>();
    }

    [Fact]
    public async Task HttpRequest_WithEndpointSecurityHeaders_WhenNoEndpoints_SetsDefaultHeaders()
    {
        var policyName = "custom";

        // Arrange
        var hostBuilder = new WebHostBuilder()
            .ConfigureServices(s =>
            {
                s.AddRouting();
                s.AddSecurityHeaderPolicies()
                    .AddPolicy(policyName, p => p.AddCustomHeader("Custom-Header", "MyValue"));
            })
            .Configure(app =>
            {
                app.UseSecurityHeaders();
                app.UseRouting();
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
            response.Headers.AssertHttpRequestDefaultSecurityHeaders();
        }
    }

    [Fact]
    public async Task HttpRequest_WithEndpointSecurityHeaders_WhenEndpointsHasNoMetadata_SetsDefaultHeaders()
    {
        var policyName = "custom";

        // Arrange
        var hostBuilder = new WebHostBuilder()
            .ConfigureServices(s =>
            {
                s.AddRouting();
                s.AddSecurityHeaderPolicies()
                    .AddPolicy(policyName, p => p.AddCustomHeader("Custom-Header", "MyValue"));
            })
            .Configure(app =>
            {
                app.UseSecurityHeaders();
                app.UseRouting();
                app.UseEndpoints(endpoints =>
                {
                    endpoints.MapPut("/", async context =>
                    {
                        context.Response.ContentType = "text/html";
                        await context.Response.WriteAsync("Test response");
                    });
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
            response.Headers.AssertHttpRequestDefaultSecurityHeaders();
        }
    }

    [Fact]
    public async Task HttpRequest_WithEndpointSecurityHeaders_WhenEndpointsHasMetadata_SetsCustomHeaders()
    {
        var policyName = "custom";

        // Arrange
        var hostBuilder = new WebHostBuilder()
            .ConfigureServices(s =>
            {
                s.AddRouting();
                s.AddSecurityHeaderPolicies()
                    .AddPolicy(policyName, p => p.AddCustomHeader("Custom-Header", "MyValue"));
            })
            .Configure(app =>
            {
                app.UseSecurityHeaders();
                app.UseRouting();
                app.UseEndpoints(endpoints =>
                {
                    endpoints.MapPut("/", async context =>
                    {
                        context.Response.ContentType = "text/html";
                        await context.Response.WriteAsync("Test response");
                    }).WithSecurityHeadersPolicy(policyName);
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
            response.Headers.Should().NotContainKey("X-Frame-Options");
            response.Headers.Should().ContainKey("Custom-Header").WhoseValue.Should().ContainSingle("MyValue");
        }
    }

    [Fact]
    public async Task HttpRequest_WithDefaultSecurityHeaders_WithConfiguredDefaultPolicy_SetsCustomHeaders()
    {
        // Arrange
        var hostBuilder = new WebHostBuilder()
            .ConfigureServices(s => s.AddSecurityHeaderPolicies()
                .SetDefaultPolicy(p => p.AddCustomHeader("Custom-Value", "MyValue")))
            .Configure(app =>
            {
                app.UseSecurityHeaders();
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
            response.Headers.TryGetValues("X-Frame-Options", out _).Should().BeFalse();
            response.Headers.TryGetValues("Custom-Value", out var h).Should().BeTrue();
            h.Should().ContainSingle("MyValue");
        }
    }

    [Fact]
    public async Task HttpRequest_WithCustomDefaultPolicy_UsesCustomPolicy()
    {
        // Arrange
        var hostBuilder = new WebHostBuilder()
            .ConfigureServices(s => s.AddSecurityHeaderPolicies()
                .SetPolicySelector(ctx =>
                    ctx.HttpContext.Request.Headers.ContainsKey("AddTo-Default")
                        ? ctx.DefaultPolicy.Copy().AddCustomHeader("Added-Header", "MyValue")
                        : ctx.DefaultPolicy))
            .Configure(app =>
            {
                app.UseSecurityHeaders();
                app.Run(async context =>
                {
                    context.Response.ContentType = "text/html";
                    await context.Response.WriteAsync("Test response");
                });
            });

        using (var server = new TestServer(hostBuilder))
        {
            // Act
            // Add header
            var response = await server.CreateRequest("/")
                .AddHeader("AddTo-Default", "Something")
                .SendAsync("PUT");

            // Assert
            response.EnsureSuccessStatusCode();

            (await response.Content.ReadAsStringAsync()).Should().Be("Test response");
            response.Headers.AssertHttpRequestDefaultSecurityHeaders();
            response.Headers.Should().ContainKey("Added-Header").WhoseValue.Should().Contain("MyValue");
            
            
            // No header
            response = await server.CreateRequest("/")
                .SendAsync("PUT");

            // Assert
            response.EnsureSuccessStatusCode();

            (await response.Content.ReadAsStringAsync()).Should().Be("Test response");
            response.Headers.AssertHttpRequestDefaultSecurityHeaders();
            response.Headers.Should().NotContainKey("Added-Header");
            
            
        }
    }

    [Fact]
    public async Task HttpRequest_WithCustomEndpointPolicy_WhenCustomEndpointIsSelected_UsesCustomPolicy()
    {
        var policyName = "custom";

        // Arrange
        var hostBuilder = new WebHostBuilder()
            .ConfigureServices(s =>
            {
                s.AddRouting();
                s.AddSecurityHeaderPolicies()
                    .AddPolicy(policyName, p => p.AddCustomHeader("Custom-Header", "MyValue"))
                    .SetPolicySelector(ctx =>
                        ctx.SelectedPolicy.Copy().AddCustomHeader("Added-Header", "MyValue"));
            })
            .Configure(app =>
            {
                app.UseSecurityHeaders();
                app.UseRouting();
                app.UseEndpoints(endpoints =>
                {
                    endpoints.MapPut("/", async context =>
                    {
                        context.Response.ContentType = "text/html";
                        await context.Response.WriteAsync("Test response");
                    }).WithSecurityHeadersPolicy(policyName);
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
            response.Headers.Should().NotContainKey("X-Frame-Options");
            response.Headers.Should().ContainKey("Custom-Header").WhoseValue.Should().ContainSingle("MyValue");
            response.Headers.Should().ContainKey("Added-Header").WhoseValue.Should().ContainSingle("MyValue");
        }
    }

    [Fact]
    public async Task HttpRequest_WithCustomEndpointPolicy_WhenNoEndpointIsSelected_UsesCustomPolicyBasedOnDefault()
    {
        var policyName = "custom";

        // Arrange
        var hostBuilder = new WebHostBuilder()
            .ConfigureServices(s =>
            {
                s.AddRouting();
                s.AddSecurityHeaderPolicies()
                    .AddPolicy(policyName, p => p.AddCustomHeader("Custom-Header", "MyValue"))
                    .SetPolicySelector(ctx =>
                        ctx.SelectedPolicy.Copy().AddCustomHeader("Added-Header", "MyValue"));
            })
            .Configure(app =>
            {
                app.UseSecurityHeaders();
                app.UseRouting();
                app.UseEndpoints(endpoints =>
                {
                    endpoints.MapPut("/", async context =>
                    {
                        context.Response.ContentType = "text/html";
                        await context.Response.WriteAsync("Test response");
                    });
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
            response.Headers.AssertHttpRequestDefaultSecurityHeaders();
            response.Headers.Should().ContainKey("Added-Header");
            response.Headers.Should().NotContainKey("Custom-Header");
        }
    }

    [Fact]
    public async Task HttpRequest_WithCustomDefaultPolicy_WhenItReturnsNull_ThrowsInvalidOperationException()
    {
        // Arrange
        var hostBuilder = new WebHostBuilder()
            .ConfigureServices(s => s.AddSecurityHeaderPolicies()
                .SetPolicySelector(ctx => null!))
            .Configure(app =>
            {
                app.UseSecurityHeaders();
                app.Run(async context =>
                {
                    context.Response.ContentType = "text/html";
                    await context.Response.WriteAsync("Test response");
                });
            });

        using (var server = new TestServer(hostBuilder))
        {
            // Act
            // Add header
            var act = async () => await server.CreateRequest("/").SendAsync("PUT");
            await act.Should().ThrowAsync<InvalidOperationException>();
        }
    }

    [Fact]
    public async Task HttpRequest_WithCustomDefaultPolicy_WhenUsingService_UsesCollection()
    {
        // Arrange
        var hostBuilder = new WebHostBuilder()
            .ConfigureServices(s =>
            {
                s.AddScoped<HeaderPolicyCollectionFactory>();
                s.AddSecurityHeaderPolicies()
                    .SetPolicySelector(ctx =>
                    {
                        var httpContext = ctx.HttpContext;
                        var service = httpContext.RequestServices.GetRequiredService<HeaderPolicyCollectionFactory>();
                        var tenantId = httpContext.Request.Headers["Tenant-ID"];
                        return service.GetPolicy(tenantId);
                    });
            })
            .Configure(app =>
            {
                app.UseSecurityHeaders();
                app.Run(async context =>
                {
                    context.Response.ContentType = "text/html";
                    await context.Response.WriteAsync("Test response");
                });
            });

        using (var server = new TestServer(hostBuilder))
        {
            // Act
            // Add header
            var response = await server.CreateRequest("/")
                .AddHeader("Tenant-ID", "Default")
                .SendAsync("PUT");
            response.EnsureSuccessStatusCode();
            response.Headers.Should().ContainKey("Custom-Header").WhoseValue.Should().Contain("Default");

            response = await server.CreateRequest("/")
                .AddHeader("Tenant-ID", "1234")
                .SendAsync("PUT");
            response.EnsureSuccessStatusCode();
            response.Headers.Should().ContainKey("Custom-Header").WhoseValue.Should().Contain("Custom");
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
            response.Headers.AssertSecureRequestDefaultSecurityHeaders();
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
            response.Headers.Contains("Strict-Transport-Security").Should().BeFalse("Should not contain Strict-Transport-Security header on localhost");
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

    [Theory]
    [InlineData("text/html")]
    [InlineData("application/json")]
    [InlineData("")]
    public async Task HttpRequest_WithCspHeader_SetsHeader_RegardlessOfContentType(string requestContentType)
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
                    if (!string.IsNullOrEmpty(requestContentType))
                    {
                        context.Response.ContentType = requestContentType;
                    }

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
            response.Headers.Contains("Content-Security-Policy").Should().BeTrue();
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

    [Theory]
    [InlineData("text/html")]
    [InlineData("application/javascript")]
    [InlineData("")]
    [InlineData(null)]
    [InlineData("application/json")]
    [InlineData("foo/bar")]
    public async Task HttpRequest_WithCspHeaderAndDefaultContentTypes_SetsCspHeader(string requestContentType)
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
                    context.Response.ContentType = requestContentType;
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
            response.Headers
                .Should().ContainKey("Feature-Policy")
                .WhoseValue.Should().ContainSingle("fullscreen 'self'; geolocation 'none'");
        }
    }

    [Fact]
    public async Task HttpRequest_WithFeaturePolicyHeaderAndNotHtml_SetsFeaturePolicy()
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
            response.Headers
                .Should().ContainKey("Feature-Policy")
                .WhoseValue.Should().ContainSingle("fullscreen 'self'; geolocation 'none'");
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
            response.Headers
                .Should().ContainKey("Feature-Policy")
                .WhoseValue.Should().ContainSingle("fullscreen 'self'; geolocation 'none'");
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
            response.Headers
                .Should().ContainKey("Permissions-Policy")
                .WhoseValue.Should().ContainSingle("accelerometer=(self \"https://testurl1.com\" \"https://testurl2.com\" \"https://testurl3.com\" \"https://testurl4.com\"), fullscreen=self, ambient-light-sensor=\"https://testurl.com\", geolocation=(), camera=*\"");
        }
    }

    [Fact]
    public async Task HttpRequest_WithPermissionsPolicyHeaderAndNotHtml_SetsPermissionsPolicy()
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
            response.Headers
                .Should().ContainKey("Permissions-Policy")
                .WhoseValue.Should().ContainSingle("accelerometer=(self \"https://testurl1.com\" \"https://testurl2.com\" \"https://testurl3.com\" \"https://testurl4.com\"), fullscreen=self, ambient-light-sensor=\"https://testurl.com\", geolocation=(), camera=*\"");
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
            response.Headers.Contains("Expect-CT").Should().BeFalse("Should not contain Expect-CT header on localhost");
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

    [Fact]
    public async Task HttpRequest_WithCrossOriginOpenerPolicyHeader_SetsUnsafeNone()
    {
        // Arrange
        var hostBuilder = new WebHostBuilder()
            .Configure(app =>
            {
                app.UseSecurityHeaders(
                    new HeaderPolicyCollection()
                        .AddCrossOriginOpenerPolicy(builder =>
                        {
                            builder.UnsafeNone();
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
            var header = response.Headers.GetValues("Cross-Origin-Opener-Policy").FirstOrDefault();
            header.Should().NotBeNull();
            header.Should().Be("unsafe-none");
            response.Headers.Contains("Cross-Origin-Opener-Policy-Report-Only").Should().BeFalse();
        }
    }

    [Fact]
    public async Task HttpRequest_WithCrossOriginOpenerPolicyHeader_SetsUnsafeNone_WithReportingEndpoint()
    {
        // Arrange
        var hostBuilder = new WebHostBuilder()
            .Configure(app =>
            {
                app.UseSecurityHeaders(
                    new HeaderPolicyCollection()
                        .AddCrossOriginOpenerPolicy(builder =>
                        {
                            builder.UnsafeNone();
                            builder.AddReport().To("coop_endpoint");
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
            var header = response.Headers.GetValues("Cross-Origin-Opener-Policy").FirstOrDefault();
            header.Should().NotBeNull();
            header.Should().Be("unsafe-none; report-to=\"coop_endpoint\"");
            response.Headers.Contains("Cross-Origin-Opener-Policy-Report-Only").Should().BeFalse();
        }
    }

    [Fact]
    public async Task HttpRequest_WithCrossOriginOpenerPolicyHeader_SetsSameOrigin()
    {
        // Arrange
        var hostBuilder = new WebHostBuilder()
            .Configure(app =>
            {
                app.UseSecurityHeaders(
                    new HeaderPolicyCollection()
                        .AddCrossOriginOpenerPolicy(builder =>
                        {
                            builder.SameOrigin();
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
            var header = response.Headers.GetValues("Cross-Origin-Opener-Policy").FirstOrDefault();
            header.Should().NotBeNull();
            header.Should().Be("same-origin");
            response.Headers.Contains("Cross-Origin-Opener-Policy-Report-Only").Should().BeFalse();
        }
    }

    [Fact]
    public async Task HttpRequest_WithCrossOriginOpenerPolicyHeader_SetsSameOriginAllowPopups()
    {
        // Arrange
        var hostBuilder = new WebHostBuilder()
            .Configure(app =>
            {
                app.UseSecurityHeaders(
                    new HeaderPolicyCollection()
                        .AddCrossOriginOpenerPolicy(builder =>
                        {
                            builder.SameOriginAllowPopups();
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
            var header = response.Headers.GetValues("Cross-Origin-Opener-Policy").FirstOrDefault();
            header.Should().NotBeNull();
            header.Should().Be("same-origin-allow-popups");
            response.Headers.Contains("Cross-Origin-Opener-Policy-Report-Only").Should().BeFalse();
        }
    }

    [Fact]
    public async Task HttpRequest_WithCrossOriginEmbedderPolicyHeader_SetsUnsafeNone()
    {
        // Arrange
        var hostBuilder = new WebHostBuilder()
            .Configure(app =>
            {
                app.UseSecurityHeaders(
                    new HeaderPolicyCollection()
                        .AddCrossOriginEmbedderPolicy(builder =>
                        {
                            builder.UnsafeNone();
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
            var header = response.Headers.GetValues("Cross-Origin-Embedder-Policy").FirstOrDefault();
            header.Should().NotBeNull();
            header.Should().Be("unsafe-none");
            response.Headers.Contains("Cross-Origin-Embedder-Policy-Report-Only").Should().BeFalse();
        }
    }

    [Fact]
    public async Task HttpRequest_WithCrossOriginEmbedderPolicyHeader_SetsRequireCorp()
    {
        // Arrange
        var hostBuilder = new WebHostBuilder()
            .Configure(app =>
            {
                app.UseSecurityHeaders(
                    new HeaderPolicyCollection()
                        .AddCrossOriginEmbedderPolicy(builder =>
                        {
                            builder.RequireCorp();
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
            var header = response.Headers.GetValues("Cross-Origin-Embedder-Policy").FirstOrDefault();
            header.Should().NotBeNull();
            header.Should().Be("require-corp");
            response.Headers.Contains("Cross-Origin-Embedder-Policy-Report-Only").Should().BeFalse();
        }
    }

    [Fact]
    public async Task HttpRequest_WithCrossOriginResourcePolicyHeader_SetsSameSite()
    {
        // Arrange
        var hostBuilder = new WebHostBuilder()
            .Configure(app =>
            {
                app.UseSecurityHeaders(
                    new HeaderPolicyCollection()
                        .AddCrossOriginResourcePolicy(builder =>
                        {
                            builder.SameSite();
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
            var header = response.Headers.GetValues("Cross-Origin-Resource-Policy").FirstOrDefault();
            header.Should().NotBeNull();
            header.Should().Be("same-site");
            response.Headers.Contains("Cross-Origin-Resource-Policy-Report-Only").Should().BeFalse();
        }
    }

    [Fact]
    public async Task HttpRequest_WithCrossOriginResourcePolicyHeader_SetsSameOrigin()
    {
        // Arrange
        var hostBuilder = new WebHostBuilder()
            .Configure(app =>
            {
                app.UseSecurityHeaders(
                    new HeaderPolicyCollection()
                        .AddCrossOriginResourcePolicy(builder =>
                        {
                            builder.SameOrigin();
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
            var header = response.Headers.GetValues("Cross-Origin-Resource-Policy").FirstOrDefault();
            header.Should().NotBeNull();
            header.Should().Be("same-origin");
            response.Headers.Contains("Cross-Origin-Resource-Policy-Report-Only").Should().BeFalse();
        }
    }

    [Fact]
    public async Task HttpRequest_WithCrossOriginResourcePolicyHeader_SetsCrossOrigin()
    {
        // Arrange
        var hostBuilder = new WebHostBuilder()
            .Configure(app =>
            {
                app.UseSecurityHeaders(
                    new HeaderPolicyCollection()
                        .AddCrossOriginResourcePolicy(builder =>
                        {
                            builder.CrossOrigin();
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
            var header = response.Headers.GetValues("Cross-Origin-Resource-Policy").FirstOrDefault();
            header.Should().NotBeNull();
            header.Should().Be("cross-origin");
            response.Headers.Contains("Cross-Origin-Resource-Policy-Report-Only").Should().BeFalse();
        }
    }

    [Fact]
    public async Task HttpRequest_NonHtml_WithCrossOriginResourcePolicyHeader_SetsHeader()
    {
        // Arrange
        var json = @"{""Key"":""Value""}";
        var hostBuilder = new WebHostBuilder()
            .Configure(app =>
            {
                app.UseSecurityHeaders(
                    new HeaderPolicyCollection()
                        .AddCrossOriginResourcePolicy(builder =>
                        {
                            builder.CrossOrigin();
                        }));
                app.Run(async context =>
                {
                    context.Response.ContentType = "application/json";
                    await context.Response.WriteAsync(json);
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

            (await response.Content.ReadAsStringAsync()).Should().Be(json);
            var header = response.Headers.GetValues("Cross-Origin-Resource-Policy").FirstOrDefault();
            header.Should().NotBeNull();
            header.Should().Be("cross-origin");
            response.Headers.Contains("Cross-Origin-Resource-Policy-Report-Only").Should().BeFalse();
        }
    }

    [Fact]
    public async Task HttpRequest_WithCrossOriginOpenerPolicyHeaderReportOnly_SetsUnsafeNone()
    {
        // Arrange
        var hostBuilder = new WebHostBuilder()
            .Configure(app =>
            {
                app.UseSecurityHeaders(
                    new HeaderPolicyCollection()
                        .AddCrossOriginOpenerPolicyReportOnly(builder =>
                        {
                            builder.UnsafeNone();
                            builder.AddReport().To("coop_endpoint");
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
            var header = response.Headers.GetValues("Cross-Origin-Opener-Policy-Report-Only").FirstOrDefault();
            header.Should().NotBeNull();
            header.Should().Be("unsafe-none; report-to=\"coop_endpoint\"");
            response.Headers.Contains("Cross-Origin-Opener-Policy").Should().BeFalse();
        }
    }

    [Fact]
    public async Task HttpRequest_WithCrossOriginOpenerPolicyHeaderReportOnly_UsingBoolean_SetsUnsafeNone()
    {
        // Arrange
        var hostBuilder = new WebHostBuilder()
            .Configure(app =>
            {
                app.UseSecurityHeaders(
                    new HeaderPolicyCollection()
                        .AddCrossOriginOpenerPolicy(builder =>
                        {
                            builder.UnsafeNone();
                            builder.AddReport().To("coop_endpoint");
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
            var header = response.Headers.GetValues("Cross-Origin-Opener-Policy-Report-Only").FirstOrDefault();
            header.Should().NotBeNull();
            header.Should().Be("unsafe-none; report-to=\"coop_endpoint\"");
            response.Headers.Contains("Cross-Origin-Opener-Policy").Should().BeFalse();
        }
    }

    [Fact]
    public async Task HttpRequest_WithCrossOriginOpenerPolicyHeader_UsingBooleanAsFalse_SetsUnsafeNone()
    {
        // Arrange
        var hostBuilder = new WebHostBuilder()
            .Configure(app =>
            {
                app.UseSecurityHeaders(
                    new HeaderPolicyCollection()
                        .AddCrossOriginOpenerPolicy(builder =>
                        {
                            builder.UnsafeNone();
                            builder.AddReport().To("coop_endpoint");
                        }, asReportOnly: false));
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
            var header = response.Headers.GetValues("Cross-Origin-Opener-Policy").FirstOrDefault();
            header.Should().NotBeNull();
            header.Should().Be("unsafe-none; report-to=\"coop_endpoint\"");
            response.Headers.Contains("Cross-Origin-Opener-Policy-Report-Only").Should().BeFalse();
        }
    }

    [Fact]
    public async Task HttpRequest_WithCrossOriginEmbedderPolicyHeaderReportOnly_SetsUnsafeNone()
    {
        // Arrange
        var hostBuilder = new WebHostBuilder()
            .Configure(app =>
            {
                app.UseSecurityHeaders(
                    new HeaderPolicyCollection()
                        .AddCrossOriginEmbedderPolicyReportOnly(builder =>
                        {
                            builder.UnsafeNone();
                            builder.AddReport().To("coep_endpoint");
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
            var header = response.Headers.GetValues("Cross-Origin-Embedder-Policy-Report-Only").FirstOrDefault();
            header.Should().NotBeNull();
            header.Should().Be("unsafe-none; report-to=\"coep_endpoint\"");
            response.Headers.Contains("Cross-Origin-Embedder-Policy").Should().BeFalse();
        }
    }

    [Fact]
    public async Task HttpRequest_WithCrossOriginEmbedderPolicyHeaderReportOnly_UsingBoolean_SetsUnsafeNone()
    {
        // Arrange
        var hostBuilder = new WebHostBuilder()
            .Configure(app =>
            {
                app.UseSecurityHeaders(
                    new HeaderPolicyCollection()
                        .AddCrossOriginEmbedderPolicy(builder =>
                        {
                            builder.UnsafeNone();
                            builder.AddReport().To("coep_endpoint");
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
            var header = response.Headers.GetValues("Cross-Origin-Embedder-Policy-Report-Only").FirstOrDefault();
            header.Should().NotBeNull();
            header.Should().Be("unsafe-none; report-to=\"coep_endpoint\"");
            response.Headers.Contains("Cross-Origin-Embedder-Policy").Should().BeFalse();
        }
    }

    [Fact]
    public async Task HttpRequest_WithReportingEndpoints_SetsHeader()
    {
        // Arrange
        var hostBuilder = new WebHostBuilder()
            .Configure(app =>
            {
                app.UseSecurityHeaders(
                    new HeaderPolicyCollection()
                        .AddReportingEndpoints(builder =>
                        {
                            builder.AddDefaultEndpoint("https://localhost:5000/default");
                            builder.AddEndpoint("endpoint-1", "http://localhost/endpoint-1");
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
            var header = response.Headers.GetValues("Reporting-Endpoints").SingleOrDefault();
            header.Should().NotBeNull();
            header.Should().Be("default=\"https://localhost:5000/default\", endpoint-1=\"http://localhost/endpoint-1\"");
        }
    }

    [Fact]
    public async Task HttpRequest_CanApplyDifferentPolicyBasedOnResponseContentType()
    {
        // Arrange
        var hostBuilder = new WebHostBuilder()
            .ConfigureServices(services =>
            {
                services.AddRouting();
                services
                    .AddSecurityHeaderPolicies()
                    .AddPolicy("text/html", x => x.AddDefaultSecurityHeaders())
                    .SetDefaultPolicy(x => x.AddDefaultApiSecurityHeaders())
                    .SetPolicySelector(ctx =>
                        ctx.HttpContext.Response.ContentType == "text/html"
                        && ctx.ConfiguredPolicies.TryGetValue("text/html", out var htmlPolicy)
                            ? htmlPolicy
                            : ctx.SelectedPolicy);
            })
            .Configure(app =>
            {
                app.UseSecurityHeaders();
                app.UseRouting();
                app.UseEndpoints(endpoints =>
                {
                    endpoints.MapGet("/api", context =>
                    {
                        context.Response.ContentType = "text/plain";
                        return context.Response.WriteAsync("Test response");
                    });
                    endpoints.MapGet("/html", context =>
                    {
                        context.Response.ContentType = "text/html";
                        return context.Response.WriteAsync("Test response");
                    });
                });
            });

        using var server = new TestServer(hostBuilder);
        
        // API request.
        var response = await server.CreateRequest("/api")
            .SendAsync("GET");

        response.EnsureSuccessStatusCode();

        (await response.Content.ReadAsStringAsync()).Should().Be("Test response");
        response.Headers.AssertHttpRequestDefaultApiSecurityHeaders();
        
        // HTML request.
        response = await server.CreateRequest("/html")
            .SendAsync("GET");

        response.EnsureSuccessStatusCode();

        (await response.Content.ReadAsStringAsync()).Should().Be("Test response");
        response.Headers.AssertHttpRequestDefaultSecurityHeaders();
    }

    [Fact]
    public async Task HttpRequest_CanUseProviderToConfigurePolicies()
    {
        // Arrange
        var hostBuilder = new WebHostBuilder()
            .ConfigureServices(s =>
            {
                s.AddSingleton<HeaderPolicyCollectionFactory>();
                s.AddSecurityHeaderPolicies((builder, provider) =>
                    {
                        var service = provider.GetRequiredService<HeaderPolicyCollectionFactory>();
                        builder.SetDefaultPolicy(service.GetPolicy(null));
                    });
            })
            .Configure(app =>
            {
                app.UseSecurityHeaders();
                app.Run(async context =>
                {
                    context.Response.ContentType = "text/html";
                    await context.Response.WriteAsync("Test response");
                });
            });

        using (var server = new TestServer(hostBuilder))
        {
            // Act
            // Add header
            var response = await server.CreateRequest("/")
                .SendAsync("PUT");
            response.EnsureSuccessStatusCode();
            response.Headers.Should().ContainKey("Custom-Header").WhoseValue.Should().Contain("Default");
        }
    }

    private class HeaderPolicyCollectionFactory
    {
        private readonly HeaderPolicyCollection _default = new HeaderPolicyCollection().AddCustomHeader("Custom-Header", "Default");
        private readonly HeaderPolicyCollection _custom = new HeaderPolicyCollection().AddCustomHeader("Custom-Header", "Custom");
        
        public HeaderPolicyCollection GetPolicy(string? tenantId)
            => tenantId == "1234" ? _custom : _default;
    }
}