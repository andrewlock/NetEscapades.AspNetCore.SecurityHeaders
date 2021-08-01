using System;
using FluentAssertions;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Xunit;

namespace NetEscapades.AspNetCore.SecurityHeaders.Test
{
    public class CrossOriginResourcePolicyBuilderTests
    {
        [Fact]
        public void Build_WhenNoValues_ReturnsNullValue()
        {
            var builder = new CrossOriginResourcePolicyBuilder();

            var result = builder.Build();

            result.ConstantValue.Should().BeNullOrEmpty();
        }

        [Fact]
        public void Build_AddSameSite_AddsValue()
        {
            var builder = new CrossOriginResourcePolicyBuilder();
            builder.SameSite();

            var result = builder.Build();

            result.ConstantValue.Should().Be("same-site");
        }

        [Fact]
        public void Build_AddSameSite_WithReportEndpoint_AddsValue()
        {
            var builder = new CrossOriginResourcePolicyBuilder();
            builder.SameSite();
            builder.AddReport().To("default");

            var result = builder.Build();

            result.ConstantValue.Should().Be("same-site; report-to=\"default\"");
        }

        [Fact]
        public void Build_AddSameOrigin_AddsValue()
        {
            var builder = new CrossOriginResourcePolicyBuilder();
            builder.SameOrigin();
            
            var result = builder.Build();

            result.ConstantValue.Should().Be("same-origin");
        }

        [Fact]
        public void Build_AddSameOrigin_WithReportEndpoint_AddsValue()
        {
            var builder = new CrossOriginResourcePolicyBuilder();
            builder.SameOrigin();
            builder.AddReport().To("default");

            var result = builder.Build();

            result.ConstantValue.Should().Be("same-origin; report-to=\"default\"");
        }

        [Fact]
        public void Build_AddCrossOrigin_AddsValue()
        {
            var builder = new CrossOriginResourcePolicyBuilder();
            builder.CrossOrigin();

            var result = builder.Build();

            result.ConstantValue.Should().Be("cross-origin");
        }

        [Fact]
        public void Build_AddCrossOrigin_WithReportEndpoint_AddsValue()
        {
            var builder = new CrossOriginResourcePolicyBuilder();
            builder.CrossOrigin();
            builder.AddReport().To("default");

            var result = builder.Build();

            result.ConstantValue.Should().Be("cross-origin; report-to=\"default\"");
        }
    }
}
