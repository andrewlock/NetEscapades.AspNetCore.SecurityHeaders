namespace NetEscapades.AspNetCore.SecurityHeaders.Infrastructure.Constants
{
    /// <summary>
    /// X-XSS-Protection-related constants.
    /// </summary>
    public static class XssProtectionConstants
    {
        /// <summary>
        /// Header value for X-XSS-Protection
        /// </summary>
        public static readonly string Header = "X-XSS-Protection";

        /// <summary>
        /// Enables the XSS Protections
        /// </summary>
        public static readonly string Enabled = "1";

        /// <summary>
        /// Disables the XSS Protections offered by the user-agent.
        /// </summary>
        public static readonly string Disabled = "0";

        /// <summary>
        /// Enables XSS protections and instructs the user-agent to block the response in the event that script has been inserted from user input, instead of sanitizing.
        /// </summary>
        public static readonly string Block = "1; mode=block";

        /// <summary>
        /// A partially supported directive that tells the user-agent to report potential XSS attacks to a single URL. Data will be POST'd to the report URL in JSON format. 
        /// {0} specifies the report url, including protocol
        /// </summary>
        public static readonly string Report = "1; report={0}";
    }
}