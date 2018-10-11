using Microsoft.AspNetCore.Http;

namespace NetEscapades.AspNetCore.SecurityHeaders.Infrastructure
{
    /// <summary>
    /// Header value for Server header
    /// </summary>
    public class RemoveCustomHeader : HeaderPolicyBase
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="RemoveCustomHeader"/> class.
        /// </summary>
        /// <param name="header">The string value of the header to remove</param>
        public RemoveCustomHeader(string header)
        {
            Header = header;
        }

        /// <inheritdoc />
        public override string Header { get; }

        /// <inheritdoc />
        protected override string GetValue(HttpContext context) => string.Empty;

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
            result.RemoveHeaders.Add(Header);
        }
    }
}