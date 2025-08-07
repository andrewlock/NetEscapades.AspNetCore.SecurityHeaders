using System;
using FluentAssertions;
using Microsoft.AspNetCore.Builder;

namespace NetEscapades.AspNetCore.SecurityHeaders.Test;
public class FeaturePolicyBuilderTests
{
    [Test]
    public void Build_WhenNoValues_ReturnsNull()
    {
        var builder = new FeaturePolicyBuilder();
        var result = builder.Build();
        result.Should().BeNullOrEmpty();
    }

    [Test]
    public void Build_AddAccelerometer_WhenAddsMultipleValue_ReturnsAllValues()
    {
        var builder = new FeaturePolicyBuilder();
        builder.AddAccelerometer().Self().For("http://testUrl.com");
        var result = builder.Build();
        result.Should().Be("accelerometer 'self' http://testUrl.com");
    }

    [Test]
    public void Build_AddAccelerometer_WhenIncludesNone_OnlyWritesNone()
    {
        var builder = new FeaturePolicyBuilder();
        builder.AddAccelerometer().Self().For("http://testUrl.com").None();
        var result = builder.Build();
        result.Should().Be("accelerometer 'none'");
    }

    [Test]
    public void Build_AddAccelerometer_WhenIncludesAll_OnlyWritesAll()
    {
        var builder = new FeaturePolicyBuilder();
        builder.AddAccelerometer().Self().For("http://testUrl.com").All();
        var result = builder.Build();
        result.Should().Be("accelerometer *");
    }

    [Test]
    public void Build_AddAccelerometer_WhenIncludesAllAndNone_ThrowsInvalidOperationException()
    {
        var builder = new FeaturePolicyBuilder();
        builder.AddAccelerometer().None().All();
        var act = () => builder.Build();
        act.Should().ThrowExactly<InvalidOperationException>();
    }

    [Test]
    public void Build_CustomFeatureDirective_AddsValues()
    {
        var builder = new FeaturePolicyBuilder();
        builder.AddCustomFeature("push", "'none'");
        builder.AddCustomFeature("vibrate", "*");
        builder.AddCustomFeature("something-else", string.Empty);
        var result = builder.Build();
        result.Should().Be("push 'none'; vibrate *; something-else");
    }

    [Test]
    public void Build_CustomFeatureDirectiveBuilder_AddsValues()
    {
        var builder = new FeaturePolicyBuilder();
        builder.AddCustomFeature("push").None();
        builder.AddCustomFeature("vibrate").All();
        var result = builder.Build();
        result.Should().Be("push 'none'; vibrate *");
    }

    [Test]
    public void Build_AddingTheSameDirectiveTwice_OverwritesThePreviousCopy()
    {
        var builder = new FeaturePolicyBuilder();
        builder.AddAccelerometer().Self();
        builder.AddAccelerometer().None();
        var result = builder.Build();
        result.Should().Be("accelerometer 'none'");
    }

    [Test]
    public void Build_AddSyncXHR_WhenIncludesSelf_WritesSelf()
    {
        var builder = new FeaturePolicyBuilder();
        builder.AddSyncXHR().Self();
        var result = builder.Build();
        result.Should().Be("sync-xhr 'self'");
    }
}