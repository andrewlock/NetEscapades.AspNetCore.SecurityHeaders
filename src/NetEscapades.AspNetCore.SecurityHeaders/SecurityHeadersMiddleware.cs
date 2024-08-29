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
internal class SecurityHeadersMiddleware
{
    private const string HttpContextKey = "__NetEscapades.AspNetCore.SecurityHeaders";
    private readonly RequestDelegate _next;
    private readonly ILogger<SecurityHeadersMiddleware> _logger;
    private readonly CustomHeaderOptions? _options;
    private readonly HeaderPolicyCollection _defaultPolicy;
    private readonly NonceGenerator? _nonceGenerator;

    /// <summary>
    /// Initializes a new instance of the <see cref="SecurityHeadersMiddleware"/> class.
    /// </summary>
    /// <param name="next">The next middleware in the pipeline.</param>
    /// <param name="logger">A logger for recording errors.</param>
    /// <param name="options">Options on how to control the settings that are applied</param>
    /// <param name="defaultPolicy">A <see cref="HeaderPolicyCollection"/> containing the policies to be applied.</param>
    public SecurityHeadersMiddleware(RequestDelegate next, ILogger<SecurityHeadersMiddleware> logger, CustomHeaderOptions? options, HeaderPolicyCollection defaultPolicy)
    {
        _next = next ?? throw new ArgumentNullException(nameof(next));
        _logger = logger;
        _options = options;
        _defaultPolicy = defaultPolicy ?? throw new ArgumentNullException(nameof(defaultPolicy));
        _nonceGenerator = MustGenerateNonce(_defaultPolicy) ? new() : null;
    }

    /// <summary>
    /// Invoke the middleware
    /// </summary>
    /// <param name="context">The current context</param>
    /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
    public async Task Invoke(HttpContext context)
    {
        // Policy resolution rules:
        //
        // 1. If there is an endpoint with a named policy, then fetch that policy
        // 2. Use the provided default policy
        var endpoint = context.GetEndpoint();
        var metadata = endpoint?.Metadata.GetMetadata<ISecurityHeadersPolicyMetadata>();

        HeaderPolicyCollection policy = _defaultPolicy;

        if (!string.IsNullOrEmpty(metadata?.PolicyName))
        {
            if (_options?.GetPolicy(metadata.PolicyName) is { } namedPolicy)
            {
                policy = namedPolicy;
            }
            else
            {
                // log that we couldn't find the policy
                _logger.LogWarning(
                    "Error configuring security headers middleware: policy '{PolicyName}' could not be found. "
                    + "Configure the policies for your application by calling AddSecurityHeaderPolicies() on IServiceCollection "
                    + "and adding a policy with the required name. Using default policy for request",
                    metadata.PolicyName);
            }
        }

        if (context.Items[HttpContextKey] is null)
        {
            context.Response.OnStarting(OnResponseStarting, context);
            if (_nonceGenerator is not null)
            {
                context.SetNonce(_nonceGenerator.GetNonce(Constants.DefaultBytesInNonce));
            }
        }

        // Write into the context, so that subsequent requests can "overwrite" it
        context.Items[HttpContextKey] = policy;

        await _next(context);
    }

    private static Task OnResponseStarting(object state)
    {
        var context = (HttpContext)state;

        if (context.Items[HttpContextKey] is HeaderPolicyCollection policy)
        {
            var result = CustomHeaderService.EvaluatePolicy(context, policy);
            CustomHeaderService.ApplyResult(context.Response, result);
        }

        return Task.CompletedTask;
    }

    private static bool MustGenerateNonce(HeaderPolicyCollection policy)
    {
        // TODO: Yuk. Don't want to be generating a nonce every request if we don't have to though...
        // Could look at generalising this if we need it for other CSP headers
        return policy.Values
            .OfType<ContentSecurityPolicyHeader>()
            .Any(header => header.HasPerRequestValues);
    }
}