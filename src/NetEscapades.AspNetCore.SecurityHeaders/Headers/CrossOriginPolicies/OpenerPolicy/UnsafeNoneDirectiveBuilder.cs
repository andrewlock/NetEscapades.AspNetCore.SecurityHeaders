using System;
using Microsoft.AspNetCore.Http;

namespace NetEscapades.AspNetCore.SecurityHeaders.Headers.CrossOriginPolicies.OpenerPolicy
{
    /// <summary>
    /// This is the default value. Allows the document to be added to its opener's browsing context
    /// group unless the opener itself has a COOP of same-origin or same-origin-allow-popups.
    /// From: https://developer.mozilla.org/en-US/docs/Web/HTTP/Headers/Cross-Origin-Opener-Policy#directives
    /// </summary>
    public class UnsafeNoneDirectiveBuilder : CrossOriginOpenerPolicyDirectiveBuilderBase
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