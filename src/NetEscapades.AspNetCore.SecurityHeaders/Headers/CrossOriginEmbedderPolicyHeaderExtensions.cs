using System;
using NetEscapades.AspNetCore.SecurityHeaders.Headers;

// ReSharper disable once CheckNamespace
namespace Microsoft.AspNetCore.Builder
{
    /// <summary>
    /// Extension methods for adding a <see cref="CrossOriginEmbedderPolicyHeader" /> to a <see cref="HeaderPolicyCollection" />
    /// </summary>
    public static class CrossOriginEmbedderPolicyHeaderExtensions
    {
        /// <summary>
        /// Add a Cross-Origin-Embedder-Policy Header to all requests
        /// </summary>
        /// <param name="policies">The collection of policies</param>
        /// <param name="configure">Configure the COEP</param>
        /// <param name="asReportOnly">If true, the COEP header is addded as "Cross-Origin-Embedder-Policy-Report-Only". If false, it's set to "Cross-Origin-Embedder-Policy";</param>
        /// <returns>The <see cref="HeaderPolicyCollection"/> for method chaining</returns>
        public static HeaderPolicyCollection AddCrossOriginEmbedderPolicy(this HeaderPolicyCollection policies, Action<CrossOriginEmbedderPolicyBuilder> configure, bool asReportOnly = false)
        {
            return policies.ApplyPolicy(CrossOriginEmbedderPolicyHeader.Build(configure, asReportOnly));
        }

        /// <summary>
        /// Add a Cross-Origin-Embedder-Policy Header Report-Only to all requests
        /// </summary>
        /// <param name="policies">The collection of policies</param>
        /// <param name="configure">Configure the COEP</param>
        /// <returns>The <see cref="HeaderPolicyCollection"/> for method chaining</returns>
        public static HeaderPolicyCollection AddCrossOriginEmbedderPolicyReportOnly(this HeaderPolicyCollection policies, Action<CrossOriginEmbedderPolicyBuilder> configure)
        {
            return policies.ApplyPolicy(CrossOriginEmbedderPolicyHeader.Build(configure, asReportOnly: true));
        }
    }
}