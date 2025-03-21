﻿using System;
using FluentAssertions;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Xunit;

namespace NetEscapades.AspNetCore.SecurityHeaders.Test;

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
    public void Build_WhenAddingTheSameValue_DoesntAddTheSecondValue()
    {
        var builder = new CspBuilder();

        builder.AddDefaultSrc().Self().Self().Self();
        
        var result = builder.Build();

        result.ConstantValue.Should().Be("default-src 'self'");
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
    public void Build_AddDefaultSrc_WhenAddsMultipleValueEnumerable_ReturnsAllValues()
    {
        var builder = new CspBuilder();
        builder.AddDefaultSrc()
            .Self()
            .Blob()
            .Data()
            .From(new []{"http://testUrl.com", "http://testUrl2.com"});

        var result = builder.Build();

        result.ConstantValue.Should().Be("default-src 'self' blob: data: http://testUrl.com http://testUrl2.com");
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
    public void Build_AddSrciptSrcAttr_WhenAddsMultipleValue_ReturnsAllValues()
    {
        var builder = new CspBuilder();
        builder.AddScriptSrcAttr()
            .Self()
            .Blob()
            .Data()
            .From("http://testUrl.com");

        var result = builder.Build();

        result.ConstantValue.Should().Be("script-src-attr 'self' blob: data: http://testUrl.com");
    }

    [Fact]
    public void Build_AddSrciptSrcElem_WhenAddsMultipleValue_ReturnsAllValues()
    {
        var builder = new CspBuilder();
        builder.AddScriptSrcElem()
            .Self()
            .Blob()
            .Data()
            .From("http://testUrl.com");

        var result = builder.Build();

        result.ConstantValue.Should().Be("script-src-elem 'self' blob: data: http://testUrl.com");
    }

    [Fact]
    public void Build_AddSrciptSrc_WhenAddsNonce_ConstantValueThrowsInvalidOperation()
    {
        var builder = new CspBuilder();
        builder.AddScriptSrc()
            .Self()
            .UnsafeEval()
            .WasmUnsafeEval()
            .UnsafeInline()
            .StrictDynamic()
            .ReportSample()
            .WithNonce()
            .From("http://testUrl.com");

        var result = builder.Build();

        result.Invoking(x=>x.ConstantValue)
            .Should()
            .Throw<InvalidOperationException>();
    }
    [Theory]
    [InlineData(true, false)]
    [InlineData(false, true)]
    [InlineData(true, true)]
    public void Build_AddSrciptSrc_ReportSampleAndNone_ShouldAllowBoth(bool reportSample, bool none)
    {
        var builder = new CspBuilder();
        var src = builder.AddScriptSrc();
        if (reportSample)
        {
            src.ReportSample();
        }

        if (none)
        {
            src.None();
        }

        var expected = (reportSample, none) switch
        {
            (true, false) => "script-src 'report-sample'",
            (false, true) => "script-src 'none'",
            (true, true) => "script-src 'report-sample' 'none'",
            _ => throw new InvalidOperationException(),
        };

        var result = builder.Build();

        result.ConstantValue.Should().Be(expected);
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

        result.Invoking(x=>x.Builder)
            .Should()
            .Throw<InvalidOperationException>();
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
            .ReportSample()
            .Blob()
            .Data()
            .From("http://testUrl.com");

        var result = builder.Build();

        result.ConstantValue.Should().Be("style-src 'self' 'report-sample' blob: data: http://testUrl.com");
    }

    [Fact]
    public void Build_AddStyleSrcAttr_WhenAddsMultipleValue_ReturnsAllValues()
    {
        var builder = new CspBuilder();
        builder.AddStyleSrcAttr()
            .Self()
            .ReportSample()
            .Blob()
            .Data()
            .From("http://testUrl.com");

        var result = builder.Build();

        result.ConstantValue.Should().Be("style-src-attr 'self' 'report-sample' blob: data: http://testUrl.com");
    }

    [Fact]
    public void Build_AddStyleSrcElem_WhenAddsMultipleValue_ReturnsAllValues()
    {
        var builder = new CspBuilder();
        builder.AddStyleSrcElem()
            .Self()
            .ReportSample()
            .Blob()
            .Data()
            .From("http://testUrl.com");

        var result = builder.Build();

        result.ConstantValue.Should().Be("style-src-elem 'self' 'report-sample' blob: data: http://testUrl.com");
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
            .From(["http://testUrl.com", "https://testUrl.com"]);

        var result = builder.Build();

        result.ConstantValue.Should().Be("frame-ancestors 'self' blob: data: http://testUrl.com https://testUrl.com");
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
    public void Build_AddSandbox_WhenAddsMultipleValue_ReturnsAllValues()
    {
        var builder = new CspBuilder();
        builder.AddSandbox()
            .AllowDownloads()
            .AllowForms()
            .AllowModals();

        var result = builder.Build();

        result.ConstantValue.Should().Be("sandbox allow-downloads allow-forms allow-modals");
    }

    [Fact]
    public void Build_AddSandbox_WhenSingleValue_ReturnsDirective()
    {
        var builder = new CspBuilder();
        builder.AddSandbox()
            .AllowTopNavigationToCustomProtocols();

        var result = builder.Build();

        result.ConstantValue.Should().Be("sandbox allow-top-navigation-to-custom-protocols");
    }

    [Fact]
    public void Build_AddSandbox_WhenNoValues_ReturnsDirective()
    {
        var builder = new CspBuilder();
        builder.AddSandbox();

        var result = builder.Build();

        result.ConstantValue.Should().Be("sandbox");
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
    public void Build_ReportTo_AddsValue()
    {
        var builder = new CspBuilder();
        builder.AddReportTo("some-endpoint");

        var result = builder.Build();

        result.ConstantValue.Should().Be("report-to some-endpoint");
    }

    [Fact]
    public void Build_RequireTrustedTypesFor_AddsValue()
    {
        var builder = new CspBuilder();
        
        builder.AddRequireTrustedTypesFor().Script();
        var result = builder.Build();

        result.ConstantValue.Should().Be("require-trusted-types-for 'script'");
    }

    [Fact]
    public void Build_TrustedTypes_AddsValue()
    {
        var builder = new CspBuilder();

        builder.AddTrustedTypes()
            .AllowPolicy("one")
            .AllowPolicy("two")
            .Default()
            .AllowDuplicates();
        var result = builder.Build();

        result.ConstantValue.Should().Be("trusted-types one two default 'allow-duplicates'");
    }

    [Fact]
    public void Build_TrustedTypes_AddsNone()
    {
        var builder = new CspBuilder();

        builder.AddTrustedTypes().None();
        var result = builder.Build();

        result.ConstantValue.Should().Be("trusted-types 'none'");
    }

    [Fact]
    public void Build_TrustedTypes_AddsAny()
    {
        var builder = new CspBuilder();

        builder.AddTrustedTypes().Any();
        var result = builder.Build();

        result.ConstantValue.Should().Be("trusted-types *");
    }

    [Theory]
    [InlineData("*")]
    [InlineData("Oops!")]
    [InlineData(" ")]
    public void Build_TrustedTypes_RejectsInvalidValues(string policyName)
    {
        var builder = new CspBuilder();

        var method = () => builder.AddTrustedTypes().AllowPolicy(policyName);

        method.Should().Throw<ArgumentException>();
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
        builder.AddScriptSrc().Self().UnsafeEval().UnsafeHashes().UnsafeInline().WasmUnsafeEval().StrictDynamic().ReportSample().From("http://testUrl.com");
        builder.AddStyleSrc().Self().Blob().Data().From("http://testUrl.com");
        builder.AddMediaSrc().Self().Blob().Data().From("http://testUrl.com");
        builder.AddFrameAncestors().Self().Blob().Data().From("http://testUrl.com");
        builder.AddBaseUri().Self().Blob().Data().From("http://testUrl.com");
        builder.AddSandbox();
        builder.AddUpgradeInsecureRequests();
        builder.AddBlockAllMixedContent();

        var result = builder.Build();

        result.HasPerRequestValues.Should().BeFalse();
    }

    [Fact]
    public void Build_ForAllHeaders_WhenUsingNonce_HasPerRequestValuesReturnsTrue()
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
        builder.AddSandbox();
        builder.AddUpgradeInsecureRequests();
        builder.AddBlockAllMixedContent();

        // add nonce
        builder.AddScriptSrc().WithNonce();

        var result = builder.Build();

        result.HasPerRequestValues.Should().BeTrue();
    }

    [Fact]
    public void Builder_WhenUsingNonce_AddsNonceToCSP()
    {
        var builder = new CspBuilder();
        builder.AddScriptSrc().WithNonce();
        builder.AddStyleSrc().WithNonce();
        builder.AddCustomDirectiveBuilder("test-directive").WithNonce();

        var result = builder.Build();

        var httpContext = new DefaultHttpContext();
        var nonce = "ABC123";
        httpContext.Items[Infrastructure.Constants.DefaultNonceKey] = nonce;

        var csp = result.Builder(httpContext);

        csp.Should().Be($"script-src 'nonce-{nonce}'; style-src 'nonce-{nonce}'; test-directive 'nonce-{nonce}'");
    }

    [Fact]
    public void Builder_WhenCallingWithNonceTwice_OnlyAddsNonceToCSPOnce()
    {
        var builder = new CspBuilder();
        builder.AddScriptSrc().WithNonce().WithNonce();

        var result = builder.Build();

        var httpContext = new DefaultHttpContext();
        var nonce = "ABC123";
        httpContext.Items[Infrastructure.Constants.DefaultNonceKey] = nonce;

        var csp = result.Builder(httpContext);

        csp.Should().Be($"script-src 'nonce-{nonce}'");
    }

    [Fact]
    public void Builder_WhenCallingWithNonceOnDifferentDirectives_AddsNoneForBoth()
    {
        var builder = new CspBuilder();
        builder.AddScriptSrc().WithNonce().WithNonce();
        builder.AddStyleSrc().WithNonce().WithNonce();

        var result = builder.Build();

        var httpContext = new DefaultHttpContext();
        var nonce = "ABC123";
        httpContext.Items[Infrastructure.Constants.DefaultNonceKey] = nonce;

        var csp = result.Builder(httpContext);

        csp.Should().Be($"script-src 'nonce-{nonce}'; style-src 'nonce-{nonce}'");
    }
}