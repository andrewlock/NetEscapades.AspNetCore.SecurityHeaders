using System;
using Microsoft.AspNetCore.Http;

namespace NetEscapades.AspNetCore.SecurityHeaders.Headers.CrossOriginPolicies.OpenerPolicy
{
    /// <summary>
    /// Retains references to newly opened windows or tabs which either don't set COOP or which opt out
    /// of isolation by setting a COOP of unsafe-none.
    /// From: https://developer.mozilla.org/en-US/docs/Web/HTTP/Headers/Cross-Origin-Opener-Policy#directives
    /// </summary>
    public class SameOriginAllowPopupsDirectiveBuilder : CrossOriginOpenerPolicyDirectiveBuilderBase
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SameOriginAllowPopupsDirectiveBuilder"/> class.
        /// </summary>
        public SameOriginAllowPopupsDirectiveBuilder() : base("same-origin-allow-popups")
        {
        }

        /// <inheritdoc />
        internal override Func<HttpContext, string> CreateBuilder()
        {
            return ctx => Directive;
        }
    }
}