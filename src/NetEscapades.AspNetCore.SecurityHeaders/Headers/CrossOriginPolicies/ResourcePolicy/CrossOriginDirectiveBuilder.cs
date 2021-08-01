using System;
using Microsoft.AspNetCore.Http;

namespace NetEscapades.AspNetCore.SecurityHeaders.Headers.CrossOriginPolicies.ResourcePolicy
{
    /// <summary>
    /// Allows resources to be loaded from other domains.
    /// </summary>
    public class CrossOriginDirectiveBuilder : CrossOriginResourcePolicyDirectiveBuilderBase
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CrossOriginDirectiveBuilder"/> class.
        /// </summary>
        public CrossOriginDirectiveBuilder() : base("cross-origin")
        {
        }

        /// <inheritdoc />
        internal override Func<HttpContext, string> CreateBuilder()
        {
            return ctx => Directive;
        }
    }
}