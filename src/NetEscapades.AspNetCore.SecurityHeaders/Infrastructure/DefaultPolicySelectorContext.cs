using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;

namespace NetEscapades.AspNetCore.SecurityHeaders.Infrastructure;

/// <summary>
/// A context object used during policy selection
/// </summary>
public readonly struct DefaultPolicySelectorContext
{
    /// <summary>
    /// Initializes a new instance of the <see cref="DefaultPolicySelectorContext"/> struct.
    /// </summary>
    /// <param name="httpContext">The current <see cref="HttpContext"/> for the request</param>
    /// <param name="defaultPolicy">The default policy to apply to the request</param>
    internal DefaultPolicySelectorContext(HttpContext httpContext, HeaderPolicyCollection defaultPolicy)
    {
        HttpContext = httpContext;
        DefaultPolicy = defaultPolicy;
    }

    /// <summary>
    /// The current <see cref="HttpContext"/> for the request
    /// </summary>
    public HttpContext HttpContext { get; }

    /// <summary>
    /// The default policy that will be applied to the request
    /// </summary>
    public IReadOnlyHeaderPolicyCollection DefaultPolicy { get; }
}