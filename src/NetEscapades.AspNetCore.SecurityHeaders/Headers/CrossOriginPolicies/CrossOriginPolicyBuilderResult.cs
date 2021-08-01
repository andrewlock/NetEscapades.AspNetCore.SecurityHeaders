using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;

namespace NetEscapades.AspNetCore.SecurityHeaders.Headers.CrossOriginPolicies
{
    /// <summary>
    /// The output from calling <see cref="CrossOriginPolicyBuilder.Build"/>
    /// </summary>
    internal abstract class CrossOriginPolicyBuilderResult
    {
        /// <summary>
        /// The Cross Origin Policy header value to use
        /// </summary>
        internal abstract string ConstantValue { get; }

        /// <summary>
        /// A builder function to generate the directives for a given request
        /// </summary>
        internal abstract Func<HttpContext, string> Builder { get; }

        /// <summary>
        /// Create a new <see cref="CrossOriginPolicyBuilderResult"/> with a constant value
        /// </summary>
        /// <param name="constantValue">The constant value to use for all requests</param>
        /// <returns>The <see cref="CrossOriginPolicyBuilderResult"/></returns>
        public static CrossOriginPolicyBuilderResult CreateStaticResult(string constantValue) =>
            new StaticCrossOriginPolicyBuilderResult(constantValue);

        /// <summary>
        /// The output from calling <see cref="CrossOriginPolicyBuilder.Build"/>
        /// </summary>
        private class StaticCrossOriginPolicyBuilderResult : CrossOriginPolicyBuilderResult
        {
            private readonly string _constantValue;

            /// <summary>
            /// Initializes a new instance of the <see cref="CrossOriginPolicyBuilderResult.StaticCrossOriginPolicyBuilderResult"/> class.
            /// </summary>
            /// <param name="constantValue">The constant value to use for all requests</param>
            public StaticCrossOriginPolicyBuilderResult(string constantValue) : base()
            {
                _constantValue = constantValue ?? throw new ArgumentNullException(nameof(constantValue));
            }

            /// <summary>
            /// The Cross Origin  Policy header value to use
            /// </summary>
            internal override string ConstantValue => _constantValue;

            /// <summary>
            /// A builder function to generate the directives for a given request
            /// </summary>
            internal override Func<HttpContext, string> Builder =>
                throw new InvalidOperationException("Cannot return dynamic builder: CrossOriginPolicyBuilderResult has constant value");
        }
    }
}