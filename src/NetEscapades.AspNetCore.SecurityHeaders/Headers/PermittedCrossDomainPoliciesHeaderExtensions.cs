using Microsoft.AspNetCore.Builder;
using NetEscapades.AspNetCore.SecurityHeaders.Headers;

// ReSharper disable once CheckNamespace
namespace Microsoft.AspNetCore.Builder
{
    /// <summary>
    /// Extension methods for adding a <see cref="PermittedCrossDomainPoliciesHeader" /> to a <see cref="HeaderPolicyCollection" />
    /// </summary>
    public static class PermittedCrossDomainPoliciesHeaderExtensions
    {
        /// <summary>
        /// Add X-Permitted-Cross-Domain-Policies none to all requests.
        /// Disables PDF and Flash embedding.
        /// </summary>
        /// <param name="policies">The collection of policies</param>
        /// <returns>The <see cref="HeaderPolicyCollection"/> for method chaining</returns>
        public static HeaderPolicyCollection AddPermittedCrossDomainPoliciesNone(this HeaderPolicyCollection policies)
        {
            return policies.ApplyPolicy(new PermittedCrossDomainPoliciesHeader("none"));
        }

        /// <summary>
        /// Add X-Permitted-Cross-Domain-Policies master-only to all requests.
        /// Disables PDF and Flash embedding but allows the /crossdomain.xml.
        /// </summary>
        /// <param name="policies">The collection of policies</param>
        /// <returns>The <see cref="HeaderPolicyCollection"/> for method chaining</returns>
        public static HeaderPolicyCollection AddPermittedCrossDomainPoliciesMasterOnly(this HeaderPolicyCollection policies)
        {
            return policies.ApplyPolicy(new PermittedCrossDomainPoliciesHeader("master-only"));
        }

        /// <summary>
        /// Add X-Permitted-Cross-Domain-Policies by-content-type to all requests.
        /// Disables PDF and Flash embedding but allows the files with the content type "text/x-cross-domain-policy".
        /// </summary>
        /// <param name="policies">The collection of policies</param>
        /// <returns>The <see cref="HeaderPolicyCollection"/> for method chaining</returns>
        public static HeaderPolicyCollection AddPermittedCrossDomainPoliciesByContentType(this HeaderPolicyCollection policies)
        {
            return policies.ApplyPolicy(new PermittedCrossDomainPoliciesHeader("by-content-type"));
        }

        /// <summary>
        /// Add X-Permitted-Cross-Domain-Policies none to all requests.
        /// Allows PDF and Flash embedding for all policy files.
        /// </summary>
        /// <param name="policies">The collection of policies</param>
        /// <returns>The <see cref="HeaderPolicyCollection"/> for method chaining</returns>
        public static HeaderPolicyCollection AddPermittedCrossDomainPoliciesAll(this HeaderPolicyCollection policies)
        {
            return policies.ApplyPolicy(new PermittedCrossDomainPoliciesHeader("all"));
        }
    }
}