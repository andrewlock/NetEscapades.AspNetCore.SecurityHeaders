using System;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using NetEscapades.AspNetCore.SecurityHeaders;
using NetEscapades.AspNetCore.SecurityHeaders.Infrastructure;

// ReSharper disable once CheckNamespace
namespace Microsoft.AspNetCore.Builder;

/// <summary>
/// The <see cref="IApplicationBuilder"/> extensions for adding Security headers middleware support.
/// </summary>
public static class SecurityHeadersMiddlewareExtensions
{
    /// <summary>
    /// Adds middleware to your web application pipeline to automatically add security headers to requests
    /// </summary>
    /// <param name="app">The IApplicationBuilder passed to your Configure method.</param>
    /// <param name="policies">A configured policy collection to use by default.</param>
    /// <returns>The original app parameter</returns>
    public static IApplicationBuilder UseSecurityHeaders(this IApplicationBuilder app, HeaderPolicyCollection policies)
    {
        if (app == null)
        {
            throw new ArgumentNullException(nameof(app));
        }

        if (policies == null)
        {
            throw new ArgumentNullException(nameof(policies));
        }

        var service = (CustomHeaderOptions)app.ApplicationServices.GetService(typeof(CustomHeaderOptions)) ?? new CustomHeaderOptions();
        return app.UseMiddleware<SecurityHeadersMiddleware>(service, policies);
    }

    /// <summary>
    /// Adds middleware to your web application pipeline to automatically add security headers to requests
    /// </summary>
    /// <param name="app">The IApplicationBuilder passed to your Configure method.</param>
    /// <param name="configure">An <see cref="Action{T}"/>to configure the security headers for the application</param>
    /// <returns>The original app parameter</returns>
    public static IApplicationBuilder UseSecurityHeaders(this IApplicationBuilder app, Action<HeaderPolicyCollection> configure)
    {
        if (configure == null)
        {
            throw new ArgumentNullException(nameof(configure));
        }

        var policies = new HeaderPolicyCollection();
        configure(policies);

        return app.UseSecurityHeaders(policies);
    }

    /// <summary>
    /// Adds middleware to your web application pipeline to automatically add security headers to requests
    ///
    /// Adds a policy collection configured using the default security headers, as in <see cref="HeaderPolicyCollectionExtensions.AddDefaultSecurityHeaders"/>
    /// </summary>
    /// <param name="app">The IApplicationBuilder passed to your Configure method.</param>
    /// <returns>The original app parameter</returns>
    public static IApplicationBuilder UseSecurityHeaders(this IApplicationBuilder app)
    {
        return app.UseSecurityHeaders(policies => policies.AddDefaultSecurityHeaders());
    }

    /// <summary>
    /// Adds middleware to your web application pipeline using the specified policy by default.
    /// </summary>
    /// <param name="app">The IApplicationBuilder passed to your Configure method.</param>
    /// <param name="policyName">The name of the policy to apply</param>
    /// <returns>The original app parameter</returns>
    public static IApplicationBuilder UseSecurityHeaders(this IApplicationBuilder app, string policyName)
    {
        var service = (CustomHeaderOptions)app.ApplicationServices.GetService(typeof(CustomHeaderOptions));
        var policy = service?.GetPolicy(policyName);
        if (policy is null)
        {
            var log = ((ILoggerFactory)app.ApplicationServices.GetRequiredService(typeof(ILoggerFactory))).CreateLogger(typeof(SecurityHeadersMiddlewareExtensions));
            log.LogWarning(
                "Error configuring security headers middleware: policy '{PolicyName}' could not be found. "
                + "Configure the policies for your application by calling AddSecurityHeaderPolicies() on IServiceCollection "
                + "and adding a policy with the required name.",
                policyName);
            return app;
        }

        return app.UseSecurityHeaders(policy);
    }
}