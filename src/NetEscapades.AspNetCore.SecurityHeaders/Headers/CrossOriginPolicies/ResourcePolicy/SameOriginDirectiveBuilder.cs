using System;
using Microsoft.AspNetCore.Http;

namespace NetEscapades.AspNetCore.SecurityHeaders.Headers.CrossOriginPolicies.ResourcePolicy
{
    /// <summary>
    /// Most strict setting - only allow resources to be loaded from the same origin.
    /// </summary>
    public class SameOriginDirectiveBuilder : CrossOriginResourcePolicyDirectiveBuilderBase
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