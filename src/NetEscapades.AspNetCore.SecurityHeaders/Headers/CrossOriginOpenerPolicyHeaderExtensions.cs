using System;
using NetEscapades.AspNetCore.SecurityHeaders.Headers;

// ReSharper disable once CheckNamespace
namespace Microsoft.AspNetCore.Builder
{
    /// <summary>
    /// Extension methods for adding a <see cref="CrossOriginOpenerPolicyHeader" /> to a <see cref="HeaderPolicyCollection" />
    /// </summary>
    public static class CrossOriginOpenerPolicyHeaderExtensions
    {
        /// <summary>
        /// Add a Cross-Origin-Opener-Policy Header to all requests
        /// </summary>
        /// <param name="policies">The collection of policies</param>
        /// <param name="configure">Configure the COOP</param>
        /// <param name="asReportOnly">If true, the COOP header is addded as "Cross-Origin-Opener-Policy-Report-Only". If false, it's set to "Cross-Origin-Opener-Policy";</param>
        /// <returns>The <see cref="HeaderPolicyCollection"/> for method chaining</returns>
        public static HeaderPolicyCollection AddCrossOriginOpenerPolicy(this HeaderPolicyCollection policies, Action<CrossOriginOpenerPolicyBuilder> configure, bool asReportOnly = false)
        {
            return policies.ApplyPolicy(CrossOriginOpenerPolicyHeader.Build(configure, asReportOnly));
        }

        /// <summary>
        /// Add a Cross-Origin-Opener-Policy Header Report-Only to all requests
        /// </summary>
        /// <param name="policies">The collection of policies</param>
        /// <param name="configure">Configure the COOP</param>
        /// <returns>The <see cref="HeaderPolicyCollection"/> for method chaining</returns>
        public static HeaderPolicyCollection AddCrossOriginOpenerPolicyReportOnly(this HeaderPolicyCollection policies, Action<CrossOriginOpenerPolicyBuilder> configure)
        {
            return policies.ApplyPolicy(CrossOriginOpenerPolicyHeader.Build(configure, asReportOnly: true));
        }
    }
}