using System;
using FluentAssertions;
using NetEscapades.AspNetCore.SecurityHeaders.Headers;

namespace NetEscapades.AspNetCore.SecurityHeaders.Test;
public class ReportingEndpointsHeaderBuilderTests
{
    [Test]
    public void Build_WhenNoValues_ReturnsNull()
    {
        var builder = new ReportingEndpointsHeaderBuilder();
        var result = builder.Build();
        result.Should().BeNullOrEmpty();
    }

    [Test]
    [Arguments(null)]
    [Arguments("")]
    public void Build_WhenNullName_Throws(string? name)
    {
        var builder = new ReportingEndpointsHeaderBuilder();
        var act = () => builder.AddEndpoint(name!, "http://localhost:80/");
        act.Should().ThrowExactly<ArgumentNullException>();
    }

    [Test]
    [Arguments(null)]
    [Arguments("")]
    public void Build_WhenNullUrl_Throws(string? url)
    {
        var builder = new ReportingEndpointsHeaderBuilder();
        var act = () => builder.AddDefaultEndpoint(url!);
        act.Should().ThrowExactly<ArgumentNullException>();
    }

    [Test]
    [Arguments("not/\\aurl")]
    [Arguments("/relative")]
    public void Build_WhenInvalidUrlUrl_Throws(string url)
    {
        var builder = new ReportingEndpointsHeaderBuilder();
        var act = () => builder.AddDefaultEndpoint(url);
        act.Should().ThrowExactly<ArgumentException>();
    }

    [Test]
    public void Build_WithDefault_ReturnsExpected()
    {
        var builder = new ReportingEndpointsHeaderBuilder().AddDefaultEndpoint("https://localhost:1000/default");
        var result = builder.Build();
        result.Should().Be("default=\"https://localhost:1000/default\"");
    }

    [Test]
    public void Build_WithDefault_AddsTrailingSlashIfNone()
    {
        var builder = new ReportingEndpointsHeaderBuilder().AddDefaultEndpoint("https://localhost:1000");
        var result = builder.Build();
        result.Should().Be("default=\"https://localhost:1000/\"");
    }

    [Test]
    public void Build_WithDefault_AsUri_ReturnsExpected()
    {
        var builder = new ReportingEndpointsHeaderBuilder().AddDefaultEndpoint(new Uri("https://localhost:1000/default"));
        var result = builder.Build();
        result.Should().Be("default=\"https://localhost:1000/default\"");
    }

    [Test]
    public void Build_WithNamedEndpoint_ReturnsExpected()
    {
        var builder = new ReportingEndpointsHeaderBuilder().AddEndpoint("endpoint-1", "https://localhost:1000/default");
        var result = builder.Build();
        result.Should().Be("endpoint-1=\"https://localhost:1000/default\"");
    }

    [Test]
    public void Build_WithNamedEndpoint_AsUri_ReturnsExpected()
    {
        var builder = new ReportingEndpointsHeaderBuilder().AddEndpoint("endpoint-1", new Uri("https://localhost:1000/default"));
        var result = builder.Build();
        result.Should().Be("endpoint-1=\"https://localhost:1000/default\"");
    }

    [Test]
    public void Build_WithMultipleEndpoints_ReturnsExpected()
    {
        var builder = new ReportingEndpointsHeaderBuilder().AddDefaultEndpoint("https://localhost:1000/default").AddEndpoint("endpoint-1", new Uri("https://localhost:1001/non-default"));
        var result = builder.Build();
        result.Should().Be("default=\"https://localhost:1000/default\", endpoint-1=\"https://localhost:1001/non-default\"");
    }
}