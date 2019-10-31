using System;
using FluentAssertions;
using Microsoft.AspNetCore.Builder;
using Xunit;

namespace NetEscapades.AspNetCore.SecurityHeaders.Test
{
    public class CspBuilderTests
    {
        [Fact]
        public void Build_WhenNoValues_ReturnsNullValue()
        {
            var builder = new CspBuilder();

            var result = builder.Build();

            result.ConstantValue.Should().BeNullOrEmpty();
        }

        [Fact]
        public void Build_WhenNoValues_HasPerRequestValuesReturnsFalse()
        {
            var builder = new CspBuilder();

            var result = builder.Build();

            result.HasPerRequestValues.Should().BeFalse();
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

            result.ConstantValue.Should().Be("default-src 'self' blob: data: http://testUrl.com");
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

            result.ConstantValue.Should().Be("connect-src 'self' blob: data: http://testUrl.com");
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

            result.ConstantValue.Should().Be("font-src 'self' blob: data: http://testUrl.com");
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

            result.ConstantValue.Should().Be("object-src 'self' blob: data: http://testUrl.com");
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

            result.ConstantValue.Should().Be("form-action 'self' blob: data: http://testUrl.com");
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

            result.ConstantValue.Should().Be("img-src 'self' blob: data: http://testUrl.com");
        }

        [Fact]
        public void Build_AddWorkerSrc_WhenAddsMultipleValue_ReturnsAllValues()
        {
            var builder = new CspBuilder();
            builder.AddWorkerSrc()
                .Self()
                .Blob()
                .Data()
                .From("http://testUrl.com");

            var result = builder.Build();

            result.ConstantValue.Should().Be("worker-src 'self' blob: data: http://testUrl.com");
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

            result.ConstantValue.Should().Be("script-src 'self' blob: data: http://testUrl.com");
        }

        [Fact]
        public void Build_AddSrciptSrc_WhenAddsNonce_ConstantValueThrowsInvalidOperation()
        {
            var builder = new CspBuilder();
            builder.AddScriptSrc()
                .Self()
                .UnsafeEval()
                .UnsafeInline()
                .StrictDynamic()
                .ReportSample()
                .WithNonce()
                .From("http://testUrl.com");

            var result = builder.Build();

            result.Invoking(x =>
                {
                    var val = x.ConstantValue;
                })
                .ShouldThrow<InvalidOperationException>();
        }

        [Fact]
        public void Build_AddSrciptSrc_WhenDoesntAddNonce_BuilderThrowsInvalidOperation()
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

            result.Invoking(x =>
                {
                    var val = x.Builder;
                })
                .ShouldThrow<InvalidOperationException>();
        }

        [Fact]
        public void Build_AddSrciptSrc_WhenAddsNonce_HasPerRequestValuesReturnsTrue()
        {
            var builder = new CspBuilder();
            builder.AddScriptSrc()
                .Self()
                .UnsafeEval()
                .UnsafeInline()
                .StrictDynamic()
                .ReportSample()
                .WithNonce()
                .From("http://testUrl.com");

            var result = builder.Build();

            result.HasPerRequestValues.Should().BeTrue();
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

            result.ConstantValue.Should().Be("style-src 'self' blob: data: http://testUrl.com");
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

            result.ConstantValue.Should().Be("media-src 'self' blob: data: http://testUrl.com");
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

            result.ConstantValue.Should().Be("frame-ancestors 'self' blob: data: http://testUrl.com");
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

            result.ConstantValue.Should().Be("base-uri 'self' blob: data: http://testUrl.com");
        }

        [Fact]
        public void Build_AddUpgradeInsecureRequests_AddsValue()
        {
            var builder = new CspBuilder();
            builder.AddUpgradeInsecureRequests();

            var result = builder.Build();

            result.ConstantValue.Should().Be("upgrade-insecure-requests");
        }

        [Fact]
        public void Build_AddBlockAllMixedContent_AddsValue()
        {
            var builder = new CspBuilder();
            builder.AddBlockAllMixedContent();

            var result = builder.Build();

            result.ConstantValue.Should().Be("block-all-mixed-content");
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

            result.ConstantValue.Should().Be("default-src 'none'");
        }

