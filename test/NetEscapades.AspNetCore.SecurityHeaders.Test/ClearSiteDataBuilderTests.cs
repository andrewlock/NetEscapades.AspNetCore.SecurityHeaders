using System;
using FluentAssertions;
using Microsoft.AspNetCore.Builder;

namespace NetEscapades.AspNetCore.SecurityHeaders.Test;
public class ClearSiteDataBuilderTests
{
    [Test]
    public void Build_WhenNoDirectives_Throws()
    {
        var builder = new ClearSiteDataBuilder();
        Action act = () => builder.Build();
        act.Should().Throw<InvalidOperationException>();
    }

    [Test]
    public void Build_AddCache_AddsValue()
    {
        var builder = new ClearSiteDataBuilder();
        builder.Cache();
        var result = builder.Build();
        result.Should().Be("\"cache\"");
    }

    [Test]
    public void Build_AddCookies_AddsValue()
    {
        var builder = new ClearSiteDataBuilder();
        builder.Cookies();
        var result = builder.Build();
        result.Should().Be("\"cookies\"");
    }

    [Test]
    public void Build_AddStorage_AddsValue()
    {
        var builder = new ClearSiteDataBuilder();
        builder.Storage();
        var result = builder.Build();
        result.Should().Be("\"storage\"");
    }

    [Test]
    public void Build_AddExecutionContexts_AddsValue()
    {
        var builder = new ClearSiteDataBuilder();
        builder.ExecutionContexts();
        var result = builder.Build();
        result.Should().Be("\"executionContexts\"");
    }

    [Test]
    public void Build_AddCustomDirective_AddsQuotedValue()
    {
        var builder = new ClearSiteDataBuilder();
        builder.CustomDirective("\"prefetchCache\"");
        var result = builder.Build();
        result.Should().Be("\"prefetchCache\"");
    }

    [Test]
    public void Build_AddCustomDirective_AlongsideOthers_ComposesCorrectly()
    {
        var builder = new ClearSiteDataBuilder();
        builder.Cookies().CustomDirective("\"clientHints\"");
        var result = builder.Build();
        result.Should().Be("\"cookies\", \"clientHints\"");
    }

    [Test]
    [Arguments(null)]
    [Arguments("")]
    [Arguments("   ")]
    public void AddCustomDirective_WithInvalidName_Throws(string? directiveName)
    {
        var builder = new ClearSiteDataBuilder();
        Action act = () => builder.CustomDirective(directiveName!);
        act.Should().Throw<ArgumentException>();
    }

    [Test]
    public void Build_MultipleDirectives_AreCommaSeparatedAndQuoted()
    {
        var builder = new ClearSiteDataBuilder();
        builder.Cache().Cookies().Storage().ExecutionContexts();
        var result = builder.Build();
        result.Should().Be("\"cache\", \"cookies\", \"storage\", \"executionContexts\"");
    }

    [Test]
    public void Build_DuplicateDirectives_AreDeduplicated()
    {
        var builder = new ClearSiteDataBuilder();
        builder.Cache().Cookies().Cache();
        var result = builder.Build();
        result.Should().Be("\"cache\", \"cookies\"");
    }

    [Test]
    public void Build_All_EmitsWildcard()
    {
        var builder = new ClearSiteDataBuilder();
        builder.All();
        var result = builder.Build();
        result.Should().Be("\"*\"");
    }

    [Test]
    public void Build_AllAfterOtherDirectives_SupersedesThem()
    {
        var builder = new ClearSiteDataBuilder();
        builder.Cache().Cookies();
        builder.All();
        var result = builder.Build();
        result.Should().Be("\"*\"");
    }

    [Test]
    public void Build_OtherDirectivesAfterAll_StillEmitsWildcard()
    {
        var builder = new ClearSiteDataBuilder();
        builder.All();
        builder.Cache().Cookies();
        var result = builder.Build();
        result.Should().Be("\"*\"");
    }
}
