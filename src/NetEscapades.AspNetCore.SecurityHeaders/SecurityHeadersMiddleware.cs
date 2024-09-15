using System;
using System.Diagnostics.CodeAnalysis;
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
    private readonly ILogger<SecurityHeadersMiddleware> _logger;
    private readonly RequestDelegate _next;
    private readonly IReadOnlyHeaderPolicyCollection _defaultPolicy;
    private readonly NonceGenerator? _nonceGenerator;
    private readonly CustomHeaderOptions _options;

    /// <summary>
    /// Initializes a new instance of the <see cref="SecurityHeadersMiddleware"/> class.
    /// </summary>
    /// <param name="next">The next middleware in the pipeline.</param>
    /// <param name="options">Options on how to control the settings that are applied</param>
    /// <param name="defaultPolicy">A <see cref="HeaderPolicyCollection"/> containing the policy to apply by default.</param>
    /// <param name="logger">A logger for recording errors.</param>
    public SecurityHeadersMiddleware(
        RequestDelegate next,
        IReadOnlyHeaderPolicyCollection defaultPolicy,
        ILogger<SecurityHeadersMiddleware> logger,
        CustomHeaderOptions options)
    {
        _next = next ?? throw new ArgumentNullException(nameof(next));
        _defaultPolicy = defaultPolicy ?? throw new ArgumentNullException(nameof(defaultPolicy));
        _logger = logger;
        _options = options ?? throw new ArgumentNullException(nameof(options));
        _nonceGenerator = MustGenerateNonce(_defaultPolicy) ? new() : null;
    }

    /// <summary>
    /// Invoke the middleware
    /// </summary>
    /// <param name="context">The current context</param>
    /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
    public Task Invoke(HttpContext context)
    {
        // Write into the context, so that subsequent requests can "overwrite" it
        context.Response.OnStarting(OnResponseStarting, Tuple.Create(context, this));
        if (_nonceGenerator is not null)
        {
            context.SetNonce(_nonceGenerator.GetNonce(Constants.DefaultBytesInNonce));
        }

        return _next(context);
    }

    [DoesNotReturn]
    private static void ThrowNull()
    {
        throw new InvalidOperationException($"{nameof(SecurityHeaderPolicyBuilder.SetPolicySelector)} must not return null.");
    }

    private static Task OnResponseStarting(object state)
    {
        var (context, middleware) = (Tuple<HttpContext, SecurityHeadersMiddleware>)state;
        var options = middleware._options;

        // Policy resolution rules:
        //
        // 1. Use the policy returned by options.PolicySelector (if provided)
        // 2. If there is an endpoint with a named policy, then fetch that policy
        // 3. Use the provided default policy
        var endpoint = context.GetEndpoint();
        var metadata = endpoint?.Metadata.GetMetadata<ISecurityHeadersPolicyMetadata>();
        var policyName = metadata?.PolicyName;

        IReadOnlyHeaderPolicyCollection? endpointPolicy = null;

        if (!string.IsNullOrEmpty(policyName))
        {
            if (options.GetPolicy(policyName) is { } policy)
            {
                endpointPolicy = policy;
            }
            else
            {
                // log that we couldn't find the policy
                // clear out policyName so that it matches
                policyName = null;
                middleware._logger.LogWarning(
                    "Error configuring security headers middleware: policy '{PolicyName}' could not be found. "
                    + "Configure the policies for your application by calling AddSecurityHeaderPolicies() on IServiceCollection "
                    + "and adding a policy with the required name.",
                    policyName);
            }
        }

        IReadOnlyHeaderPolicyCollection? policyToApply = null;
        if (options.PolicySelector is not null)
        {
            policyToApply = options.PolicySelector(
                new(context, options.NamedPolicyCollections, middleware._defaultPolicy, policyName, endpointPolicy));

            // ReSharper disable once ConditionIsAlwaysTrueOrFalseAccordingToNullableAPIContract
            if (policyToApply is null)
            {
                // This obviously kills the request, but it's better (from a security PoV)
                // than accidentally not setting any policies
                ThrowNull();
            }
        }

        var result = CustomHeaderService.EvaluatePolicy(context, policyToApply ?? endpointPolicy ?? middleware._defaultPolicy);
        CustomHeaderService.ApplyResult(context.Response, result);

        return Task.CompletedTask;
    }

    private static bool MustGenerateNonce(IReadOnlyHeaderPolicyCollection policy)
    {
        // TODO: Yuk. Don't want to be generating a nonce every request if we don't have to though...
        // Could look at generalising this if we need it for other CSP headers
        return policy.Values
            .OfType<ContentSecurityPolicyHeader>()
            .Any(header => header.HasPerRequestValues);
    }
}