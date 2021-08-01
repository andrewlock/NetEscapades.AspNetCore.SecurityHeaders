using System;
using Microsoft.AspNetCore.Http;

namespace NetEscapades.AspNetCore.SecurityHeaders.Headers.CrossOriginPolicies.OpenerPolicy
{
    /// <summary>
    /// Isolates the browsing context exclusively to same-origin documents.
    /// Cross-origin documents are not loaded in the same browsing context.
    /// From: https://developer.mozilla.org/en-US/docs/Web/HTTP/Headers/Cross-Origin-Opener-Policy#directives
    /// </summary>
    public class SameOriginDirectiveBuilder : CrossOriginOpenerPolicyDirectiveBuilderBase
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SameOriginDirectiveBuilder"/> class.
        /// </summary>
        public SameOriginDirectiveBuilder() : base("same-origin")
        {
        }

        /// <inheritdoc />
        internal override Func<HttpContext, string> CreateBuilder()
        {
            return ctx => Directive;
        }
    }
}