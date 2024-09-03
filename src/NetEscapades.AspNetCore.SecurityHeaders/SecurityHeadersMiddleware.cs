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
    /// <summary>
    /// The HttpContext key that tracks the policy to apply
    /// </summary>
    internal const string HttpContextKey = "__NetEscapades.AspNetCore.SecurityHeaders";

    private readonly RequestDelegate _next;
    private readonly HeaderPolicyCollection _defaultPolicy;
    private readonly NonceGenerator? _nonceGenerator;
    private readonly Func<DefaultPolicySelectorContext, IReadOnlyHeaderPolicyCollection> _policySelector;

    /// <summary>
    /// Initializes a new instance of the <see cref="SecurityHeadersMiddleware"/> class.
    /// </summary>
    /// <param name="next">The next middleware in the pipeline.</param>
    /// <param name="policySelector">An optional function to execute to change the policy prior to applying</param>
    /// <param name="defaultPolicy">A <see cref="HeaderPolicyCollection"/> containing the policy to apply by default.</param>
    public SecurityHeadersMiddleware(
        RequestDelegate next,
        Func<DefaultPolicySelectorContext, IReadOnlyHeaderPolicyCollection> policySelector,
        HeaderPolicyCollection defaultPolicy)
    {
        _next = next ?? throw new ArgumentNullException(nameof(next));
        _policySelector = policySelector;
        _defaultPolicy = defaultPolicy ?? throw new ArgumentNullException(nameof(defaultPolicy));
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
        var policyToApply = _policySelector(new(context, _defaultPolicy));
        if (policyToApply is null)
        {
            ThrowNull();
        }

        context.Items[HttpContextKey] = _policySelector(new(context, _defaultPolicy));
        context.Response.OnStarting(OnResponseStarting, context);
        if (_nonceGenerator is not null)
        {
            context.SetNonce(_nonceGenerator.GetNonce(Constants.DefaultBytesInNonce));
        }

        return _next(context);
    }

    private static void ThrowNull()
    {
        throw new InvalidOperationException($"{nameof(SecurityHeaderPolicyBuilder.SetDefaultPolicySelector)} must not return null.");
    }

    private static Task OnResponseStarting(object state)
    {
        var context = (HttpContext)state;

        if (context.Items[HttpContextKey] is IReadOnlyHeaderPolicyCollection policy)
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