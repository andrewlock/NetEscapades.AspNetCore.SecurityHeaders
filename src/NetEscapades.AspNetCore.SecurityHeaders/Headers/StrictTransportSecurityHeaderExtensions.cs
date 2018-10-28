using NetEscapades.AspNetCore.SecurityHeaders.Headers;

// ReSharper disable once CheckNamespace
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
            return policies.AddStrictTransportSecurity(maxAgeInSeconds, includeSubdomains: false, preload: false);
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
            return policies.AddStrictTransportSecurity(maxAgeInSeconds, includeSubdomains: true, preload: false);
        }

        /// <summary>
        /// Add Strict-Transport-Security max-age=<see paramref="maxAge"/>; includeSubDomains to all requests.
        /// Tells the user-agent to cache the domain in the STS list for the number of seconds provided and include any sub-domains. Additionally, enable preloading of the site in the HSTS preload list
        ///
        /// WARNING:Sending the preload directive from your site can have PERMANENT CONSEQUENCES and prevent users from accessing your site and any of its subdomains if you find you need to switch back to HTTP.
        /// </summary>
        /// <param name="policies">The collection of policies</param>
        /// <param name="maxAgeInSeconds">The maximum number of seconds to cache the domain</param>
        /// <returns>The <see cref="HeaderPolicyCollection"/> for method chaining</returns>
        public static HeaderPolicyCollection AddStrictTransportSecurityMaxAgeIncludeSubDomainsAndPreload(this HeaderPolicyCollection policies, int maxAgeInSeconds = StrictTransportSecurityHeader.OneYearInSeconds)
        {
            return policies.AddStrictTransportSecurity(maxAgeInSeconds, includeSubdomains: true, preload: true);
        }

        /// <summary>
        /// Add Strict-Transport-Security to all HTTPS requests.
        /// Tells the user-agent to cache the domain in the STS list for the number of seconds provided.
        /// </summary>
        /// <param name="policies">The collection of policies</param>
        /// <param name="maxAgeInSeconds">The maximum number of seconds to cache the domain</param>
        /// <param name="includeSubdomains">If true, includes sub-domains in the STS list</param>
        /// <param name="preload">If true, enable preloading of the site in the HSTS preload list
        /// WARNING:Sending the preload directive from your site can have PERMANENT CONSEQUENCES and prevent users from accessing your site and any of its subdomains if you find you need to switch back to HTTP.</param>
        /// <returns>The <see cref="HeaderPolicyCollection"/> for method chaining</returns>
        public static HeaderPolicyCollection AddStrictTransportSecurity(this HeaderPolicyCollection policies, int maxAgeInSeconds, bool includeSubdomains, bool preload)
        {
            var subdomainsDirective = includeSubdomains ? "; includeSubDomains" : string.Empty;
            var preloadDirective = preload ? "; preload" : string.Empty;
            return policies.ApplyPolicy(new StrictTransportSecurityHeader($"max-age={maxAgeInSeconds}{subdomainsDirective}{preloadDirective}"));
        }

        /// <summary>
        /// Add Strict-Transport-Security max-age=0 to all requests.
        /// Tells the user-agent to remove, or not cache the host in the STS cache
        /// </summary>
        /// <param name="policies">The collection of policies</param>
        /// <returns>The <see cref="HeaderPolicyCollection"/> for method chaining</returns>
        public static HeaderPolicyCollection AddStrictTransportSecurityNoCache(this HeaderPolicyCollection policies)
        {
            return policies.AddStrictTransportSecurity(0, includeSubdomains: false, preload: false);
        }
    }
}