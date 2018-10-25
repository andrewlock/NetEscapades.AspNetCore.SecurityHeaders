using System;
using Microsoft.AspNetCore.Http;

namespace NetEscapades.AspNetCore.SecurityHeaders.Infrastructure
{
    /// <summary>
    /// The output from calling <see cref="CspBuilder.Build"/>
    /// </summary>
    internal class CspBuilderResult
    {
        private readonly Func<HttpContext, string> _builder;
        private readonly string _constantValue;

        /// <summary>
        /// Initializes a new instance of the <see cref="CspBuilderResult"/> class.
        /// </summary>
        /// <param name="constantValue">The constant value to use for all requests</param>
        public CspBuilderResult(string constantValue)
        {
            _constantValue = constantValue;
            HasPerRequestValues = false;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="CspBuilderResult"/> class.
        /// </summary>
        /// <param name="builder">A builder function to generate the directives for a given request</param>
        public CspBuilderResult(Func<HttpContext, string> builder)
        {
            _builder = builder ?? throw new ArgumentNullException(nameof(builder));
            HasPerRequestValues = true;
        }

        /// <summary>
        /// If true, the header directives are unique per request, and require
        /// runtime formatting (e.g. for use with Nonce)
        /// </summary>
        internal bool HasPerRequestValues { get; }

        /// <summary>
        /// The CSP header value to use
        /// </summary>
        internal string ConstantValue => !HasPerRequestValues
            ? _constantValue
            : throw new InvalidOperationException("Cannot return constant value: CspBuilderResult has dynamic values");

        /// <summary>
        /// A builder function to generate the directives for a given request
        /// </summary>
        internal Func<HttpContext, string> Builder => HasPerRequestValues
            ? _builder
            : throw new InvalidOperationException("Cannot return dynamic builder: CspBuilderResult has constant value");
    }
}