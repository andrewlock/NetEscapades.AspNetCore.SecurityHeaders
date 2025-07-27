using System;
using FluentAssertions;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;

namespace NetEscapades.AspNetCore.SecurityHeaders.Test;
public class CrossOriginEmbedderPolicyBuilderTests
{
    [Test]
    public void Build_WhenNoValues_ReturnsNullValue()
    {
        var builder = new CrossOriginEmbedderPolicyBuilder();
        var result = builder.Build();
        result.ConstantValue.Should().BeNullOrEmpty();
    }

    [Test]
    public void Build_AddUnsafeNone_AddsValue()
    {
        var builder = new CrossOriginEmbedderPolicyBuilder();
        builder.UnsafeNone();
        var result = builder.Build();
        result.ConstantValue.Should().Be("unsafe-none");
    }

    [Test]
    public void Build_AddUnsafeNone_WithReportEndpoint_AddsValue()
    {
        var builder = new CrossOriginEmbedderPolicyBuilder();
        builder.UnsafeNone();
        builder.AddReport().To("default");
        var result = builder.Build();
        result.ConstantValue.Should().Be("unsafe-none; report-to=\"default\"");
    }

    [Test]
    public void Build_AddRequireCorp_AddsValue()
    {
        var builder = new CrossOriginEmbedderPolicyBuilder();
        builder.RequireCorp();
        var result = builder.Build();
        result.ConstantValue.Should().Be("require-corp");
    }

    [Test]
    public void Build_AddRequireCorp_WithReportEndpoint_AddsValue()
    {
        var builder = new CrossOriginEmbedderPolicyBuilder();
        builder.RequireCorp();
        builder.AddReport().To("default");
        var result = builder.Build();
        result.ConstantValue.Should().Be("require-corp; report-to=\"default\"");
    }

    [Test]
    public void Build_AddCredentialless_AddsValue()
    {
        var builder = new CrossOriginEmbedderPolicyBuilder();
        builder.Credentialless();
        var result = builder.Build();
        result.ConstantValue.Should().Be("credentialless");
    }

    [Test]
    public void Build_AddCredentialless_WithReportEndpoint_AddsValue()
    {
        var builder = new CrossOriginEmbedderPolicyBuilder();
        builder.Credentialless();
        builder.AddReport().To("default");
        var result = builder.Build();
        result.ConstantValue.Should().Be("credentialless; report-to=\"default\"");
    }
}