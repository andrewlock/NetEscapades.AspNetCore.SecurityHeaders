using Microsoft.AspNetCore.Http;
using System;

namespace NetEscapades.AspNetCore.SecurityHeaders.Infrastructure
{
    /// <summary>
    /// The header value to use for Strict-Transport-Security
    /// </summary>
    public class StrictTransportSecurityHeader : HeaderPolicyBase
    {        
        /// <inheritdoc />
        public StrictTransportSecurityHeader(string value) : base(value)
        {
        }

        /// <summary>
        /// The number of seconds in one year
        /// </summary>
        public const int OneYearInSeconds = 60 * 60 * 24 * 365;

        ///<inheritdoc />
        public override string Header { get; } = "Strict-Transport-Security";

        /// <inheritdoc />
        protected override void EvaluateHttpRequest(HttpContext context, CustomHeadersResult result)
        {
            return;
        }
    }
}