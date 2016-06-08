namespace NetEscapades.AspNetCore.SecurityHeaders.Infrastructure
{
    /// <summary>
    /// The header value to use for XSS-Protection
    /// </summary>
    public class XssProtectionHeader : HeaderPolicyBase
    {
        public XssProtectionHeader(string value) : base(value)
        {
            Value = value;
        }

        /// <inheritdoc />
        public override string Header { get; } = "X-XSS-Protection";

        /// <summary>
        /// Enables the XSS Protections
        /// </summary>
        /// <returns></returns>
        public static XssProtectionHeader Enabled()
        {
            return new XssProtectionHeader("1");
        }

        /// <summary>
        /// Disables the XSS Protections offered by the user-agent.
        /// </summary>
        /// <returns></returns>
        public static XssProtectionHeader Disabled()
        {
            return new XssProtectionHeader("0");
        }

        /// <summary>
        /// Enables XSS protections and instructs the user-agent to block the response in the event that script has been inserted from user input, instead of sanitizing.
        /// </summary>
        /// <returns></returns>
        public static XssProtectionHeader Block()
        {
            return new XssProtectionHeader("1; mode=block");
        }

        /// <summary>
        /// A partially supported directive that tells the user-agent to report potential XSS attacks to a single URL. Data will be POST'd to the report URL in JSON format. 
        /// {0} specifies the report url, including protocol 
        /// </summary>
        /// <param name="reportingUrl">The url to report to</param>
        /// <returns></returns>
        public static XssProtectionHeader Report(string reportingUrl)
        {
            return new XssProtectionHeader($"1; report={reportingUrl}");
        }
    }

}