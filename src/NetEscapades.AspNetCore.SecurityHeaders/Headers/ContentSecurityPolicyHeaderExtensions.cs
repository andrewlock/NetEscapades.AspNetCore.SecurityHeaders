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
        /// <param name="asReportOnly">If true, the CSP header is addded as "Content-Security-Policy-Report-Only". If false, it's set to "Content-Security-Policy";</param>
        public static HeaderPolicyCollection AddContentSecurityPolicy(this HeaderPolicyCollection policies, Action<CspBuilder> configure, bool asReportOnly = false)
        {
            return policies.ApplyPolicy(ContentSecurityPolicyHeader.Build(configure, asReportOnly));
        }

        /// <summary>
        /// Add a Content-Security-Header-Report-Only to all requests
        /// </summary>
        /// <param name="policies">The collection of policies</param>
        /// <param name="configure">Configure the CSP</param>
        public static HeaderPolicyCollection AddContentSecurityPolicyReportOnly(this HeaderPolicyCollection policies, Action<CspBuilder> configure)
        {
            return policies.ApplyPolicy(ContentSecurityPolicyHeader.Build(configure, asReportOnly: true));
        }
    }
}