using System;
using Microsoft.AspNetCore.Http;

namespace NetEscapades.AspNetCore.SecurityHeaders.Infrastructure
{
    /// <summary>
    /// Base class for building CSP directives.
    /// </summary>
    public abstract class CspDirectiveBuilderBase
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CspDirectiveBuilderBase"/> class.
        /// </summary>
        /// <param name="directive">The name of the directive.</param>
        public CspDirectiveBuilderBase(string directive)
        {
            Directive = directive;
        }

        /// <summary>
        /// The name of the directive.
        /// </summary>
        internal string Directive { get; }

        /// <summary>
        /// If true, the directive value is unique per request, and must be executed
        /// with an <see cref="HttpContext"/> (e.g. for use with Nonce). If not the value
        /// is fixed, and does not rely on the <see cref="HttpContext"/>
        /// </summary>
        internal virtual bool HasPerRequestValues { get; } = false;

        /// <summary>
        /// Create a builder function that can be invoked to find the directive's value
        /// If <see cref="HasPerRequestValues"/> is <code>false</code>, can be executed
        /// ahead of time with a null parameter
        /// </summary>
        /// <returns>A builder function for generating the directive's value</returns>
        internal abstract Func<HttpContext, string> CreateBuilder();
    }
}