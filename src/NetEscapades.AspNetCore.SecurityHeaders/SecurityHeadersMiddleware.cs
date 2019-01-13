using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using NetEscapades.AspNetCore.SecurityHeaders.Headers;
using NetEscapades.AspNetCore.SecurityHeaders.Infrastructure;

namespace NetEscapades.AspNetCore.SecurityHeaders
{
    /// <summary>
    /// An ASP.NET Core middleware for adding security headers.
    /// </summary>
    public class SecurityHeadersMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly HeaderPolicyCollection _policy;
        private readonly string _policyName;
        private readonly NonceGenerator _nonceGenerator;
        private readonly bool _mustGenerateNonce;

        /// <summary>
        /// Initializes a new instance of the <see cref="SecurityHeadersMiddleware"/> class.
        /// </summary>
        /// <param name="next">The next middleware in the pipeline.</param>
        /// <param name="service">An instance of <see cref="ICustomHeaderService"/>.</param>
        public SecurityHeadersMiddleware(RequestDelegate next, ICustomHeaderService service)
            : this(next, service, policyName: null)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="SecurityHeadersMiddleware"/> class.
        /// </summary>
        /// <param name="next">The next middleware in the pipeline.</param>
        /// <param name="service">An instance of <see cref="ICustomHeaderService"/>.</param>
        /// /// <param name="policyName">An optional name of the policy to be fetched.</param>
        public SecurityHeadersMiddleware(RequestDelegate next, ICustomHeaderService service, string policyName)
            : this(next, service, new NonceGenerator())
        {
            _policyName = policyName;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="SecurityHeadersMiddleware"/> class.
        /// </summary>
        /// <param name="next">The next middleware in the pipeline.</param>
        /// <param name="service">An instance of <see cref="ICustomHeaderService"/>.</param>
        /// <param name="policies">A <see cref="HeaderPolicyCollection"/> containing the policies to be applied.</param>
        public SecurityHeadersMiddleware(RequestDelegate next, ICustomHeaderService service, HeaderPolicyCollection policies)
            : this(next, service, new NonceGenerator())
        {
            _policy = policies ?? throw new ArgumentNullException(nameof(policies));
            _mustGenerateNonce = MustGenerateNonce(policies);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="SecurityHeadersMiddleware"/> class.
        /// </summary>
        /// <param name="next">The next middleware in the pipeline.</param>
        /// <param name="service">An instance of <see cref="ICustomHeaderService"/>.</param>
        /// <param name="nonceGenerator">Used to generate nonce (number used once) values for headers</param>
        internal SecurityHeadersMiddleware(RequestDelegate next, ICustomHeaderService service, NonceGenerator nonceGenerator)
        {
            _next = next ?? throw new ArgumentNullException(nameof(next));
            CustomHeaderService = service ?? throw new ArgumentNullException(nameof(service));
            _nonceGenerator = nonceGenerator ?? throw new ArgumentException(nameof(nonceGenerator));
        }

        private ICustomHeaderService CustomHeaderService { get; }

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

            var policy = _policy;
            var policyName = _policyName;
            var mustGenerateNonce = _mustGenerateNonce;

            if (policy == null)
            {
                // Resolve policy by name if the local policy is not being used
                var securityHeadersPolicyProvider = context.RequestServices.GetRequiredService<ISecurityHeadersPolicyProvider>();
                policy = await securityHeadersPolicyProvider.GetPolicyAsync(context, policyName);
                if (policy != null)
                {
                    mustGenerateNonce = MustGenerateNonce(policy);
                }
            }

            if (policy == null)
            {
                await _next(context);
                return;
            }

            if (mustGenerateNonce)
            {
                context.SetNonce(_nonceGenerator.GetNonce(Constants.DefaultBytesInNonce));
            }

            context.Response.OnStarting(OnResponseStarting, Tuple.Create(this, context, policy));
            await _next(context);
        }

        private static Task OnResponseStarting(object state)
        {
            var tuple = (Tuple<SecurityHeadersMiddleware, HttpContext, HeaderPolicyCollection>)state;
            var middleware = tuple.Item1;
            var context = tuple.Item2;
            var policy = tuple.Item3;

            var result = middleware.CustomHeaderService.EvaluatePolicy(context, policy);
            middleware.CustomHeaderService.ApplyResult(context.Response, result);

#if NET451
            return Task.FromResult(true);
#else
            return Task.CompletedTask;
#endif
        }

        private static bool MustGenerateNonce(HeaderPolicyCollection policy)
        {
            // TODO: Yuk. Don't want to be generating a nonce every request if we don't have to though...
            // Could look at generalising this if we need it for other CSP headers
            return policy.Values
                .OfType<ContentSecurityPolicyHeader>()
                .Any(header => header.HasPerRequestValues);
        }
    }
}
