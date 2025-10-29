using System;
using System.Threading.Tasks;
using FluentAssertions;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using NetEscapades.AspNetCore.SecurityHeaders.Infrastructure;

namespace NetEscapades.AspNetCore.SecurityHeaders.Test;

public class SecurityHeadersMiddlewareExtensionsTests
{
    [Test]
    public void AddSecurityHeaderPolicies_NullPolicyByDefault()
    {
        var serviceCollection = new ServiceCollection();
        var provider = serviceCollection.BuildServiceProvider();
        var opts = SecurityHeadersMiddlewareExtensions.GetOptions(provider);
        
        opts.Should().BeNull();
    }

    [Test]
    [MatrixDataSource]
    public void AddSecurityHeaderPolicies_OverwritesPreviousDefault(
        [Matrix] RegistrationType first,
        [Matrix] RegistrationType second)
    {
        var serviceCollection = new ServiceCollection();
        HeaderPolicyCollection? policy1 = null;
        switch (first)
        {
            case RegistrationType.Direct:
                serviceCollection.AddSecurityHeaderPolicies().SetDefaultPolicy(p => { policy1 = p; });
                break;
            case RegistrationType.Func:
                serviceCollection.AddSecurityHeaderPolicies(o => o.SetDefaultPolicy(p => { policy1 = p; }));
                break;
            case RegistrationType.FuncWithProvider:
                serviceCollection.AddSecurityHeaderPolicies((o, _) => o.SetDefaultPolicy(p => { policy1 = p; }));
                break;
        }
        
        HeaderPolicyCollection? policy2 = null;
        switch (first)
        {
            case RegistrationType.Direct:
                serviceCollection.AddSecurityHeaderPolicies().SetDefaultPolicy(p => { policy2 = p; });
                break;
            case RegistrationType.Func:
                serviceCollection.AddSecurityHeaderPolicies(o => o.SetDefaultPolicy(p => { policy2 = p; }));
                break;
            case RegistrationType.FuncWithProvider:
                serviceCollection.AddSecurityHeaderPolicies((o, _) => o.SetDefaultPolicy(p => { policy2 = p; }));
                break;
        }

        var provider = serviceCollection.BuildServiceProvider();
        var opts = SecurityHeadersMiddlewareExtensions.GetOptions(provider);
        
        policy1.Should().NotBeNull();
        policy2.Should().NotBeNull();
        opts.Should().NotBeNull();
        opts.DefaultPolicy.Should().BeSameAs(policy2);
    }

    [Test]
    public void AddSecurityHeaderPolicies_OverwritesMultiplePreviousDefault()
    {
        var serviceCollection = new ServiceCollection();
        HeaderPolicyCollection? policy1 = null;
        serviceCollection.AddSecurityHeaderPolicies().SetDefaultPolicy(p => { policy1 = p; });
        HeaderPolicyCollection? policy2 = null;
        serviceCollection.AddSecurityHeaderPolicies(o => o.SetDefaultPolicy(p => { policy2 = p; }));
        HeaderPolicyCollection? policy3 = null;
        serviceCollection.AddSecurityHeaderPolicies((o, _) => o.SetDefaultPolicy(p => { policy3 = p; }));

        var provider = serviceCollection.BuildServiceProvider();
        var opts = SecurityHeadersMiddlewareExtensions.GetOptions(provider);
        
        policy1.Should().NotBeNull();
        policy2.Should().NotBeNull();
        policy3.Should().NotBeNull();
        opts.Should().NotBeNull();
        opts.DefaultPolicy.Should().BeSameAs(policy3);
    }

    [Test]
    [MatrixDataSource]
    public void AddSecurityHeaderPolicies_OverwritesPreviousPolicySelector(
        [Matrix] RegistrationType first,
        [Matrix] RegistrationType second)
    {
        var serviceCollection = new ServiceCollection();
        Func<PolicySelectorContext, Task<IReadOnlyHeaderPolicyCollection>> selector1 = x => Task.FromResult(x.DefaultPolicy);
        Func<PolicySelectorContext, Task<IReadOnlyHeaderPolicyCollection>> selector2 = x => Task.FromResult(x.DefaultPolicy);
        SetSelector(serviceCollection, first, selector1);
        SetSelector(serviceCollection, second, selector2);
        var provider = serviceCollection.BuildServiceProvider();
        var opts = SecurityHeadersMiddlewareExtensions.GetOptions(provider);
        
        opts.Should().NotBeNull();
        opts.PolicySelector.Should().BeSameAs(selector2);

        static void SetSelector(ServiceCollection services, RegistrationType type,
            Func<PolicySelectorContext, Task<IReadOnlyHeaderPolicyCollection>> selector)
        {
            switch (type)
            {
                case RegistrationType.Direct:
                    services.AddSecurityHeaderPolicies().SetPolicySelector(selector);
                    break;
                case RegistrationType.Func:
                    services.AddSecurityHeaderPolicies(o => o.SetPolicySelector(selector));
                    break;
                case RegistrationType.FuncWithProvider:
                    services.AddSecurityHeaderPolicies((o, _) => o.SetPolicySelector(selector));
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(type), type, null);
            }
        }
    }

    [Test]
    public void AddSecurityHeaderPolicies_OverwritesAndIncludesPreviousNamedPolicies()
    {
        var serviceCollection = new ServiceCollection();

        var name1 = "name1";
        var name2 = "name2";
        var name3 = "name3";
        var replacement = new HeaderPolicyCollection();
        serviceCollection.AddSecurityHeaderPolicies().AddPolicy(name1, new HeaderPolicyCollection());
        serviceCollection.AddSecurityHeaderPolicies(o => o.AddPolicy(name2, new HeaderPolicyCollection()));
        serviceCollection.AddSecurityHeaderPolicies((o, _) => o.AddPolicy(name3, new HeaderPolicyCollection()));
        serviceCollection.AddSecurityHeaderPolicies().AddPolicy(name3, replacement);

        var provider = serviceCollection.BuildServiceProvider();
        var opts = SecurityHeadersMiddlewareExtensions.GetOptions(provider);
        
        opts.Should().NotBeNull();
        opts.NamedPolicyCollections.Should().ContainKeys(name1, name2, name3).And.HaveCount(3);
        opts.NamedPolicyCollections[name3].Should().BeSameAs(replacement);
    }

    public enum RegistrationType
    {
        Direct,
        Func,
        FuncWithProvider
    }
}