using NetEscapades.AspNetCore.SecurityHeaders.Infrastructure;

namespace Microsoft.AspNetCore.Builder
{
    /// <summary>
    /// Utility class exposing common extension methods
    /// </summary>
    public static class HeaderPolicyCollectionExtensions
    {
        /// <summary>
        /// Add the policy to the policy collection
        /// </summary>
        /// <param name="policies">The <see cref="HeaderPolicyCollection" /> to add the deafult security header policies too</param>
        /// <param name="policy">The policy to add to the collection</param>
        /// <returns>The <see cref="HeaderPolicyCollection" /> for method chaining</returns>
        internal static HeaderPolicyCollection ApplyPolicy(
            this HeaderPolicyCollection policies,
            IHeaderPolicy policy)
        {
            policies[policy.Header] = policy;
            return policies;
        }

        /// <summary>
        /// Add default headers in accordance with the most secure approach
        /// </summary>
        /// <param name="policies">The <see cref="HeaderPolicyCollection" /> to add the deafult security header policies too</param>
        /// <returns>The <see cref="HeaderPolicyCollection" /> for method chaining</returns>
        public static HeaderPolicyCollection AddDefaultSecurityHeaders(this HeaderPolicyCollection policies)
        {
            policies.AddFrameOptionsDeny();
            policies.AddXssProtectionBlock();
            policies.AddContentTypeOptionsNoSniff();
            policies.AddStrictTransportSecurityMaxAge();
            policies.AddReferrerPolicyStrictOriginWhenCrossOrigin();
            policies.RemoveServerHeader();
            policies.AddContentSecurityPolicy(builder =>
            {
                builder.AddObjectSrc().None();
                builder.AddFormAction().Self();
                builder.AddFrameAncestors().None();
            });
            return policies;
        }
    }
}