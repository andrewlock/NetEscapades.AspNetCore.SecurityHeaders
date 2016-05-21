using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Http;
using NetEscapades.AspNetCore.SecurityHeaders.Infrastructure.Constants;

namespace NetEscapades.AspNetCore.SecurityHeaders.Infrastructure
{
    /// <summary>
    /// Default implementation of <see cref="ISecurityHeadersService"/>.
    /// </summary>
    public class SecurityHeadersService : ISecurityHeadersService
    {
        /// <summary>
        /// Evaluates the given <paramref name="policy"/> using the passed in <paramref name="context"/>.
        /// </summary>
        /// <param name="context">The <see cref="HttpContext"/> associated with the call.</param>
        /// <param name="policy">The <see cref="SecurityHeadersPolicy"/> which needs to be evaluated.</param>
        /// <returns>A <see cref="SecurityHeadersResult"/> which contains the result of policy evaluation and can be
        /// used by the caller to set appropriate response headers.</returns>
        public virtual SecurityHeadersResult EvaluatePolicy(HttpContext context, SecurityHeadersPolicy policy)
        {
            if (context == null)
            {
                throw new ArgumentNullException(nameof(context));
            }

            if (policy == null)
            {
                throw new ArgumentNullException(nameof(policy));
            }

            var result = new SecurityHeadersResult();

            if (context.Request.IsHttps)
            {
                EvaluateHttpsRequest(context, policy, result);
            }
            else
            {
                EvaluateHttpRequest(context, policy, result);
            }

            return result;
        }

        /// <summary>
        /// Adds and removes the required headers to the given <paramref name="response"/>.
        /// </summary>
        /// <param name="response">The <see cref="HttpResponse"/> associated with the current call.</param>
        /// <param name="result">The <see cref="SecurityHeadersResult"/> used to read the allowed values.</param>
        public virtual void ApplyResult(HttpResponse response, SecurityHeadersResult result)
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

        public virtual void EvaluateHttpsRequest(HttpContext context, SecurityHeadersPolicy policy, SecurityHeadersResult result)
        {
            EvaluateHttpRequest(context, policy, result);

            //Strict-Transport-Security should only be used over SSL
            if (policy.UseStrictTransportSecurity)
            {
                result.SetHeaders[XssProtectionConstants.Header] = policy.XssProtection;
            }
        }

        public virtual void EvaluateHttpRequest(HttpContext context, SecurityHeadersPolicy policy, SecurityHeadersResult result)
        {
            if (policy.UseXFrameOptions)
            {
                result.SetHeaders[FrameOptionsConstants.Header] = policy.XFramesOptions;
            }

            if (policy.UseXContentTypeOptions)
            {
                result.SetHeaders[ContentTypeOptionsConstants.Header] = policy.XContentTypeOptions;
            }

            if (policy.UseXssProtection)
            {
                result.SetHeaders[XssProtectionConstants.Header] = policy.XssProtection;
            }

            if (policy.RemoveServer)
            {
                result.RemoveHeaders.Add(ServerConstants.Header);
            }
        }

    }
}
