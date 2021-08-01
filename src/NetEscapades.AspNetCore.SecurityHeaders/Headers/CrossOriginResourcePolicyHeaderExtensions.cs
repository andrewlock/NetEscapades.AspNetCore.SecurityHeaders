using System;
using NetEscapades.AspNetCore.SecurityHeaders.Headers;

// ReSharper disable once CheckNamespace
namespace Microsoft.AspNetCore.Builder
{
    /// <summary>
    /// Extension methods for adding a <see cref="CrossOriginResourcePolicyHeader" /> to a <see cref="HeaderPolicyCollection" />
    /// </summary>
    public static class CrossOriginResourcePolicyHeaderExtensions
    {
        /// <summary>
        /// Add a Cross-Origin-Resource-Policy Header to all requests
        /// </summary>
        /// <param name="policies">The collection of policies</param>
        /// <param name="configure">Configure the CORP</param>
        /// <returns>The <see cref="HeaderPolicyCollection"/> for method chaining</returns>
        public static HeaderPolicyCollection AddCrossOriginResourcePolicy(this HeaderPolicyCollection policies, Action<CrossOriginResourcePolicyBuilder> configure)
        {
            return policies.ApplyPolicy(CrossOriginResourcePolicyHeader.Build(configure));
        }
    }
}