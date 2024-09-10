using Microsoft.AspNetCore.Http;

namespace NetEscapades.AspNetCore.SecurityHeaders.Infrastructure;

/// <summary>
/// A context object used during policy selection
/// </summary>
public readonly struct PolicySelectorContext
{
    /// <summary>
    /// Initializes a new instance of the <see cref="PolicySelectorContext"/> struct.
    /// </summary>
    /// <param name="httpContext">The current <see cref="HttpContext"/> for the request</param>
    /// <param name="defaultPolicy">The default policy for the endpoint</param>
    /// <param name="endpointPolicyName">The name of the selected policy for the endpoint</param>
    /// <param name="endpointPolicy">The endpoint policy for the endpoint</param>
    internal PolicySelectorContext(
        HttpContext httpContext,
        IReadOnlyHeaderPolicyCollection defaultPolicy,
        string? endpointPolicyName,
        IReadOnlyHeaderPolicyCollection? endpointPolicy)
    {
        HttpContext = httpContext;
        DefaultPolicy = defaultPolicy;
        EndpointPolicyName = endpointPolicyName;
        EndpointPolicy = endpointPolicy;
    }

    /// <summary>
    /// The current <see cref="HttpContext"/> for the request
    /// </summary>
    public HttpContext HttpContext { get; }

    /// <summary>
    /// The default policy that applies to the request.
    /// </summary>
    public IReadOnlyHeaderPolicyCollection DefaultPolicy { get; }

    /// <summary>
    /// The endpoint policy name that applies to the request, if any.
    /// If no endpoint-specific policy applies to the request, returns <c>null</c>.
    /// </summary>
    public string? EndpointPolicyName { get; }

    /// <summary>
    /// The endpoint policy that applies to the request, if any.
    /// If no endpoint-specific policy applies to the request, returns <c>null</c>.
    /// </summary>
    public IReadOnlyHeaderPolicyCollection? EndpointPolicy { get; }

    /// <summary>
    /// The policy that will be applied to the endpoint
    /// </summary>
    public IReadOnlyHeaderPolicyCollection SelectedPolicy => EndpointPolicy ?? DefaultPolicy;
}