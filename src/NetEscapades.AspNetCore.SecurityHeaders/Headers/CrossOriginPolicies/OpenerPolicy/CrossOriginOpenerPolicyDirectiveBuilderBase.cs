using System;
using Microsoft.AspNetCore.Http;

namespace NetEscapades.AspNetCore.SecurityHeaders.Headers.CrossOriginPolicies.OpenerPolicy
{
    /// <summary>
    /// grfr
    /// </summary>
    public abstract class CrossOriginOpenerPolicyDirectiveBuilderBase : CrossOriginPolicyDirectiveBuilderBase
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CrossOriginOpenerPolicyDirectiveBuilderBase"/> class.
        /// </summary>
        /// <param name="directive">The name of the directive</param>
        protected CrossOriginOpenerPolicyDirectiveBuilderBase(string directive) : base(directive)
        {
        }

        /// <inheritdoc />
        internal override abstract Func<HttpContext, string> CreateBuilder();
    }
}
