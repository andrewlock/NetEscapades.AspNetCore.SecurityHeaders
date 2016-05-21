using System.Collections.Generic;

namespace NetEscapades.AspNetCore.SecurityHeaders.Infrastructure
{
    /// <summary>
    /// Defines the policy for customising security headers for a request.
    /// </summary>
    public class SecurityHeadersPolicy
    {
        /// <summary>
        /// The header value to use for X-Frame-Options
        /// </summary>
        public string XFramesOptions { get; set; }

        /// <summary>
        /// The header value to use for XSS-Protection
        /// </summary>
        public string XssProtection { get; set; }

        /// <summary>
        /// The header value to use for X-Content-Type-Options
        /// </summary>
        public string XContentTypeOptions { get; set; }

        /// <summary>
        /// The header value to use for Strict-Transport-Security
        /// </summary>
        public string StrictTransportSecurity { get; set; }

        /// <summary>
        /// Whether to remove the Server tag
        /// </summary>
        public bool RemoveServer { get; set; }

        /// <summary>
        /// Has the X-Frame options header been specified
        /// </summary>
        public bool UseXFrameOptions => !string.IsNullOrWhiteSpace(XFramesOptions);

        /// <summary>
        /// Has the XSS-Protection header been specifed
        /// </summary>
        public bool UseXssProtection => !string.IsNullOrWhiteSpace(XssProtection);

        /// <summary>
        /// Has the X-Content-Type-Options header been specified
        /// </summary>
        public bool UseXContentTypeOptions => !string.IsNullOrWhiteSpace(XContentTypeOptions);

        /// <summary>
        /// Has the Strict-Transport-Security header been specified
        /// </summary>
        public bool UseStrictTransportSecurity => !string.IsNullOrWhiteSpace(StrictTransportSecurity);
    }
}