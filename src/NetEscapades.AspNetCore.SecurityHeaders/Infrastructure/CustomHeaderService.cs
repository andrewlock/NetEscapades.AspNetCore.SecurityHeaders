using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using NetEscapades.AspNetCore.SecurityHeaders.Headers;

namespace NetEscapades.AspNetCore.SecurityHeaders.Infrastructure
{
    /// <summary>
    /// Default implementation of <see cref="ICustomHeaderService"/>.
    /// </summary>
    public class CustomHeaderService : ICustomHeaderService
    {
        /// <summary>
        /// Evaluates the given <paramref name="policies"/> using the passed in <paramref name="context"/>.
        /// </summary>
        /// <param name="context">The <see cref="HttpContext"/> associated with the call.</param>
        /// <param name="policies">The <see cref="HeaderPolicyCollection"/> containing policies to be evaluated.</param>
        /// <returns>A <see cref="CustomHeadersResult"/> which contains the result of policy evaluation and can be
        /// used by the caller to set appropriate response headers.</returns>
        public virtual CustomHeadersResult EvaluatePolicy(HttpContext context, HeaderPolicyCollection policies)
        {
            if (context == null)
            {
                throw new ArgumentNullException(nameof(context));
            }

            if (policies == null)
            {
                throw new ArgumentNullException(nameof(policies));
            }

            var result = new CustomHeadersResult();
            foreach (var policy in policies.Values)
            {
                if (policy is IDocumentHeaderPolicy documentPolicy)
                {
                    documentPolicy.Apply(context, result, policies);
                }
                else
                {
                    policy.Apply(context, result);
                }
            }

            return result;
        }

        /// <summary>
        /// Adds and removes the required headers to the given <paramref name="response"/>.
        /// </summary>
        /// <param name="response">The <see cref="HttpResponse"/> associated with the current call.</param>
        /// <param name="result">The <see cref="CustomHeadersResult"/> used to read the allowed values.</param>
        public virtual void ApplyResult(HttpResponse response, CustomHeadersResult result)
        {
            if (response == null)
            {
                throw new ArgumentNullException(nameof(response));
            }

            var headers = response.Headers;

            foreach (var headerValuePair in result.SetHeaders)
            {
                headers[headerValuePair.Key] = headerValuePair.Value;
            }

            foreach (var header in result.RemoveHeaders)
            {
                headers.Remove(header);
            }
        }
    }
}
