using System;
using Microsoft.AspNetCore.Http;

namespace NetEscapades.AspNetCore.SecurityHeaders.Infrastructure
{
    /// <summary>
    /// A base implementation of <see cref="IHeaderPolicy" />
    /// </summary>
    public abstract class HeaderPolicyBase : IHeaderPolicy
    {
        /// <summary>
        /// The header value of the policy
        /// </summary>
        /// <returns>The HTTP header name</returns>
        public abstract string Header { get; }

        /// <summary>
        /// The value to set for the header
        /// </summary>
        /// <param name="context">The <see cref="HttpContext"/> for the request</param>
        /// <returns>The HTTP header value</returns>
        protected abstract string GetValue(HttpContext context);

        /// <inheritdoc />
        public void Apply(HttpContext context, CustomHeadersResult result)
        {
            if (context == null)
            {
                throw new ArgumentNullException(nameof(context));
            }

            var request = context.Request;
            if (request.IsHttps)
            {
                EvaluateHttpsRequest(context, result);
            }
            else
            {
                EvaluateHttpRequest(context, result);
            }
        }

        /// <summary>
        /// Apply the header to the result when the provided <paramref see="context" />  has an insecure request
        /// </summary>
        /// <param name="context">The <see cref="HttpContext"/> associated with the current call.</param>
        /// <param name="result">The <see cref="CustomHeadersResult"/> to update.</param>
        protected virtual void EvaluateHttpRequest(HttpContext context, CustomHeadersResult result)
        {
            result.SetHeaders[Header] = GetValue(context);
        }

        /// <summary>
        /// Apply the header to the result when the provided <paramref see="context" />  has an secure request
        /// </summary>
        /// <param name="context">The <see cref="HttpContext"/> associated with the current call.</param>
        /// <param name="result">The <see cref="CustomHeadersResult"/> to update.</param>
        protected virtual void EvaluateHttpsRequest(HttpContext context, CustomHeadersResult result)
        {
            result.SetHeaders[Header] = GetValue(context);
        }
    }
}