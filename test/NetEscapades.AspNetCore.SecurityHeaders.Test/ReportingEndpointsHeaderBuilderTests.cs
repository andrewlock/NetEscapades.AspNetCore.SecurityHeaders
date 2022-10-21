using System;
using FluentAssertions;
using NetEscapades.AspNetCore.SecurityHeaders.Headers;
using Xunit;

namespace NetEscapades.AspNetCore.SecurityHeaders.Test;

public class ReportingEndpointsHeaderBuilderTests
{
    [Fact]
    public void Build_WhenNoValues_ReturnsNull()
    {
        var builder = new ReportingEndpointsHeaderBuilder();

        var result = builder.Build();
        result.Should().BeNullOrEmpty();
    }

    [Theory]
    [InlineData(null)]
    [InlineData("")]
    public void Build_WhenNullName_Throws(string name)
    {
        var builder = new ReportingEndpointsHeaderBuilder();

        Assert.Throws<ArgumentNullException>(() => builder.AddEndpoint(name, "http://localhost:80/"));
    }

    [Theory]
    [InlineData(null)]
    [InlineData("")]
    public void Build_WhenNullUrl_Throws(string url)
    {
        var builder = new ReportingEndpointsHeaderBuilder();

        Assert.Throws<ArgumentNullException>(() => builder.AddDefaultEndpoint(url));
    }

    [Theory]
    [InlineData("not/\\aurl")]
    [InlineData("/relative")]
    public void Build_WhenInvalidUrlUrl_Throws(string url)
    {
        var builder = new ReportingEndpointsHeaderBuilder();

        Assert.Throws<ArgumentException>(() => builder.AddDefaultEndpoint(url));
    }

    [Fact]
    public void Build_WithDefault_ReturnsExpected()
    {
        var builder = new ReportingEndpointsHeaderBuilder()
            .AddDefaultEndpoint("https://localhost:1000/default");

        var result = builder.Build();
        result.Should().Be("default=\"https://localhost:1000/default\"");
    }

    [Fact]
    public void Build_WithDefault_AddsTrailingSlashIfNone()
    {
        var builder = new ReportingEndpointsHeaderBuilder()
            .AddDefaultEndpoint("https://localhost:1000");

        var result = builder.Build();
        result.Should().Be("default=\"https://localhost:1000/\"");
    }

    [Fact]
    public void Build_WithDefault_AsUri_ReturnsExpected()
    {
        var builder = new ReportingEndpointsHeaderBuilder()
            .AddDefaultEndpoint(new Uri("https://localhost:1000/default"));

        var result = builder.Build();
        result.Should().Be("default=\"https://localhost:1000/default\"");
    }

    [Fact]
    public void Build_WithNamedEndpoint_ReturnsExpected()
    {
        var builder = new ReportingEndpointsHeaderBuilder()
            .AddEndpoint("endpoint-1", "https://localhost:1000/default");

        var result = builder.Build();
        result.Should().Be("endpoint-1=\"https://localhost:1000/default\"");
    }

    [Fact]
    public void Build_WithNamedEndpoint_AsUri_ReturnsExpected()
    {
        var builder = new ReportingEndpointsHeaderBuilder()
            .AddEndpoint("endpoint-1", new Uri("https://localhost:1000/default"));

        var result = builder.Build();
        result.Should().Be("endpoint-1=\"https://localhost:1000/default\"");
    }

    [Fact]
    public void Build_WithMultipleEndpoints_ReturnsExpected()
    {
        var builder = new ReportingEndpointsHeaderBuilder()
            .AddDefaultEndpoint("https://localhost:1000/default")
            .AddEndpoint("endpoint-1", new Uri("https://localhost:1001/non-default"));

        var result = builder.Build();
        result.Should().Be("default=\"https://localhost:1000/default\", endpoint-1=\"https://localhost:1001/non-default\"");
    }
}