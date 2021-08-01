using System;
using Microsoft.AspNetCore.Http;

namespace NetEscapades.AspNetCore.SecurityHeaders.Headers.CrossOriginPolicies.ResourcePolicy
{
    /// <summary>
    /// More relaxed setting - only allow resources to be loaded from the same domain and sub-domains.
    /// </summary>
    public class SameSiteDirectiveBuilder : CrossOriginResourcePolicyDirectiveBuilderBase
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SameSiteDirectiveBuilder"/> class.
        /// </summary>
        public SameSiteDirectiveBuilder() : base("same-site")
        {
        }

        /// <inheritdoc />
        internal override Func<HttpContext, string> CreateBuilder()
        {
            return ctx => Directive;
        }
    }
}