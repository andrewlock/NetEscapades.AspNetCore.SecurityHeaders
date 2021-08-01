// ReSharper disable once CheckNamespace
using NetEscapades.AspNetCore.SecurityHeaders.Headers.CrossOriginPolicies.OpenerPolicy;

namespace Microsoft.AspNetCore.Builder
{
    /// <summary>
    /// Used to build a Cross Origin Opener Policy header from multiple directives.
    /// </summary>
    public class CrossOriginOpenerPolicyBuilder : CrossOriginPolicyBuilder
    {
        /// <summary>
        /// This is the default value. Allows the document to be added to its opener's browsing context
        /// group unless the opener itself has a COOP of same-origin or same-origin-allow-popups.
        /// From: https://developer.mozilla.org/en-US/docs/Web/HTTP/Headers/Cross-Origin-Opener-Policy#directives
        /// </summary>
        /// <returns>A configured <see cref="UnsafeNoneDirectiveBuilder"/></returns>
        public UnsafeNoneDirectiveBuilder UnsafeNone() => AddDirective(new UnsafeNoneDirectiveBuilder());

        /// <summary>
        /// Isolates the browsing context exclusively to same-origin documents.
        /// Cross-origin documents are not loaded in the same browsing context.
        /// From: https://developer.mozilla.org/en-US/docs/Web/HTTP/Headers/Cross-Origin-Opener-Policy#directives
        /// </summary>
        /// <returns>A configured <see cref="SameOriginDirectiveBuilder"/></returns>
        public SameOriginDirectiveBuilder SameOrigin() => AddDirective(new SameOriginDirectiveBuilder());

        /// <summary>
        /// Retains references to newly opened windows or tabs which either don't set COOP or which opt out
        /// of isolation by setting a COOP of unsafe-none.
        /// From: https://developer.mozilla.org/en-US/docs/Web/HTTP/Headers/Cross-Origin-Opener-Policy#directives
        /// </summary>
        /// <returns>A configured <see cref="SameOriginAllowPopupsDirectiveBuilder"/></returns>
        public SameOriginAllowPopupsDirectiveBuilder SameOriginAllowPopups() => AddDirective(new SameOriginAllowPopupsDirectiveBuilder());
    }
}