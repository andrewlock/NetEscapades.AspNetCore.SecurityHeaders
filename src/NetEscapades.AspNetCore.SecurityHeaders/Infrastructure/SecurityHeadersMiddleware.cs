using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace NetEscapades.AspNetCore.SecurityHeaders.Infrastructure
{
    /// <summary>
    /// An ASP.NET middleware for adding security headers.
    /// </summary>
    public class SecurityHeadersMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly SecurityHeadersPolicy _policy;
        private readonly ISecurityHeadersService _service;
        private readonly ISecurityHeadersPolicyProvider _policyProvider;
        private readonly string _policyName;

        /// <summary>
        /// Instantiates a new <see cref="SecurityHeadersMiddleware"/>.
        /// </summary>
        /// <param name="next">The next middleware in the pipeline.</param>
        /// <param name="service">An instance of <see cref="ISecurityHeadersService"/>.</param>
        /// <param name="policyProvider">A policy provider which can get an <see cref="SecurityHeadersPolicy"/>.</param>
        /// <param name="policyName">An optional name of the policy to be fetched..</param>
        public SecurityHeadersMiddleware(RequestDelegate next, ISecurityHeadersService service, ISecurityHeadersPolicyProvider policyProvider, string policyName)
        {
            if (next == null)
            {
                throw new ArgumentNullException(nameof(next));
            }

            if (service == null)
            {
                throw new ArgumentNullException(nameof(service));
            }

            if (policyProvider == null)
            {
                throw new ArgumentNullException(nameof(policyProvider));
            }

            _next = next;
            _service = service;
            _policyProvider = policyProvider;
            _policyName = policyName;
        }

        /// <summary>
        /// Instantiates a new <see cref="SecurityHeadersMiddleware"/>.
        /// </summary>
        /// <param name="next">The next middleware in the pipeline.</param>
        /// <param name="service">An instance of <see cref="ISecurityHeadersService"/>.</param>
        /// <param name="policy">An instance of the <see cref="SecurityHeadersPolicy"/> which can be applied.</param>
        public SecurityHeadersMiddleware(RequestDelegate next, ISecurityHeadersService service, SecurityHeadersPolicy policy)
        {
            if (next == null)
            {
                throw new ArgumentNullException(nameof(next));
            }

            if (service == null)
            {
                throw new ArgumentNullException(nameof(service));
            }
            
            if (policy == null)
            {
                throw new ArgumentNullException(nameof(policy));
            }

            _next = next;
            _service = service;
            _policy = policy;
        }

        /// <summary>
        /// Invoke the middleware
        /// </summary>
        /// <param name="context">The current context</param>
        /// <returns></returns>
        public async Task Invoke(HttpContext context)
        {
            if (context == null)
            {
                throw new ArgumentNullException(nameof(context));
            }

            var policy = _policy ?? await _policyProvider.GetPolicyAsync(context, _policyName);
            if (policy != null)
            {
                var result = _service.EvaluatePolicy(context, policy);
                _service.ApplyResult(context.Response, result);
            }

            await _next(context);
        }

    }
}
