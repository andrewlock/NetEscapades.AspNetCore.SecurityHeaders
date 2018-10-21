using NetEscapades.AspNetCore.SecurityHeaders.Infrastructure;

namespace Microsoft.AspNetCore.Builder
{
    /// <summary>
    /// Extension methods for adding a <see cref="XssProtectionHeader" /> to a <see cref="HeaderPolicyCollection" />
    /// </summary>
    public static class XssProtectionHeaderExtensions
    {
        /// <summary>
        /// Add X-XSS-Protection 1 to all requests.
        /// Enables the XSS Protections
        /// </summary>
        /// <param name="policies">The collection of policies</param>
        /// <returns>The <see cref="HeaderPolicyCollection"/> for method chaining</returns>
        public static HeaderPolicyCollection AddXssProtectionEnabled(this HeaderPolicyCollection policies)
        {
            return policies.ApplyPolicy(new XssProtectionHeader("1"));
        }

        /// <summary>
        /// Add X-XSS-Protection 0 to all requests.
        /// Disables the XSS Protections offered by the user-agent.
        /// </summary>
        /// <param name="policies">The collection of policies</param>
        /// <returns>The <see cref="HeaderPolicyCollection"/> for method chaining</returns>
        public static HeaderPolicyCollection AddXssProtectionDisabled(this HeaderPolicyCollection policies)
        {
            return policies.ApplyPolicy(new XssProtectionHeader("0"));
        }

        /// <summary>
        /// Add X-XSS-Protection 1; mode=block to all requests.
        /// Enables XSS protections and instructs the user-agent to block the response in the event that script has been inserted from user input, instead of sanitizing.
        /// </summary>
        /// <param name="policies">The collection of policies</param>
        /// <returns>The <see cref="HeaderPolicyCollection"/> for method chaining</returns>
        public static HeaderPolicyCollection AddXssProtectionBlock(this HeaderPolicyCollection policies)
        {
            return policies.ApplyPolicy(new XssProtectionHeader("1; mode=block"));
        }

        /// <summary>
        /// Add X-XSS-Protection 1; report=http://site.com/report to all requests.
        /// A partially supported directive that tells the user-agent to report potential XSS attacks to a single URL. Data will be POST'd to the report URL in JSON format.
        /// </summary>
        /// <param name="policies">The collection of policies</param>
        /// <param name="reportUrl">The url to report potential XSS attacks to</param>
        /// <returns>The <see cref="HeaderPolicyCollection"/> for method chaining</returns>
        public static HeaderPolicyCollection AddXssProtectionReport(this HeaderPolicyCollection policies, string reportUrl)
        {
            return policies.ApplyPolicy(new XssProtectionHeader($"1; report={reportUrl}"));
        }
    }
}
