using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using NetEscapades.AspNetCore.SecurityHeaders.Infrastructure;
using Xunit;

namespace NetEscapades.AspNetCore.SecurityHeaders.Test;

public class SecurityHeaderPolicyBuilderTests
{
    [Fact]
    public void AddPolicy_WithOptions_AddsPolicyToNamedPolicyCollections()
    {
        // Arrange
        var options = new CustomHeaderOptions();
        var builder = new SecurityHeaderPolicyBuilder(options);
        var policy = new HeaderPolicyCollection();
        string policyName = "TestPolicy";

        // Act
        builder.AddPolicy(policyName, policy);

        // Assert
        Assert.True(options.NamedPolicyCollections.ContainsKey(policyName));
        Assert.Same(options.NamedPolicyCollections[policyName], policy);
    }
    
    [Fact]
    public void AddPolicy_WithServiceCollection_AddsPolicyToNamedPolicyCollections()
    {
        // Arrange
        var services = new ServiceCollection();
        var builder = new SecurityHeaderPolicyBuilder(services);
        var policy = new HeaderPolicyCollection();
        string policyName = "TestPolicy";

        // Act
        builder.AddPolicy(policyName, policy);

        // Assert
        var serviceProvider = services.BuildServiceProvider();
        var options = serviceProvider.GetRequiredService<IOptions<CustomHeaderOptions>>().Value;
        Assert.True(options.NamedPolicyCollections.ContainsKey(policyName));
        Assert.Same(options.NamedPolicyCollections[policyName], policy);
    }
    
    [Fact]
    public void SetDefaultPolicy_WithOptions_SetsDefaultPolicy()
    {
        // Arrange
        var options = new CustomHeaderOptions();
        var builder = new SecurityHeaderPolicyBuilder(options);
        var policy = new HeaderPolicyCollection();

        // Act
        builder.SetDefaultPolicy(policy);

        // Assert
        Assert.Same(options.DefaultPolicy, policy);
    }

    [Fact]
    public void SetDefaultPolicy_WithServiceCollection_SetsDefaultPolicy()
    {
        // Arrange
        var services = new ServiceCollection();
        var builder = new SecurityHeaderPolicyBuilder(services);
        var policy = new HeaderPolicyCollection();

        // Act
        builder.SetDefaultPolicy(policy);

        // Assert
        var serviceProvider = services.BuildServiceProvider();
        var options = serviceProvider.GetRequiredService<IOptions<CustomHeaderOptions>>().Value;
        Assert.Same(options.DefaultPolicy, policy);
    }
    
    [Fact]
    public void SetPolicySelector_WithOptions_SetsPolicySelector()
    {
        // Arrange
        var options = new CustomHeaderOptions();
        var builder = new SecurityHeaderPolicyBuilder(options);
        Func<PolicySelectorContext, IReadOnlyHeaderPolicyCollection> policySelector = context => context.DefaultPolicy;
    
        // Act
        builder.SetPolicySelector(policySelector);
    
        // Assert
        Assert.Same(options.PolicySelector, policySelector);
    }
    
    [Fact]
    public void SetPolicySelector_WithServiceCollection_SetsPolicySelector()
    {
        // Arrange
        var services = new ServiceCollection();
        var builder = new SecurityHeaderPolicyBuilder(services);
        Func<PolicySelectorContext, IReadOnlyHeaderPolicyCollection> policySelector = context => context.DefaultPolicy;
    
        // Act
        builder.SetPolicySelector(policySelector);
    
        // Assert
        var serviceProvider = services.BuildServiceProvider();
        var options = serviceProvider.GetRequiredService<IOptions<CustomHeaderOptions>>().Value;
        Assert.Same(options.PolicySelector, policySelector);
    }
}