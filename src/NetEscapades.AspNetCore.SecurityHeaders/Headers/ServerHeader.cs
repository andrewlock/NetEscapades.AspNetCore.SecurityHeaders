using Microsoft.AspNetCore.Http;
using System;

namespace NetEscapades.AspNetCore.SecurityHeaders.Infrastructure
{
    /// <summary>
    /// Header value for Server header
    /// </summary>
    public class ServerHeader : HeaderPolicyBase
    {

        /// <inheritdoc />
        public ServerHeader(string value) : base(value)
        {
        }

        /// <inheritdoc />
        public override string Header { get; } = "Server";

        /// <inheritdoc />
        protected override void EvaluateHttpRequest(HttpContext context, CustomHeadersResult result)
        {
            EvaluateRequest(context, result);
        }

        /// <inheritdoc />
        protected override void EvaluateHttpsRequest(HttpContext context, CustomHeadersResult result)
        {
            EvaluateRequest(context, result);
        }

        private void EvaluateRequest(HttpContext context, CustomHeadersResult result)
        {
            if (string.IsNullOrEmpty(Value))
            {
                result.RemoveHeaders.Add(Header);
            }
            else
            {
                result.SetHeaders[Header] = Value;
            }
        }
    }

}