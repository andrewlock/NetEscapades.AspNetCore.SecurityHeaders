using NetEscapades.AspNetCore.SecurityHeaders.Headers;

// ReSharper disable once CheckNamespace
namespace Microsoft.AspNetCore.Builder;

/// <summary>
/// Extension methods for adding a <see cref="ServerHeader" /> to a <see cref="HeaderPolicyCollection" />
/// </summary>
public static class RemoveCustomHeaderExtensions
{
    /// <summary>
    /// Remove a custom header from all requests
    /// </summary>
    /// <param name="policies">The collection of policies</param>
    /// <param name="header">The header value to remove</param>
    /// <returns>The <see cref="HeaderPolicyCollection"/> for method chaining</returns>
    public static HeaderPolicyCollection RemoveCustomHeader(this HeaderPolicyCollection policies, string header)
    {
        return policies.ApplyPolicy(new RemoveCustomHeader(header));
    }
}