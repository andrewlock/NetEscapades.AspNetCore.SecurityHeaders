using Microsoft.AspNetCore.Http;
using NetEscapades.AspNetCore.SecurityHeaders.Infrastructure;

namespace NetEscapades.AspNetCore.SecurityHeaders.Headers
{
    /// <summary>
    /// A base implementation of <see cref="IHeaderPolicy" /> that only adds the header for text/html responses
    /// </summary>
    public abstract class HtmlOnlyHeaderPolicyBase : HeaderPolicyBase
    {
        /// <inheritdoc />
        protected override void EvaluateHttpRequest(HttpContext context, CustomHeadersResult result)
        {
            if (context.Response.ContentType?.StartsWith("text/html") ?? true)
            {
                base.EvaluateHttpRequest(context, result);
            }
        }

        /// <inheritdoc />
        protected override void EvaluateHttpsRequest(HttpContext context, CustomHeadersResult result)
        {
            if (context.Response.ContentType?.StartsWith("text/html") ?? true)
            {
                base.EvaluateHttpsRequest(context, result);
            }
        }
    }
}