using System;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Primitives;
using System.Collections.Generic;
using System.Linq;

namespace NetEscapades.AspNetCore.SecurityHeaders.Infrastructure
{
    /// <summary>
    /// A base implementation of <see cref="IHeaderPolicy" />
    /// </summary>
    public abstract class HeaderPolicyBase : IHeaderPolicy
    {
        /// <summary>
        /// Headers for forwarded https requests, see https://developer.mozilla.org/en-US/docs/Web/HTTP/Headers/X-Forwarded-Proto
        /// </summary>
        private readonly static Tuple<string, string>[] HttpsForwardedHeaders = new Tuple<string, string>[5]
        {
            Tuple.Create("X-Forwarded-Proto", "https"),
            Tuple.Create("X-Forwarded-Protocol", "https"),
            Tuple.Create("X-Url-Scheme", "https"),
            Tuple.Create("Front-End-Https", "on"),
            Tuple.Create("X-Forwarded-Ssl", "on"),
        };

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
            var request = context?.Request;
            if (context == null)
            {
                throw new ArgumentNullException(nameof(context));
            }

            if (request.IsHttps || IsForwardedHttps(request))
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

        /// <summary>
        /// Check if the if http request was originally https and forwarded by a proxy
        /// See: https://developer.mozilla.org/en-US/docs/Web/HTTP/Headers/X-Forwarded-Proto
        /// </summary>
        /// <param name="request">The http request</param>
        /// <returns>If the request was forwarded from a proxy that had a user perform an https request against it</returns>
        private bool IsForwardedHttps(HttpRequest request)
            => HttpsForwardedHeaders.Any(forwardedHeader =>
            {
                return request.Headers.TryGetValue(forwardedHeader.Item1, out StringValues headerValue) &&
                       headerValue[0] == forwardedHeader.Item2;
            });
    }
}