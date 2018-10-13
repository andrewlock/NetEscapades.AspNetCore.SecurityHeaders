using System;
using System.Linq;
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
        private readonly INonceGenerator _nonceGenerator;
        private readonly bool _mustGenerateNonce;

#if NETSTANDARD1_3
        /// <summary>
        /// Initializes a new instance of the <see cref="SecurityHeadersMiddleware"/> class.
        /// </summary>
        /// <param name="next">The next middleware in the pipeline.</param>
        /// <param name="service">An instance of <see cref="ICustomHeaderService"/>.</param>
        /// <param name="policies">A <see cref="HeaderPolicyCollection"/> containing the policies to be applied.</param>
        public SecurityHeadersMiddleware(RequestDelegate next, ICustomHeaderService service, HeaderPolicyCollection policies)
            : this(next, service, policies, new NullNonceGenerator())
        {
            _mustGenerateNonce = false;
        }
#else
        /// <summary>
        /// Initializes a new instance of the <see cref="SecurityHeadersMiddleware"/> class.
        /// </summary>
        /// <param name="next">The next middleware in the pipeline.</param>
        /// <param name="service">An instance of <see cref="ICustomHeaderService"/>.</param>
        /// <param name="policies">A <see cref="HeaderPolicyCollection"/> containing the policies to be applied.</param>
        public SecurityHeadersMiddleware(RequestDelegate next, ICustomHeaderService service, HeaderPolicyCollection policies)
            : this(next, service, policies, new RNGNonceGenerator())
        {
            // TODO: Yuk. Don't want to be generating a noce every request if we don't have to though...
            // Could look at generalising this if we need it for other CSP headers
            _mustGenerateNonce = _policy.Values
                .Where(header => header is ContentSecurityPolicyHeader)
                .Cast<ContentSecurityPolicyHeader>()
                .Any(header => header.HasPerRequestValues);
        }
#endif

        /// <summary>
        /// Initializes a new instance of the <see cref="SecurityHeadersMiddleware"/> class.
        /// </summary>
        /// <param name="next">The next middleware in the pipeline.</param>
        /// <param name="service">An instance of <see cref="ICustomHeaderService"/>.</param>
        /// <param name="policies">A <see cref="HeaderPolicyCollection"/> containing the policies to be applied.</param>
        /// <param name="nonceGenerator">Used to generate nonce (number used once) values for headers</param>
        internal SecurityHeadersMiddleware(RequestDelegate next, ICustomHeaderService service, HeaderPolicyCollection policies, INonceGenerator nonceGenerator)
        {
            _next = next ?? throw new ArgumentNullException(nameof(next));
            _service = service ?? throw new ArgumentNullException(nameof(service));
            _policy = policies ?? throw new ArgumentNullException(nameof(policies));
            _nonceGenerator = nonceGenerator ?? throw new ArgumentException(nameof(nonceGenerator));
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

            if (_mustGenerateNonce)
            {
                context.SetNonce(_nonceGenerator.GetNonce(Constants.DefaultBytesInNonce));
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
