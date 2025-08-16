using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace NetEscapades.AspNetCore.SecurityHeaders.Infrastructure;

/// <summary>
/// Used to configure <see cref="HeaderPolicyCollection"/> for security headers
/// </summary>
public class SecurityHeaderPolicyBuilder
{
    private readonly IServiceCollection? _services;
    private readonly CustomHeaderOptions? _options;

    /// <summary>
    /// Initializes a new instance of the <see cref="SecurityHeaderPolicyBuilder"/> class.
    /// </summary>
    /// <param name="services">The <see cref="IServiceCollection"/> where the options are configured.</param>
    internal SecurityHeaderPolicyBuilder(IServiceCollection services)
    {
        _services = services;
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="SecurityHeaderPolicyBuilder"/> class.
    /// </summary>
    /// <param name="options">The options to configure.</param>
    internal SecurityHeaderPolicyBuilder(CustomHeaderOptions options)
    {
        _options = options;
    }

    private SecurityHeaderPolicyBuilder ConfigureOptions(Action<CustomHeaderOptions> configureOptions)
    {
        if (_options != null)
        {
            configureOptions(_options);
        }
        else
        {
            _services.Configure(configureOptions);
        }

        return this;
    }

    /// <summary>
    /// Adds a security header policy to the application with the given name.
    /// This name can be used to refer to the policy in endpoints
    /// </summary>
    /// <param name="name">The name of the policy </param>
    /// <param name="configurePolicy">An <see cref="Action{T}"/>to configure the security headers for the policy</param>
    /// <returns>The <see cref="SecurityHeaderPolicyBuilder"/> for chaining</returns>
    public SecurityHeaderPolicyBuilder AddPolicy(string name, Action<HeaderPolicyCollection> configurePolicy)
    {
        return ConfigureOptions(o =>
        {
            var policyCollection = new HeaderPolicyCollection();
            configurePolicy(policyCollection);
            o.NamedPolicyCollections[name] = policyCollection;
        });
    }

    /// <summary>
    /// Adds a security header policy to the application with the given name.
    /// This name can be used to refer to the policy in endpoints
    /// </summary>
    /// <param name="name">The name of the policy </param>
    /// <param name="policyCollection">The security headers for the policy</param>
    /// <returns>The <see cref="SecurityHeaderPolicyBuilder"/> for chaining</returns>
    public SecurityHeaderPolicyBuilder AddPolicy(string name, HeaderPolicyCollection policyCollection)
    {
        return ConfigureOptions(o =>
        {
            o.NamedPolicyCollections[name] = policyCollection;
        });
    }

    /// <summary>
    /// Sets the default security header policy to use when no other named policy is provided
    /// This policy is used wherever a named policy does not apply
    /// </summary>
    /// <param name="configurePolicy">An <see cref="Action{T}"/>to configure the security headers for the policy</param>
    /// <returns>The <see cref="SecurityHeaderPolicyBuilder"/> for chaining</returns>
    public SecurityHeaderPolicyBuilder SetDefaultPolicy(Action<HeaderPolicyCollection> configurePolicy)
    {
        return ConfigureOptions(o =>
        {
            var policyCollection = new HeaderPolicyCollection();
            configurePolicy(policyCollection);
            o.DefaultPolicy = policyCollection;
        });
    }

    /// <summary>
    /// Adds the default security header policy to the application.
    /// This policy is used wherever a named policy does not apply
    /// </summary>
    /// <param name="policyCollection">The security headers for the policy</param>
    /// <returns>The <see cref="SecurityHeaderPolicyBuilder"/> for chaining</returns>
    public SecurityHeaderPolicyBuilder SetDefaultPolicy(HeaderPolicyCollection policyCollection)
    {
        return ConfigureOptions(o =>
        {
            o.DefaultPolicy = policyCollection;
        });
    }

    /// <summary>
    /// Sets the policy selector. Use this to customise a policy before it's applied to the request.
    /// </summary>
    /// <param name="policySelector">A function to invoke just before applying policies to a response,
    /// to select the policy to use. The final policy to execute should be returned from the function.</param>
    /// <returns>The <see cref="SecurityHeaderPolicyBuilder"/> for chaining</returns>
    public SecurityHeaderPolicyBuilder SetPolicySelector(
        Func<PolicySelectorContext, IReadOnlyHeaderPolicyCollection> policySelector)
    {
        return ConfigureOptions(o =>
        {
            o.PolicySelector = policySelector;
        });
    }
}