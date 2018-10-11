using Microsoft.AspNetCore.Http;

namespace NetEscapades.AspNetCore.SecurityHeaders.Infrastructure
{
    /// <summary>
    /// Header value for Server header
    /// </summary>
    public class ServerHeader : HeaderPolicyBase
    {
        private readonly string _value;

        /// <summary>
        /// Initializes a new instance of the <see cref="ServerHeader"/> class.
        /// </summary>
        /// <param name="value">The value to apply for the header</param>
        public ServerHeader(string value)
        {
            _value = value;
        }

        /// <inheritdoc />
        public override string Header { get; } = "Server";

        /// <inheritdoc />
        protected override string GetValue(HttpContext context) => _value;

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
            var value = GetValue(context);
            if (string.IsNullOrEmpty(value))
            {
                result.RemoveHeaders.Add(Header);
            }
            else
            {
                result.SetHeaders[Header] = value;
            }
        }
    }
}