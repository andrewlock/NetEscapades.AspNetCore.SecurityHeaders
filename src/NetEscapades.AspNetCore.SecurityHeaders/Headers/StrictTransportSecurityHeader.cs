using System;
using System.Collections.Generic;
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
        private readonly IReadOnlyList<string> _excludedHosts;

        /// <summary>
        /// Initializes a new instance of the <see cref="StrictTransportSecurityHeader"/> class.
        /// </summary>
        /// <param name="value">The value to apply for the header</param>
        /// <param name="excludedHosts">A collection of hosts that will not have HSTS headers set</param>
        public StrictTransportSecurityHeader(string value, IReadOnlyList<string> excludedHosts)
        {
            _value = value;
            _excludedHosts = excludedHosts;
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

        /// <inheritdoc />
        protected override void EvaluateHttpsRequest(HttpContext context, CustomHeadersResult result)
        {
            if (IsHostExcluded(context.Request.Host.Host))
            {
                return;
            }

            base.EvaluateHttpsRequest(context, result);
        }

        private bool IsHostExcluded(string host)
        {
            if (_excludedHosts == null)
            {
                return false;
            }

            for (var i = 0; i < _excludedHosts.Count; i++)
            {
                if (string.Equals(host, _excludedHosts[i], StringComparison.OrdinalIgnoreCase))
                {
                    return true;
                }
            }

            return false;
        }
    }
}