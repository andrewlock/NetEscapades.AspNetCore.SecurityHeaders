using System;
using NetEscapades.AspNetCore.SecurityHeaders.Headers;

// ReSharper disable once CheckNamespace
namespace Microsoft.AspNetCore.Builder;

/// <summary>
/// Extension methods for adding a <see cref="FeaturePolicyHeader" /> to a <see cref="HeaderPolicyCollection" />
/// </summary>
public static class FeaturePolicyHeaderExtensions
{
    /// <summary>
    /// Add a Feature-Policy header to all requests
    /// </summary>
    /// <param name="policies">The collection of policies</param>
    /// <param name="configure">Configure the Feature-Policy</param>
    /// <returns>The <see cref="HeaderPolicyCollection"/> for method chaining</returns>
    [Obsolete("The Feature-Policy header has been deprecated. Use Permissions-Policy instead by calling AddPermissionsPolicy()")]
    public static HeaderPolicyCollection AddFeaturePolicy(this HeaderPolicyCollection policies, Action<FeaturePolicyBuilder> configure)
    {
        return policies.ApplyPolicy(FeaturePolicyHeader.Build(configure));
    }
}