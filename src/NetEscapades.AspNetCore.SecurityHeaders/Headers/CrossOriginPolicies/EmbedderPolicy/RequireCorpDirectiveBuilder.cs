using System;
using Microsoft.AspNetCore.Http;

namespace NetEscapades.AspNetCore.SecurityHeaders.Headers.CrossOriginPolicies.EmbedderPolicy
{
    /// <summary>
    /// A document can only load resources from the same origin, or resources explicitly
    /// marked as loadable from another origin.
    /// If a cross origin resource supports CORS, the crossorigin attribute or the
    /// Cross-Origin-Resource-Policy header must be used to load it without being blocked by COEP.
    /// From: https://developer.mozilla.org/en-US/docs/Web/HTTP/Headers/Cross-Origin-Embedder-Policy#directives
    /// </summary>
    public class RequireCorpDirectiveBuilder : CrossOriginEmbedderPolicyDirectiveBuilderBase
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="RequireCorpDirectiveBuilder"/> class.
        /// </summary>
        public RequireCorpDirectiveBuilder() : base("require-corp")
        {
        }

        /// <inheritdoc />
        internal override Func<HttpContext, string> CreateBuilder()
        {
            return ctx => Directive;
        }
    }
}