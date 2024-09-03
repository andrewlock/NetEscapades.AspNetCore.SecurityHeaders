using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;

namespace NetEscapades.AspNetCore.SecurityHeaders.Infrastructure;

/// <summary>
/// A context object used during policy selection
/// </summary>
public readonly struct EndpointPolicySelectorContext
{
    /// <summary>
    /// Initializes a new instance of the <see cref="EndpointPolicySelectorContext"/> struct.
    /// </summary>
    /// <param name="httpContext">The current <see cref="HttpContext"/> for the request</param>
    /// <param name="selectedPolicyName">The name of the selected policy for the endpoint</param>
    /// <param name="selectedPolicy">The selected policy for the endpoint</param>
    internal EndpointPolicySelectorContext(HttpContext httpContext, string selectedPolicyName, IReadOnlyHeaderPolicyCollection selectedPolicy)
    {
        HttpContext = httpContext;
        SelectedPolicyName = selectedPolicyName;
        SelectedPolicy = selectedPolicy;
    }

    /// <summary>
    /// The current <see cref="HttpContext"/> for the request
    /// </summary>
    public HttpContext HttpContext { get; }

    /// <summary>
    /// The name of the selected policy for the endpoint
    /// </summary>
    public string SelectedPolicyName { get; }

    /// <summary>
    /// The selected policy for the endpoint
    /// </summary>
    public IReadOnlyHeaderPolicyCollection SelectedPolicy { get; }
}