using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using NetEscapades.AspNetCore.SecurityHeaders.Headers;
using NetEscapades.AspNetCore.SecurityHeaders.Infrastructure;

namespace NetEscapades.AspNetCore.SecurityHeaders;

/// <summary>
/// An ASP.NET Core middleware for adding security headers.
/// </summary>
internal class EndpointSecurityHeadersMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<EndpointSecurityHeadersMiddleware> _logger;
    private readonly CustomHeaderOptions _options;

    /// <summary>
    /// Initializes a new instance of the <see cref="EndpointSecurityHeadersMiddleware"/> class.
    /// </summary>
    /// <param name="next">The next middleware in the pipeline.</param>
    /// <param name="logger">A logger for recording errors.</param>
    /// <param name="options">Options on how to control the settings that are applied</param>
    public EndpointSecurityHeadersMiddleware(RequestDelegate next, ILogger<EndpointSecurityHeadersMiddleware> logger, CustomHeaderOptions options)
    {
        _next = next ?? throw new ArgumentNullException(nameof(next));
        _logger = logger;
        _options = options;
    }

    /// <summary>
    /// Invoke the middleware
    /// </summary>
    /// <param name="context">The current context</param>
    /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
    public Task Invoke(HttpContext context)
    {
        // Policy resolution rules:
        //
        // 1. If there is an endpoint with a named policy, then fetch that policy
        // 2. Use the provided default policy
        var endpoint = context.GetEndpoint();
        var metadata = endpoint?.Metadata.GetMetadata<ISecurityHeadersPolicyMetadata>();

        if (!string.IsNullOrEmpty(metadata?.PolicyName))
        {
            if (_options.GetPolicy(metadata.PolicyName) is { } namedPolicy)
            {
                context.Items[SecurityHeadersMiddleware.HttpContextKey] = namedPolicy;
            }
            else
            {
                // log that we couldn't find the policy
                _logger.LogWarning(
                    "Error configuring security headers middleware: policy '{PolicyName}' could not be found. "
                    + "Configure the policies for your application by calling AddSecurityHeaderPolicies() on IServiceCollection "
                    + "and adding a policy with the required name.",
                    metadata.PolicyName);
            }
        }

        return _next(context);
    }
}