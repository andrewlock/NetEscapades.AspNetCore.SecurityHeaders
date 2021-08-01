using System;
using FluentAssertions;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Xunit;

namespace NetEscapades.AspNetCore.SecurityHeaders.Test
{
    public class CrossOriginEmbedderPolicyBuilderTests
    {
        [Fact]
        public void Build_WhenNoValues_ReturnsNullValue()
        {
            var builder = new CrossOriginEmbedderPolicyBuilder();

            var result = builder.Build();

            result.ConstantValue.Should().BeNullOrEmpty();
        }

        [Fact]
        public void Build_AddUnsafeNone_AddsValue()
        {
            var builder = new CrossOriginEmbedderPolicyBuilder();
            builder.UnsafeNone();

            var result = builder.Build();

            result.ConstantValue.Should().Be("unsafe-none");
        }

        [Fact]
        public void Build_AddUnsafeNone_WithReportEndpoint_AddsValue()
        {
            var builder = new CrossOriginEmbedderPolicyBuilder();
            builder.UnsafeNone();
            builder.AddReport().To("default");

            var result = builder.Build();

            result.ConstantValue.Should().Be("unsafe-none; report-to=\"default\"");
        }

        [Fact]
        public void Build_AddRequireCorp_AddsValue()
        {
            var builder = new CrossOriginEmbedderPolicyBuilder();
            builder.RequireCorp();
            
            var result = builder.Build();

            result.ConstantValue.Should().Be("require-corp");
        }

        [Fact]
        public void Build_AddRequireCorp_WithReportEndpoint_AddsValue()
        {
            var builder = new CrossOriginEmbedderPolicyBuilder();
            builder.RequireCorp();
            builder.AddReport().To("default");

            var result = builder.Build();

            result.ConstantValue.Should().Be("require-corp; report-to=\"default\"");
        }
    }
}
