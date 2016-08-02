namespace NetEscapades.AspNetCore.SecurityHeaders.Infrastructure
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
        /// <param name="maxAge">The maximum number of seconds to cache the domain</param>
        public static HeaderPolicyCollection AddStrictTransportSecurityMaxAge(this HeaderPolicyCollection policies, int maxAge = StrictTransportSecurityHeader.OneYearInSeconds)
        {
            return policies.ApplyPolicy(StrictTransportSecurityHeader.MaxAge(maxAge));
        }

        /// <summary>
        /// Add Strict-Transport-Security max-age=<see paramref="maxAge"/>; includeSubDomains to all requests.
        /// Tells the user-agent to cache the domain in the STS list for the number of seconds provided and include any sub-domains.
        /// </summary>
        /// <param name="policies">The collection of policies</param>
        /// <param name="maxAge">The maximum number of seconds to cache the domain</param>
        public static HeaderPolicyCollection AddStrictTransportSecurityMaxAgeIncludeSubDomains(this HeaderPolicyCollection policies, int maxAge = StrictTransportSecurityHeader.OneYearInSeconds)
        {
            return policies.ApplyPolicy(StrictTransportSecurityHeader.MaxAgeIncludeSubdomains(maxAge));
        }

        /// <summary>
        /// Add Strict-Transport-Security max-age=0 to all requests.
        /// Tells the user-agent to remove, or not cache the host in the STS cache
        /// </summary>
        /// <param name="policies">The collection of policies</param>
        public static HeaderPolicyCollection AddStrictTransportSecurityNoCache(this HeaderPolicyCollection policies)
        {
            return policies.ApplyPolicy(StrictTransportSecurityHeader.NoCache());
        }
    }

}