using FluentAssertions;
using NetEscapades.AspNetCore.SecurityHeaders.Infrastructure;
using Xunit;

namespace NetEscapades.AspNetCore.SecurityHeaders.Test
{
    public class CspBuilderTests
    {
        [Fact]
        public void Build_WhenNoValues_ReturnsNull()
        {
            var builder = new CspBuilder();

            var result = builder.Build();

            result.Should().BeNullOrEmpty();
        }

        [Fact]
        public void Build_AddDefaultSrc_WhenAddsMultipleValue_ReturnsAllValues()
        {
            var builder = new CspBuilder();
            builder.AddDefaultSrc()
                .Self()
                .Blob()
                .Data()
                .From("http://testUrl.com");

            var result = builder.Build();

            result.Should().Be("default-src 'self' blob: data: http://testUrl.com");
        }

        [Fact]
        public void Build_AddConnectSrc_WhenAddsMultipleValue_ReturnsAllValues()
        {
            var builder = new CspBuilder();
            builder.AddConnectSrc()
                .Self()
                .Blob()
                .Data()
                .From("http://testUrl.com");

            var result = builder.Build();

            result.Should().Be("connect-src 'self' blob: data: http://testUrl.com");
        }

        [Fact]
        public void Build_AddFontSrc_WhenAddsMultipleValue_ReturnsAllValues()
        {
            var builder = new CspBuilder();
            builder.AddFontSrc()
                .Self()
                .Blob()
                .Data()
                .From("http://testUrl.com");

            var result = builder.Build();

            result.Should().Be("font-src 'self' blob: data: http://testUrl.com");
        }

        [Fact]
        public void Build_AddObjectSrc_WhenAddsMultipleValue_ReturnsAllValues()
        {
            var builder = new CspBuilder();
            builder.AddObjectSrc()
                .Self()
                .Blob()
                .Data()
                .From("http://testUrl.com");

            var result = builder.Build();

            result.Should().Be("object-src 'self' blob: data: http://testUrl.com");
        }

        [Fact]
        public void Build_AddFormAction_WhenAddsMultipleValue_ReturnsAllValues()
        {
            var builder = new CspBuilder();
            builder.AddFormAction()
                .Self()
                .Blob()
                .Data()
                .From("http://testUrl.com");

            var result = builder.Build();

            result.Should().Be("form-action 'self' blob: data: http://testUrl.com");
        }

        [Fact]
        public void Build_AddImgSrc_WhenAddsMultipleValue_ReturnsAllValues()
        {
            var builder = new CspBuilder();
            builder.AddImgSrc()
                .Self()
                .Blob()
                .Data()
                .From("http://testUrl.com");

            var result = builder.Build();

            result.Should().Be("img-src 'self' blob: data: http://testUrl.com");
        }

        [Fact]
        public void Build_AddSrciptSrc_WhenAddsMultipleValue_ReturnsAllValues()
        {
            var builder = new CspBuilder();
            builder.AddScriptSrc()
                .Self()
                .Blob()
                .Data()
                .From("http://testUrl.com");

            var result = builder.Build();

            result.Should().Be("script-src 'self' blob: data: http://testUrl.com");
        }

        [Fact]
        public void Build_AddSrciptSrc_WhenAddsInsecureValues_ReturnsAllValues()
        {
            var builder = new CspBuilder();
            builder.AddScriptSrc()
                .Self()
                .UnsafeEval()
                .UnsafeInline()
                .StrictDynamic()
                .ReportSample()
                .From("http://testUrl.com");

            var result = builder.Build();

            result.Should().Be("script-src 'self' 'unsafe-eval' 'unsafe-inline' 'strict-dynamic' 'report-sample' http://testUrl.com");
        }

        [Fact]
        public void Build_AddStyleSrc_WhenAddsMultipleValue_ReturnsAllValues()
        {
            var builder = new CspBuilder();
            builder.AddStyleSrc()
                .Self()
                .Blob()
                .Data()
                .From("http://testUrl.com");

            var result = builder.Build();

            result.Should().Be("style-src 'self' blob: data: http://testUrl.com");
        }

        [Fact]
        public void Build_AddMediaSrc_WhenAddsMultipleValue_ReturnsAllValues()
        {
            var builder = new CspBuilder();
            builder.AddMediaSrc()
                .Self()
                .Blob()
                .Data()
                .From("http://testUrl.com");

            var result = builder.Build();

            result.Should().Be("media-src 'self' blob: data: http://testUrl.com");
        }

        [Fact]
        public void Build_AddFrameAncestors_WhenAddsMultipleValue_ReturnsAllValues()
        {
            var builder = new CspBuilder();
            builder.AddFrameAncestors()
                .Self()
                .Blob()
                .Data()
                .From("http://testUrl.com");

            var result = builder.Build();

            result.Should().Be("frame-ancestors 'self' blob: data: http://testUrl.com");
        }

        [Fact]
        public void Build_AddBaseUri_WhenAddsMultipleValue_ReturnsAllValues()
        {
            var builder = new CspBuilder();
            builder.AddBaseUri()
                .Self()
                .Blob()
                .Data()
                .From("http://testUrl.com");

            var result = builder.Build();

            result.Should().Be("base-uri 'self' blob: data: http://testUrl.com");
        }

        [Fact]
        public void Build_AddUpgradeInsecureRequests_AddsValue()
        {
            var builder = new CspBuilder()
                .AddUpgradeInsecureRequests();

            var result = builder.Build();

            result.Should().Be("upgrade-insecure-requests");
        }

        [Fact]
        public void Build_AddBlockAllMixedContent_AddsValue()
        {
            var builder = new CspBuilder()
                .AddBlockAllMixedContent();

            var result = builder.Build();

            result.Should().Be("block-all-mixed-content");
        }

        [Fact]
        public void Build_AddDefaultSrc_WhenIncludesNone_OnlyWritesNone()
        {
            var builder = new CspBuilder();
            builder.AddDefaultSrc()
                .Self()
                .Blob()
                .Data()
                .From("http://testUrl.com")
                .None();

            var result = builder.Build();

            result.Should().Be("default-src 'none'");
        }

        [Fact]
        public void Build_ReportUri_AddsValue()
        {
            var builder = new CspBuilder()
                .AddReportUri()
                    .To("http://testUrl.com");

            var result = builder.Build();

            result.Should().Be("report-uri http://testUrl.com");
        }

        [Fact]
        public void Build_CustomDirective_AddsValues()
        {
            var builder = new CspBuilder();
            builder.AddCustomDirective("report-to");
            builder.AddCustomDirective("plugin-types", "application/x-shockwave-flash");

            var result = builder.Build();

            result.Should().Be("report-to; plugin-types application/x-shockwave-flash");
        }

        [Fact]
        public void Build_AddingTheSameDirectiveTwice_OverwritesThePreviousCopy()
        {
            var builder = new CspBuilder();
            builder.AddDefaultSrc().Self();
            builder.AddDefaultSrc().None();

            var result = builder.Build();

            result.Should().Be("default-src 'none'");

        }
    }
}