        [Fact]
        public void Build_ReportUri_AddsValue()
        {
            var builder = new CspBuilder();
            builder.AddReportUri()
                    .To("http://testUrl.com");

            var result = builder.Build();

            result.ConstantValue.Should().Be("report-uri http://testUrl.com");
        }

        [Fact]
        public void Build_CustomDirective_AddsValues()
        {
            var builder = new CspBuilder();
            builder.AddCustomDirective("report-to");
            builder.AddCustomDirective("plugin-types", "application/x-shockwave-flash");

            var result = builder.Build();

            result.ConstantValue.Should().Be("report-to; plugin-types application/x-shockwave-flash");
        }

        [Fact]
        public void Build_AddingTheSameDirectiveTwice_OverwritesThePreviousCopy()
        {
            var builder = new CspBuilder();
            builder.AddDefaultSrc().Self();
            builder.AddDefaultSrc().None();

            var result = builder.Build();

            result.ConstantValue.Should().Be("default-src 'none'");

        }

        [Fact]
        public void Build_ForAllHeaders_WhenNotUsingNonce_HasPerRequestValuesReturnsFalse()
        {

            var builder = new CspBuilder();
            builder.AddDefaultSrc().Self().Blob().Data().From("http://testUrl.com");
            builder.AddConnectSrc().Self().Blob().Data().From("http://testUrl.com");
            builder.AddFontSrc().Self().Blob().Data().From("http://testUrl.com");
            builder.AddObjectSrc().Self().Blob().Data().From("http://testUrl.com");
            builder.AddFormAction().Self().Blob().Data().From("http://testUrl.com");
            builder.AddImgSrc().Self().Blob().Data().From("http://testUrl.com");
            builder.AddWorkerSrc().Self().Blob().Data().From("http://testUrl.com");
            builder.AddScriptSrc().Self().UnsafeEval().UnsafeInline().StrictDynamic().ReportSample().From("http://testUrl.com");
            builder.AddStyleSrc().Self().Blob().Data().From("http://testUrl.com");
            builder.AddMediaSrc().Self().Blob().Data().From("http://testUrl.com");
            builder.AddFrameAncestors().Self().Blob().Data().From("http://testUrl.com");
            builder.AddBaseUri().Self().Blob().Data().From("http://testUrl.com");
            builder.AddUpgradeInsecureRequests();
            builder.AddBlockAllMixedContent();

            var result = builder.Build();

            result.HasPerRequestValues.Should().BeFalse();
        }

        [Fact]
        public void Build_ForAllHeaders_WhenNotUsingNonce_HasPerRequestValuesReturnsTrue()
        {

            var builder = new CspBuilder();
            builder.AddDefaultSrc().Self().Blob().Data().From("http://testUrl.com");
            builder.AddConnectSrc().Self().Blob().Data().From("http://testUrl.com");
            builder.AddFontSrc().Self().Blob().Data().From("http://testUrl.com");
            builder.AddObjectSrc().Self().Blob().Data().From("http://testUrl.com");
            builder.AddFormAction().Self().Blob().Data().From("http://testUrl.com");
            builder.AddWorkerSrc().Self().Blob().Data().From("http://testUrl.com");
            builder.AddImgSrc().Self().Blob().Data().From("http://testUrl.com");
            builder.AddStyleSrc().Self().Blob().Data().From("http://testUrl.com");
            builder.AddMediaSrc().Self().Blob().Data().From("http://testUrl.com");
            builder.AddFrameAncestors().Self().Blob().Data().From("http://testUrl.com");
            builder.AddBaseUri().Self().Blob().Data().From("http://testUrl.com");
            builder.AddUpgradeInsecureRequests();
            builder.AddBlockAllMixedContent();

            // add nonce
            builder.AddScriptSrc().WithNonce();

            var result = builder.Build();

            result.HasPerRequestValues.Should().BeTrue();
        }
    }
}
