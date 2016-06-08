using Microsoft.AspNetCore.Http;

namespace NetEscapades.AspNetCore.SecurityHeaders.Infrastructure
{
    /// <summary>
    /// Header value for Server header
    /// </summary>
    public class ServerHeader : HeaderPolicyBase
    {
        public ServerHeader(string value) : base(value)
        {
            Value = value;
        }

        /// <inheritdoc />
        public override string Header { get; } = "Server";

        /// <summary>
        /// Removes the server header from the response
        /// </summary>
        /// <returns></returns>
        public static ServerHeader Remove()
        {
            return new ServerHeader(null);
        }

        protected override void EvaluateHttpRequest(HttpContext context, CustomHeadersResult result)
        {
            EvaluateRequest(context, result);
        }

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