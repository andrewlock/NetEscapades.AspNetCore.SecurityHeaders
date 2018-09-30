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
        /// Create a new policy
        /// </summary>
        /// <param name="value">The value to apply for the header</param>
        protected HeaderPolicyBase(string value)
        {
            Value = value;
        }

        ///<inheritdoc />
        public abstract string Header { get; }

        ///<inheritdoc />
        public string Value { get; set; }

        ///<inheritdoc />
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
            result.SetHeaders[Header] = Value;
        }

        /// <summary>
        /// Apply the header to the result when the provided <paramref see="context" />  has an secure request 
        /// </summary>
        /// <param name="context">The <see cref="HttpContext"/> associated with the current call.</param>
        /// <param name="result">The <see cref="CustomHeadersResult"/> to update.</param>
        protected virtual void EvaluateHttpsRequest(HttpContext context, CustomHeadersResult result)
        {
            result.SetHeaders[Header] = Value;
        }
    }
}