using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace NetEscapades.AspNetCore.SecurityHeaders.Infrastructure
{
    /// <summary>
    /// An ASP.NET Core middleware for adding security headers.
    /// </summary>
    public class CustomHeadersMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly HeaderPolicyCollection _policy;
        private readonly ICustomHeaderService _service;
        private readonly ICustomHeaderPolicyProvider _policyProvider;
        private readonly string _policyName;

        /// <summary>
        /// Instantiates a new <see cref="CustomHeadersMiddleware"/>.
        /// </summary>
        /// <param name="next">The next middleware in the pipeline.</param>
        /// <param name="service">An instance of <see cref="ICustomHeaderService"/>.</param>
        /// <param name="policyProvider">A policy provider which can get a <see cref="HeaderPolicyCollection"/>.</param>
        /// <param name="policyName">An optional name of the policy to be fetched..</param>
        public CustomHeadersMiddleware(RequestDelegate next, ICustomHeaderService service, ICustomHeaderPolicyProvider policyProvider, string policyName)
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
        /// Instantiates a new <see cref="CustomHeadersMiddleware"/>.
        /// </summary>
        /// <param name="next">The next middleware in the pipeline.</param>
        /// <param name="service">An instance of <see cref="ICustomHeaderService"/>.</param>
        /// <param name="policy">A <see cref="HeaderPolicyCollection"/> containing the policies to be applied.</param>
        public CustomHeadersMiddleware(RequestDelegate next, ICustomHeaderService service, HeaderPolicyCollection policy)
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
