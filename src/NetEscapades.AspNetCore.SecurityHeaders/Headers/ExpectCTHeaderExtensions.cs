using NetEscapades.AspNetCore.SecurityHeaders.Headers;

namespace Microsoft.AspNetCore.Builder
{
    /// <summary>
    /// Extension methods for adding a <see cref="ExpectCTHeader" /> to a <see cref="HeaderPolicyCollection" />
    /// </summary>
    public static class ExpectCTHeaderExtensions
    {
        private static readonly string[] _excludedHosts =
        {
            "localhost",
            "127.0.0.1", // ipv4
            "[::1]" // ipv6
        };

        /// <summary>
        /// Add Expect-CT max-age=<see paramref="maxAge"/> to all HTTPS requests.
        /// Allows sites to opt in to reporting of Certificate Transparency requirements.
        /// Tells the user-agent to cache the domain in the Expect-CT list for the number of seconds provided.
        /// Will not report or enforce Certificate Transparency failures
        /// </summary>
        /// <param name="policies">The collection of policies</param>
        /// <param name="maxAgeInSeconds">Specifies the number of seconds after reception of the Expect-CT header field during
        /// which the user agent should regard the host from whom the message was received as a known Expect-CT host.</param>
        /// <returns>The <see cref="HeaderPolicyCollection"/> for method chaining</returns>
        public static HeaderPolicyCollection AddExpectCTNoEnforceOrReport(
            this HeaderPolicyCollection policies, int maxAgeInSeconds)
        {
            return AddExpectCT(policies, maxAgeInSeconds, reportUri: string.Empty, enforce: false, _excludedHosts);
        }

        /// <summary>
        /// Add Expect-CT max-age=<see paramref="maxAge"/> to all HTTPS requests.
        /// Allows sites to opt in to reporting of Certificate Transparency requirements.
        /// Tells the user-agent to cache the domain in the Expect-CT list for the number of seconds provided
        /// and to enforce Certificate Transparency failures
        /// </summary>
        /// <param name="policies">The collection of policies</param>
        /// <param name="maxAgeInSeconds">Specifies the number of seconds after reception of the Expect-CT header field during
        /// which the user agent should regard the host from whom the message was received as a known Expect-CT host.</param>
        /// <returns>The <see cref="HeaderPolicyCollection"/> for method chaining</returns>
        public static HeaderPolicyCollection AddExpectCTEnforceOnly(
            this HeaderPolicyCollection policies, int maxAgeInSeconds)
        {
            return AddExpectCT(policies, maxAgeInSeconds, reportUri: string.Empty, enforce: true, _excludedHosts);
        }

        /// <summary>
        /// Add Expect-CT max-age=<see paramref="maxAge"/> to all HTTPS requests.
        /// Allows sites to opt in to reporting of Certificate Transparency requirements.
        /// Tells the user-agent to cache the domain in the Expect-CT list for the number of seconds provided
        /// and to send failures to the provided <paramref name="reportUri"/>
        /// </summary>
        /// <param name="policies">The collection of policies</param>
        /// <param name="maxAgeInSeconds">Specifies the number of seconds after reception of the Expect-CT header field during
        /// which the user agent should regard the host from whom the message was received as a known Expect-CT host.</param>
        /// <param name="reportUri">Specifies the URI to which the user agent should report Expect-CT failures.</param>
        /// <returns>The <see cref="HeaderPolicyCollection"/> for method chaining</returns>
        public static HeaderPolicyCollection AddExpectCTReportOnly(
            this HeaderPolicyCollection policies, int maxAgeInSeconds, string reportUri)
        {
            if (string.IsNullOrWhiteSpace(reportUri))
            {
                throw new System.ArgumentException("Uri may not be null or empty", nameof(reportUri));
            }

            return AddExpectCT(policies, maxAgeInSeconds, reportUri, enforce: false, _excludedHosts);
        }

        /// <summary>
        /// Add Expect-CT max-age=<see paramref="maxAge"/> to all HTTPS requests.
        /// Allows sites to opt in to reporting of Certificate Transparency requirements.
        /// Tells the user-agent to cache the domain in the Expect-CT list for the number of seconds provided,
        /// to send failures to the provided <paramref name="reportUri"/>, and to enforce Certificate Transparency failures
        /// </summary>
        /// <param name="policies">The collection of policies</param>
        /// <param name="maxAgeInSeconds">Specifies the number of seconds after reception of the Expect-CT header field during
        /// which the user agent should regard the host from whom the message was received as a known Expect-CT host.</param>
        /// <param name="reportUri">Specifies the URI to which the user agent should report Expect-CT failures.</param>
        /// <returns>The <see cref="HeaderPolicyCollection"/> for method chaining</returns>
        public static HeaderPolicyCollection AddExpectCTEnforceAndReport(
            this HeaderPolicyCollection policies, int maxAgeInSeconds, string reportUri)
        {
            if (string.IsNullOrWhiteSpace(reportUri))
            {
                throw new System.ArgumentException("Uri may not be null or empty", nameof(reportUri));
            }

            return AddExpectCT(policies, maxAgeInSeconds, reportUri, enforce: true, _excludedHosts);
        }

        /// <summary>
        /// Add Expect-CT max-age=<see paramref="maxAge"/> to all HTTPS requests.
        /// Allows sites to opt in to reporting and/or enforcement of Certificate Transparency requirements.
        /// Tells the user-agent to cache the domain in the Expect-CT list for the number of seconds provided.
        /// </summary>
        /// <param name="policies">The collection of policies</param>
        /// <param name="maxAgeInSeconds">Specifies the number of seconds after reception of the Expect-CT header field during
        /// which the user agent should regard the host from whom the message was received as a known Expect-CT host.</param>
        /// <param name="reportUri">Specifies the URI to which the user agent should report Expect-CT failures.</param>
        /// <param name="enforce">If true, signals to the user agent that compliance with the Certificate Transparency policy should be enforced
        /// (rather than only reporting compliance) and that the user agent should refuse future connections that violate its
        /// Certificate Transparency policy.</param>
        /// <param name="excludedHosts">A collection of host names that will not add the Expect-CT header</param>
        /// <remarks>When both the enforce directive and the report-uri directive are present, the configuration is referred to as an "enforce-and-report" configuration, signalling
        /// to the user agent both that compliance to the Certificate Transparency policy should be enforced and that violations should be reported.</remarks>
        /// <returns>The <see cref="HeaderPolicyCollection"/> for method chaining</returns>
        public static HeaderPolicyCollection AddExpectCT(
            this HeaderPolicyCollection policies, int maxAgeInSeconds, string reportUri, bool enforce, params string[] excludedHosts)
        {
            var enforceDirective = enforce ? ", enforce" : string.Empty;
            var reportUriDirective = string.IsNullOrEmpty(reportUri) ? string.Empty : $", report-uri=\"{reportUri}\"";
            return policies.ApplyPolicy(new ExpectCTHeader($"max-age={maxAgeInSeconds}{enforceDirective}{reportUriDirective}", excludedHosts));
        }
    }
}