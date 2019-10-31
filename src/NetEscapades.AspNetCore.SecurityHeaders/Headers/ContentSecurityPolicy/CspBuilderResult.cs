using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;

namespace NetEscapades.AspNetCore.SecurityHeaders.Headers.ContentSecurityPolicy
{
    /// <summary>
    /// The output from calling <see cref="CspBuilder.Build"/>
    /// </summary>
    internal abstract class CspBuilderResult
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CspBuilderResult"/> class.
        /// </summary>
        /// <param name="hasPerRequestValues">If true, the header directives are unique per request, and require
        /// runtime formatting (e.g. for use with Nonce)</param>
        protected CspBuilderResult(bool hasPerRequestValues)
        {
            HasPerRequestValues = hasPerRequestValues;
        }

        /// <summary>
        /// If true, the header directives are unique per request, and require
        /// runtime formatting (e.g. for use with Nonce)
        /// </summary>
        internal bool HasPerRequestValues { get; }

        /// <summary>
        /// The CSP header value to use
        /// </summary>
        internal abstract string ConstantValue { get; }

        /// <summary>
        /// A builder function to generate the directives for a given request
        /// </summary>
        internal abstract Func<HttpContext, string> Builder { get; }

        /// <summary>
        /// Create a new <see cref="CspBuilderResult"/> with a constant value
        /// </summary>
        /// <param name="constantValue">The constant value to use for all requests</param>
        /// <returns>The <see cref="CspBuilderResult"/></returns>
        public static CspBuilderResult CreateStaticResult(string constantValue) =>
            new StaticCspBuilderResult(constantValue);

        /// <summary>
        /// Create a new <see cref="CspBuilderResult"/> that varies per request
        /// </summary>
        /// <param name="builder">A builder function to generate the directives for a given request</param>
        /// <returns>The <see cref="CspBuilderResult"/></returns>
        public static CspBuilderResult CreateDynamicResult(Func<HttpContext, string> builder) =>
            new DynamicCspBuilderResult(builder);

        /// <summary>
        /// The output from calling <see cref="CspBuilder.Build"/>
        /// </summary>
        private class StaticCspBuilderResult : CspBuilderResult
        {
            private readonly string _constantValue;

            /// <summary>
            /// Initializes a new instance of the <see cref="CspBuilderResult.StaticCspBuilderResult"/> class.
            /// </summary>
            /// <param name="constantValue">The constant value to use for all requests</param>
            public StaticCspBuilderResult(string constantValue) : base(false)
            {
                _constantValue = constantValue ?? throw new ArgumentNullException(nameof(constantValue));
            }

            /// <summary>
            /// The CSP header value to use
            /// </summary>
            internal override string ConstantValue => _constantValue;

            /// <summary>
            /// A builder function to generate the directives for a given request
            /// </summary>
            internal override Func<HttpContext, string> Builder =>
                throw new InvalidOperationException("Cannot return dynamic builder: CspBuilderResult has constant value");
        }

        /// <summary>
        /// The output from calling <see cref="CspBuilder.Build"/>
        /// </summary>
        private class DynamicCspBuilderResult : CspBuilderResult
        {
            private readonly Func<HttpContext, string> _builder;

            /// <summary>
            /// Initializes a new instance of the <see cref="CspBuilderResult.DynamicCspBuilderResult"/> class.
            /// </summary>
            /// <param name="builder">A builder function to generate the directives for a given request</param>
            public DynamicCspBuilderResult(Func<HttpContext, string> builder) : base(true)
            {
                _builder = builder ?? throw new ArgumentNullException(nameof(builder));
            }

            /// <summary>
            /// The CSP header value to use
            /// </summary>
            internal override string ConstantValue =>
                throw new InvalidOperationException("Cannot return constant value: CspBuilderResult has dynamic values");

            /// <summary>
            /// A builder function to generate the directives for a given request
            /// </summary>
            internal override Func<HttpContext, string> Builder => _builder;
        }
    }
}