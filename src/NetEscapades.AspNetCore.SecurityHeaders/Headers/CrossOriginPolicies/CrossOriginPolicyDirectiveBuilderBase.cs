using System;
using Microsoft.AspNetCore.Http;

namespace NetEscapades.AspNetCore.SecurityHeaders.Headers.CrossOriginPolicies
{
    /// <summary>
    /// Base class for building Cross Origin Policy directives.
    /// </summary>
    public abstract class CrossOriginPolicyDirectiveBuilderBase
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CrossOriginPolicyDirectiveBuilderBase"/> class.
        /// </summary>
        /// <param name="directive">The name of the directive.</param>
        protected CrossOriginPolicyDirectiveBuilderBase(string directive)
        {
            Directive = directive;
        }

        /// <summary>
        /// The name of the directive.
        /// </summary>
        internal string Directive { get; }

        /// <summary>
        /// Create a builder function that can be invoked to find the directive's value
        /// </summary>
        /// <returns>A builder function for generating the directive's value</returns>
        internal abstract Func<HttpContext, string> CreateBuilder();
    }
}