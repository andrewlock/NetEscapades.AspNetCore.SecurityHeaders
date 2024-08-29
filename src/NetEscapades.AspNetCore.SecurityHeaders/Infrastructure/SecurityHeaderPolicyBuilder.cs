using System;
using Microsoft.AspNetCore.Builder;

namespace NetEscapades.AspNetCore.SecurityHeaders.Infrastructure;

/// <summary>
/// Used to configure <see cref="HeaderPolicyCollection"/> for security headers
/// </summary>
public class SecurityHeaderPolicyBuilder
{
    private readonly CustomHeaderOptions _options;

    /// <summary>
    /// Initializes a new instance of the <see cref="SecurityHeaderPolicyBuilder"/> class.
    /// </summary>
    /// <param name="options">The options where the configuration is stored</param>
    internal SecurityHeaderPolicyBuilder(CustomHeaderOptions options)
    {
        _options = options;
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
        var policyCollection = new HeaderPolicyCollection();
        configurePolicy(policyCollection);
        return AddPolicy(name, policyCollection);
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
        _options.NamedPolicyCollections[name] = policyCollection;
        return this;
    }
}