using System;
using FluentAssertions;
using Microsoft.AspNetCore.Builder;
using Xunit;

namespace NetEscapades.AspNetCore.SecurityHeaders.Test
{
    public class PermissionsPolicyBuilderTests
    {
        [Fact]
        public void PermissionsPolicy_Build_WhenNoValues_Returns()
        {
            var builder = new PermissionsPolicyBuilder();

            var result = builder.Build();

            result.Should().BeNullOrEmpty();
        }

        [Fact]
        public void PermissionsPolicy_Build_AddSelfOnly_ReturnsOnlySelf()
        {
            var builder = new PermissionsPolicyBuilder();

            builder.AddAccelerometer()
              .Self();

            var result = builder.Build();

            result.Should().Be("accelerometer=self");
        }

        [Fact]
        public void PermissionsPolicy_Build_AddAccelerometer_WhenAddsMultipleValue_ReturnsAllValues()
        {
            var builder = new PermissionsPolicyBuilder();

            builder.AddAccelerometer()
              .Self()
              .For("http://testUrl.com").For("https://testUrl2.se");

            var result = builder.Build();

            result.Should().Be("accelerometer=(self \"http://testUrl.com\" \"https://testUrl2.se\")");
        }

        [Fact]
        public void PermissionsPolicy_Build_AddAccelerometer_WhenIncludesNone_OnlyWritesNone()
        {
            var builder = new PermissionsPolicyBuilder();
            builder.AddAccelerometer()
                .Self()
                .For("http://testUrl.com")
                .None();

            var result = builder.Build();

            result.Should().Be("accelerometer=()");
        }

        [Fact]
        public void PermissionsBuild_AddAccelerometer_WhenIncludesAll_OnlyWritesAll()
        {
            var builder = new PermissionsPolicyBuilder();
            builder.AddAccelerometer()
                .Self()
                .For("http://testUrl.com")
                .All();

            var result = builder.Build();

            result.Should().Be("accelerometer=*");
        }

        [Fact]
        public void PermissionsPolicy_Build_AddAccelerometer_WhenIncludesAllAndNone_ThrowsInvalidOperationException()
        {
            var builder = new PermissionsPolicyBuilder();
            builder.AddAccelerometer()
                .None()
                .All();

            Assert.Throws<InvalidOperationException>(() => builder.Build());
        }

        [Fact]
        public void PermissionsPolicy_Build_CustomPermissionsPolicyDirective_AddsValues()
        {
            var builder = new PermissionsPolicyBuilder();
            builder.AddCustomFeature("push", string.Empty);
            builder.AddCustomFeature("vibrate", "*");
            builder.AddCustomFeature("something-else", string.Empty);

            var result = builder.Build();

            result.Should().Be("push=(), vibrate=*, something-else=()");
        }

        [Fact]
        public void PermissionsPolicy_Build_CustomPermissionsPolicyBuilder_AddsValues()
        {
            var builder = new PermissionsPolicyBuilder();
            builder.AddCustomFeature("push").None();
            builder.AddCustomFeature("vibrate").All();

            var result = builder.Build();

            result.Should().Be("push=(), vibrate=*");
        }

        [Fact]
        public void PermissionsPolicy_Build_AddingTheSameDirectiveTwice_OverwritesThePreviousCopy()
        {
            var builder = new PermissionsPolicyBuilder();
            builder.AddAccelerometer().Self();
            builder.AddAccelerometer().None();

            var result = builder.Build();

            result.Should().Be("accelerometer=()");
        }
    }
}
