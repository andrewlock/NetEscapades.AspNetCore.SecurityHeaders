using System;
using NetEscapades.AspNetCore.SecurityHeaders.Headers;

// ReSharper disable once CheckNamespace
namespace Microsoft.AspNetCore.Builder
{
    /// <summary>
    /// Extension methods for adding a <see cref="PermissionsPolicyHeader" /> to a <see cref="HeaderPolicyCollection" />
    /// </summary>
    public static class PermissionsPolicyHeaderExtensions
    {
        /// <summary>
        /// Add a Permissions-Policy header to all requests
        /// </summary>
        /// <param name="policies">The collection of policies</param>
        /// <param name="configure">Configure the Permissions-Policy</param>
        /// <returns>The <see cref="HeaderPolicyCollection"/> for method chaining</returns>
        public static HeaderPolicyCollection AddPermissionsPolicy(this HeaderPolicyCollection policies, Action<PermissionsPolicyBuilder> configure)
        {
            return policies.ApplyPolicy(PermissionsPolicyHeader.Build(configure));
        }
    }
}
