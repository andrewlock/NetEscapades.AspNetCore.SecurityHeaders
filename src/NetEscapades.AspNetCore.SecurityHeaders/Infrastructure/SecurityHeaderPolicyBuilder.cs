using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;

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

    /// <summary>
    /// Sets the default security header policy to use when no other named policy is provided
    /// This policy is used wherever a named policy does not apply
    /// </summary>
    /// <param name="configurePolicy">An <see cref="Action{T}"/>to configure the security headers for the policy</param>
    /// <returns>The <see cref="SecurityHeaderPolicyBuilder"/> for chaining</returns>
    public SecurityHeaderPolicyBuilder SetDefaultPolicy(Action<HeaderPolicyCollection> configurePolicy)
    {
        var policyCollection = new HeaderPolicyCollection();
        configurePolicy(policyCollection);
        return SetDefaultPolicy(policyCollection);
    }

    /// <summary>
    /// Adds the default security header policy to the application.
    /// This policy is used wherever a named policy does not apply
    /// </summary>
    /// <param name="policyCollection">The security headers for the policy</param>
    /// <returns>The <see cref="SecurityHeaderPolicyBuilder"/> for chaining</returns>
    public SecurityHeaderPolicyBuilder SetDefaultPolicy(HeaderPolicyCollection policyCollection)
    {
        _options.DefaultPolicy = policyCollection;
        return this;
    }

    /// <summary>
    /// Sets the policy selector. Use this to customise what is the default policy selected for a given request.
    /// This is invoked for every request.
    /// </summary>
    /// <param name="policySelector">A function to invoke when the <see cref="SecurityHeadersMiddleware"/> executes,
    /// to select the policy to use. The final policy to execute should be returned from the function.</param>
    /// <returns>The <see cref="SecurityHeaderPolicyBuilder"/> for chaining</returns>
    public SecurityHeaderPolicyBuilder SetDefaultPolicySelector(
        Func<DefaultPolicySelectorContext, IReadOnlyHeaderPolicyCollection> policySelector)
    {
        _options.DefaultPolicySelector = policySelector;
        return this;
    }

    /// <summary>
    /// Sets the policy selector. Use this to customise which policy is selected for a given endpoint.
    /// This is only called  when an endpoint-specific policy is selected.
    /// </summary>
    /// <param name="policySelector">A function to invoke when the <see cref="EndpointSecurityHeadersMiddleware"/> executes,
    /// to select the policy to use. The final policy to execute should be returned from the function.</param>
    /// <returns>The <see cref="SecurityHeaderPolicyBuilder"/> for chaining</returns>
    public SecurityHeaderPolicyBuilder SetEndpointPolicySelector(
        Func<EndpointPolicySelectorContext, IReadOnlyHeaderPolicyCollection> policySelector)
    {
        _options.EndpointPolicySelector = policySelector;
        return this;
    }
}