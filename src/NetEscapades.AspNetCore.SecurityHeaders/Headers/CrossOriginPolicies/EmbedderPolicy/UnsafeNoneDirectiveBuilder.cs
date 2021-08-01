using System;
using Microsoft.AspNetCore.Http;

namespace NetEscapades.AspNetCore.SecurityHeaders.Headers.CrossOriginPolicies.EmbedderPolicy
{
    /// <summary>
    /// This is the default value. Allows the document to fetch cross-origin resources without giving
    /// explicit permission through the CORS protocol or the Cross-Origin-Resource-Policy header.
    /// From: https://developer.mozilla.org/en-US/docs/Web/HTTP/Headers/Cross-Origin-Embedder-Policy#directives
    /// </summary>
    public class UnsafeNoneDirectiveBuilder : CrossOriginEmbedderPolicyDirectiveBuilderBase
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="UnsafeNoneDirectiveBuilder"/> class.
        /// </summary>
        public UnsafeNoneDirectiveBuilder() : base("unsafe-none")
        {
        }

        /// <inheritdoc />
        internal override Func<HttpContext, string> CreateBuilder()
        {
            return ctx => Directive;
        }
    }
}