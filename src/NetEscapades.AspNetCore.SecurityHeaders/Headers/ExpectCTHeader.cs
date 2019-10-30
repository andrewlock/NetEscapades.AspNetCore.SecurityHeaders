using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Http;
using NetEscapades.AspNetCore.SecurityHeaders.Infrastructure;

namespace NetEscapades.AspNetCore.SecurityHeaders.Headers
{
    /// <summary>
    /// The header value to use for Expect-CT
    /// </summary>
    public class ExpectCTHeader : HeaderPolicyBase
    {
        private readonly string _value;
        private readonly IReadOnlyList<string> _excludedHosts;

        /// <summary>
        /// Initializes a new instance of the <see cref="ExpectCTHeader"/> class.
        /// </summary>
        /// <param name="value">The value to apply for the header</param>
        /// <param name="excludedHosts">A collection of hosts that will not have Expect CT headers set</param>
        public ExpectCTHeader(string value, IReadOnlyList<string> excludedHosts)
        {
            _value = value;
            _excludedHosts = excludedHosts;
        }

        /// <inheritdoc />
        public override string Header { get; } = "Expect-Ct";

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
            if (_excludedHosts is null)
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