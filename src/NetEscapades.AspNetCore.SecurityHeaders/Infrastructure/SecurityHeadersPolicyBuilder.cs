using System;
using NetEscapades.AspNetCore.SecurityHeaders.Infrastructure.Constants;

namespace NetEscapades.AspNetCore.SecurityHeaders.Infrastructure
{
    /// <summary>
    /// Exposes methods to build a policy.
    /// </summary>
    public class SecurityHeadersPolicyBuilder
    {
        private readonly SecurityHeadersPolicy _policy = new SecurityHeadersPolicy();

        /// <summary>
        /// The number of seconds in one year
        /// </summary>
        public const int OneYearInSeconds = 60 * 60 * 24 * 365;

        /// <summary>
        /// Add default headers in accordance with the most secure approach
        /// </summary>
        public SecurityHeadersPolicyBuilder AddDefaultSecurePolicy()
        {
            AddFrameOptionsDeny();
            AddXssProtectionBlock();
            AddContentTypeOptionsNoSniff();
            AddStrictTransportSecurityMaxAge();
            RemoveServerHeader();

            return this;
        }

        /// <summary>
        /// Add X-Frame-Options DENY to all requests.
        /// The page cannot be displayed in a frame, regardless of the site attempting to do so
        /// </summary>
        public SecurityHeadersPolicyBuilder AddFrameOptionsDeny()
        {
            _policy.XFramesOptions = FrameOptionsConstants.Deny;
            return this;
        }

        /// <summary>
        /// Add X-Frame-Options SAMEORIGIN to all requests.
        /// The page can only be displayed in a frame on the same origin as the page itself.
        /// </summary>
        public SecurityHeadersPolicyBuilder AddFrameOptionsSameOrigin()
        {
            _policy.XFramesOptions = FrameOptionsConstants.SameOrigin;
            return this;
        }

        /// <summary>
        /// Add X-Frame-Options ALLOW-FROM {uri} to all requests, where the uri is provided
        /// The page can only be displayed in a frame on the specified origin.
        /// </summary>
        /// <param name="uri">The uri of the origin in which the page may be displayed in a frame</param>
        public SecurityHeadersPolicyBuilder AddFrameOptionsSameOrigin(string uri)
        {
            _policy.XFramesOptions = string.Format(FrameOptionsConstants.AllowFromUri, uri);
            return this;
        }

        /// <summary>
        /// Add X-XSS-Protection 1 to all requests.
        /// Enables the XSS Protections
        /// </summary>
        public SecurityHeadersPolicyBuilder AddXssProtectionEnabled()
        {
            _policy.XssProtection = XssProtectionConstants.Enabled;
            return this;
        }

        /// <summary>
        /// Add X-XSS-Protection 0 to all requests.
        /// Disables the XSS Protections offered by the user-agent.
        /// </summary>
        public SecurityHeadersPolicyBuilder AddXssProtectionDisabled()
        {
            _policy.XssProtection = XssProtectionConstants.Disabled;
            return this;
        }

        /// <summary>
        /// Add X-XSS-Protection 1; mode=block to all requests.
        /// Enables XSS protections and instructs the user-agent to block the response in the event that script has been inserted from user input, instead of sanitizing.
        /// </summary>
        public SecurityHeadersPolicyBuilder AddXssProtectionBlock()
        {
            _policy.XssProtection = XssProtectionConstants.Block;
            return this;
        }

        /// <summary>
        /// Add X-XSS-Protection 1; report=http://site.com/report to all requests.
        /// A partially supported directive that tells the user-agent to report potential XSS attacks to a single URL. Data will be POST'd to the report URL in JSON format.
        /// </summary>
        public SecurityHeadersPolicyBuilder AddXssProtectionReport(string reportUrl)
        {
            _policy.XssProtection =
                string.Format(XssProtectionConstants.Report, reportUrl);
            return this;
        }

        /// <summary>
        /// Add X-Content-Type-Options nosniff to all requests.
        /// Can be set to protect against MIME type confusion attacks.
        /// </summary>
        public SecurityHeadersPolicyBuilder AddContentTypeOptionsNoSniff()
        {
            _policy.XContentTypeOptions = ContentTypeOptionsConstants.NoSniff;
            return this;
        }

        /// <summary>
        /// Add Strict-Transport-Security max-age=<see paramref="maxAge"/> to all requests.
        /// Tells the user-agent to cache the domain in the STS list for the number of seconds provided.
        /// </summary>
        public SecurityHeadersPolicyBuilder AddStrictTransportSecurityMaxAge(int maxAge = OneYearInSeconds)
        {
            _policy.StrictTransportSecurity = string.Format(StrictTransportSecurityConstants.MaxAge, maxAge);
            return this;
        }

        /// <summary>
        /// Add Strict-Transport-Security max-age=<see paramref="maxAge"/>; includeSubDomains to all requests.
        /// Tells the user-agent to cache the domain in the STS list for the number of seconds provided and include any sub-domains.
        /// </summary>
        public SecurityHeadersPolicyBuilder AddStrictTransportSecurityMaxAgeIncludeSubDomains(int maxAge = OneYearInSeconds)
        {
            _policy.StrictTransportSecurity = string.Format(StrictTransportSecurityConstants.MaxAgeIncludeSubdomains, maxAge);
            return this;
        }

        /// <summary>
        /// Add Strict-Transport-Security max-age=0 to all requests.
        /// Tells the user-agent to remove, or not cache the host in the STS cache
        /// </summary>
        public SecurityHeadersPolicyBuilder AddStrictTransportSecurityNoCache()
        {
            _policy.StrictTransportSecurity = StrictTransportSecurityConstants.NoCache;
            return this;
        }

        /// <summary>
        /// Removes the Server header from all responses
        /// </summary>
        public SecurityHeadersPolicyBuilder RemoveServerHeader()
        {
            _policy.RemoveServer = true;
            return this;
        }

        /// <summary>
        /// Builds a new <see cref="SecurityHeadersPolicy"/> using the entries added.
        /// </summary>
        /// <returns>The constructed <see cref="SecurityHeadersPolicy"/>.</returns>
        public SecurityHeadersPolicy Build()
        {
            return _policy;
        }
    }
}