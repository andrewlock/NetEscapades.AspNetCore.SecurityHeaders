// ReSharper disable once CheckNamespace
using NetEscapades.AspNetCore.SecurityHeaders.Headers.CrossOriginPolicies.EmbedderPolicy;

namespace Microsoft.AspNetCore.Builder
{
    /// <summary>
    /// Used to build a Cross Origin Embedder Policy header from multiple directives.
    /// </summary>
    public class CrossOriginEmbedderPolicyBuilder : CrossOriginPolicyBuilder
    {
        /// <summary>
        /// This is the default value. Allows the document to be added to its opener's browsing context
        /// group unless the opener itself has a COOP of same-origin or same-origin-allow-popups.
        /// From: https://developer.mozilla.org/en-US/docs/Web/HTTP/Headers/Cross-Origin-Opener-Policy#directives
        /// </summary>
        /// <returns>A configured <see cref="UnsafeNoneDirectiveBuilder"/></returns>
        public UnsafeNoneDirectiveBuilder UnsafeNone() => AddDirective(new UnsafeNoneDirectiveBuilder());

        /// <summary>
        /// A document can only load resources from the same origin, or resources explicitly
        /// marked as loadable from another origin.
        /// If a cross origin resource supports CORS, the crossorigin attribute or the
        /// Cross-Origin-Resource-Policy header must be used to load it without being blocked by COEP.
        /// From: https://developer.mozilla.org/en-US/docs/Web/HTTP/Headers/Cross-Origin-Embedder-Policy#directives
        /// </summary>
        /// <returns>A configured <see cref="RequireCorpDirectiveBuilder"/></returns>
        public RequireCorpDirectiveBuilder RequireCorp() => AddDirective(new RequireCorpDirectiveBuilder());
    }
}