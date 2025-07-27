using System;
using FluentAssertions;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;

namespace NetEscapades.AspNetCore.SecurityHeaders.Test;
public class CrossOriginOpenerPolicyBuilderTests
{
    [Test]
    public void Build_WhenNoValues_ReturnsNullValue()
    {
        var builder = new CrossOriginOpenerPolicyBuilder();
        var result = builder.Build();
        result.ConstantValue.Should().BeNullOrEmpty();
    }

    [Test]
    public void Build_AddUnsafeNone_AddsValue()
    {
        var builder = new CrossOriginOpenerPolicyBuilder();
        builder.UnsafeNone();
        var result = builder.Build();
        result.ConstantValue.Should().Be("unsafe-none");
    }

    [Test]
    public void Build_AddUnsafeNone_WithReportEndpoint_AddsValue()
    {
        var builder = new CrossOriginOpenerPolicyBuilder();
        builder.UnsafeNone();
        builder.AddReport().To("default");
        var result = builder.Build();
        result.ConstantValue.Should().Be("unsafe-none; report-to=\"default\"");
    }

    [Test]
    public void Build_AddSameOrigin_AddsValue()
    {
        var builder = new CrossOriginOpenerPolicyBuilder();
        builder.SameOrigin();
        var result = builder.Build();
        result.ConstantValue.Should().Be("same-origin");
    }

    [Test]
    public void Build_AddSameOrigin_WithReportEndpoint_AddsValue()
    {
        var builder = new CrossOriginOpenerPolicyBuilder();
        builder.SameOrigin();
        builder.AddReport().To("default");
        var result = builder.Build();
        result.ConstantValue.Should().Be("same-origin; report-to=\"default\"");
    }

    [Test]
    public void Build_AddSameOriginAllowPopups_AddsValue()
    {
        var builder = new CrossOriginOpenerPolicyBuilder();
        builder.SameOriginAllowPopups();
        var result = builder.Build();
        result.ConstantValue.Should().Be("same-origin-allow-popups");
    }

    [Test]
    public void Build_AddSameOriginAllowPopups_WithReportEndpoint_AddsValue()
    {
        var builder = new CrossOriginOpenerPolicyBuilder();
        builder.SameOriginAllowPopups();
        builder.AddReport().To("default");
        var result = builder.Build();
        result.ConstantValue.Should().Be("same-origin-allow-popups; report-to=\"default\"");
    }
}