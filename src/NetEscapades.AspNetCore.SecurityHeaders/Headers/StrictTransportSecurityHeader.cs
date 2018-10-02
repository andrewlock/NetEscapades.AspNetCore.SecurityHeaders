using Microsoft.AspNetCore.Http;

namespace NetEscapades.AspNetCore.SecurityHeaders.Infrastructure
{
    /// <summary>
    /// The header value to use for Strict-Transport-Security
    /// </summary>
    public class StrictTransportSecurityHeader : HeaderPolicyBase
    {
        /// <summary>
        /// The number of seconds in one year
        /// </summary>
        public const int OneYearInSeconds = 60 * 60 * 24 * 365;

        /// <summary>
        /// Initializes a new instance of the <see cref="StrictTransportSecurityHeader"/> class.
        /// </summary>
        /// <param name="value">The value to apply for the header</param>
        public StrictTransportSecurityHeader(string value) : base(value)
        {
        }

        /// <inheritdoc />
        public override string Header { get; } = "Strict-Transport-Security";

        /// <inheritdoc />
        protected override void EvaluateHttpRequest(HttpContext context, CustomHeadersResult result)
        {
            return;
        }
    }
}