using Microsoft.AspNetCore.Http;
using NetEscapades.AspNetCore.SecurityHeaders.Infrastructure;

namespace NetEscapades.AspNetCore.SecurityHeaders.Headers
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
        private readonly string _value;

        /// <summary>
        /// Initializes a new instance of the <see cref="StrictTransportSecurityHeader"/> class.
        /// </summary>
        /// <param name="value">The value to apply for the header</param>
        public StrictTransportSecurityHeader(string value)
        {
            _value = value;
        }

        /// <inheritdoc />
        public override string Header { get; } = "Strict-Transport-Security";

        /// <inheritdoc />
        protected override string GetValue(HttpContext context) => _value;

        /// <inheritdoc />
        protected override void EvaluateHttpRequest(HttpContext context, CustomHeadersResult result)
        {
            return;
        }
    }
}