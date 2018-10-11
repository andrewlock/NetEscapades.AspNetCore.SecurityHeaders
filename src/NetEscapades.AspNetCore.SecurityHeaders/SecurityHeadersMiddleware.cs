using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace NetEscapades.AspNetCore.SecurityHeaders.Infrastructure
{
    /// <summary>
    /// An ASP.NET Core middleware for adding security headers.
    /// </summary>
    public class SecurityHeadersMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly HeaderPolicyCollection _policy;
        private readonly ICustomHeaderService _service;

        /// <summary>
        /// Initializes a new instance of the <see cref="SecurityHeadersMiddleware"/> class.
        /// </summary>
        /// <param name="next">The next middleware in the pipeline.</param>
        /// <param name="service">An instance of <see cref="ICustomHeaderService"/>.</param>
        /// <param name="policies">A <see cref="HeaderPolicyCollection"/> containing the policies to be applied.</param>
        public SecurityHeadersMiddleware(RequestDelegate next, ICustomHeaderService service, HeaderPolicyCollection policies)
        {
            _next = next ?? throw new ArgumentNullException(nameof(next));
            _service = service ?? throw new ArgumentNullException(nameof(service));
            _policy = policies ?? throw new ArgumentNullException(nameof(policies));
        }

        /// <summary>
        /// Invoke the middleware
        /// </summary>
        /// <param name="context">The current context</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
        public async Task Invoke(HttpContext context)
        {
            if (context == null)
            {
                throw new ArgumentNullException(nameof(context));
            }

            context.Response.OnStarting(() =>
            {
                var result = _service.EvaluatePolicy(context, _policy);
                _service.ApplyResult(context.Response, result);
#if NET451
                return Task.FromResult(true);
#else
                return Task.CompletedTask;
#endif
            });

            await _next(context);
        }
    }
}
