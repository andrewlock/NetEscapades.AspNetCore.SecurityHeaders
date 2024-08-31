using System.Linq;
using System.Net.Http.Headers;
using FluentAssertions;
using NetEscapades.AspNetCore.SecurityHeaders.Headers;
using Xunit;

namespace NetEscapades.AspNetCore.SecurityHeaders.Test;

public static class HeaderAssertionHelpers
{
    public static void AssertHttpRequestDefaultSecurityHeaders(this HttpResponseHeaders headers)
    {
        string header = headers.GetValues("X-Content-Type-Options").FirstOrDefault()!;
        header.Should().Be("nosniff");
        header = headers.GetValues("X-Frame-Options").FirstOrDefault()!;
        header.Should().Be("DENY");
        header = headers.GetValues("Referrer-Policy").FirstOrDefault()!;
        header.Should().Be("strict-origin-when-cross-origin");
        header = headers.GetValues("Content-Security-Policy").FirstOrDefault()!;
        header.Should().Be("object-src 'none'; form-action 'self'; frame-ancestors 'none'");

        Assert.False(headers.Contains("Server"),
            "Should not contain server header");
        Assert.False(headers.Contains("Strict-Transport-Security"),
            "Should not contain Strict-Transport-Security header over http");
    }

    public static void AssertSecureRequestDefaultSecurityHeaders(this HttpResponseHeaders headers)
    {
        string header = headers.GetValues("X-Content-Type-Options").FirstOrDefault()!;
        header.Should().Be("nosniff");
        header = headers.GetValues("X-Frame-Options").FirstOrDefault()!;
        header.Should().Be("DENY");
        header = headers.GetValues("Strict-Transport-Security").FirstOrDefault()!;
        header.Should().Be($"max-age={StrictTransportSecurityHeader.OneYearInSeconds}");
        header = headers.GetValues("Referrer-Policy").FirstOrDefault()!;
        header.Should().Be("strict-origin-when-cross-origin");
        header = headers.GetValues("Content-Security-Policy").FirstOrDefault()!;
        header.Should().Be("object-src 'none'; form-action 'self'; frame-ancestors 'none'");

        Assert.False(headers.Contains("Server"),
            "Should not contain server header");
    }
}