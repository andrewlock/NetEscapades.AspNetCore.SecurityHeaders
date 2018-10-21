using NetEscapades.AspNetCore.SecurityHeaders.Infrastructure;

namespace Microsoft.AspNetCore.Builder
{
    /// <summary>
    /// Extension methods for adding a <see cref="StrictTransportSecurityHeader" /> to a <see cref="HeaderPolicyCollection" />
    /// </summary>
    public static class StrictTransportSecurityHeaderExtensions
    {
        /// <summary>
        /// Add Strict-Transport-Security max-age=<see paramref="maxAge"/> to all requests.
        /// Tells the user-agent to cache the domain in the STS list for the number of seconds provided.
        /// </summary>
        /// <param name="policies">The collection of policies</param>
        /// <param name="maxAgeInSeconds">The maximum number of seconds to cache the domain</param>
        /// <returns>The <see cref="HeaderPolicyCollection"/> for method chaining</returns>
        public static HeaderPolicyCollection AddStrictTransportSecurityMaxAge(this HeaderPolicyCollection policies, int maxAgeInSeconds = StrictTransportSecurityHeader.OneYearInSeconds)
        {
            return policies.ApplyPolicy(new StrictTransportSecurityHeader($"max-age={maxAgeInSeconds}"));
        }

        /// <summary>
        /// Add Strict-Transport-Security max-age=<see paramref="maxAge"/>; includeSubDomains to all requests.
        /// Tells the user-agent to cache the domain in the STS list for the number of seconds provided and include any sub-domains.
        /// </summary>
        /// <param name="policies">The collection of policies</param>
        /// <param name="maxAgeInSeconds">The maximum number of seconds to cache the domain</param>
        /// <returns>The <see cref="HeaderPolicyCollection"/> for method chaining</returns>
        public static HeaderPolicyCollection AddStrictTransportSecurityMaxAgeIncludeSubDomains(this HeaderPolicyCollection policies, int maxAgeInSeconds = StrictTransportSecurityHeader.OneYearInSeconds)
        {
            return policies.ApplyPolicy(new StrictTransportSecurityHeader($"max-age={maxAgeInSeconds}; includeSubDomains"));
        }

        /// <summary>
        /// Add Strict-Transport-Security max-age=0 to all requests.
        /// Tells the user-agent to remove, or not cache the host in the STS cache
        /// </summary>
        /// <param name="policies">The collection of policies</param>
        /// <returns>The <see cref="HeaderPolicyCollection"/> for method chaining</returns>
        public static HeaderPolicyCollection AddStrictTransportSecurityNoCache(this HeaderPolicyCollection policies)
        {
            return policies.ApplyPolicy(new StrictTransportSecurityHeader($"max-age=0"));
        }
    }
}