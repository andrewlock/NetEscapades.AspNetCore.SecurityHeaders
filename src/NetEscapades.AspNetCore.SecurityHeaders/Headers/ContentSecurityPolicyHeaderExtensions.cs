using System;
using NetEscapades.AspNetCore.SecurityHeaders.Infrastructure;

namespace NetEscapades.AspNetCore.SecurityHeaders
{
    /// <summary>
    /// Extension methods for adding a <see cref="ContentSecurityPolicyHeader" /> to a <see cref="HeaderPolicyCollection" />
    /// </summary>
    public static class ContentSecurityPolicyHeaderExtensions
    {
        /// <summary>
        /// Add a Content-Security-Header to all requests
        /// </summary>
        /// <param name="policies">The collection of policies</param>
        /// <param name="configure">Configure the CSP</param>
        public static HeaderPolicyCollection AddContentSecurityPolicy(this HeaderPolicyCollection policies, Action<CspBuilder> configure)
        {
            return policies.ApplyPolicy(ContentSecurityPolicyHeader.Build(configure));
        }
    }
}