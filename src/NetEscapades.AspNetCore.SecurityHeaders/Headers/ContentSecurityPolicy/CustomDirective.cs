using System;
using Microsoft.AspNetCore.Http;

namespace NetEscapades.AspNetCore.SecurityHeaders.Headers.ContentSecurityPolicy
{
    /// <summary>
    /// Create a custom CSP directive for an un-implemented directive
    /// </summary>
    public class CustomDirective : CspDirectiveBuilderBase
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CustomDirective"/> class.
        /// Create a custom CSP directive for an un-implemented directive
        /// </summary>
        /// <param name="directive">The directive name, e.g. default-src</param>
        /// <param name="value">The directive value</param>
        public CustomDirective(string directive, string value) : base(directive)
        {
            if (string.IsNullOrEmpty(directive))
            {
                throw new ArgumentException($"{nameof(directive)} must not be null or empty", nameof(directive));
            }

            if (string.IsNullOrEmpty(value))
            {
                throw new ArgumentException($"{nameof(value)} must not be null or empty", nameof(value));
            }

            Value = value;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="CustomDirective"/> class.
        /// Create a custom CSP directive for an un-implemented directive
        /// </summary>
        /// <param name="directive">The directive name, e.g. default-src</param>
        public CustomDirective(string directive) : base(directive)
        {
            if (string.IsNullOrEmpty(directive))
            {
                throw new ArgumentException($"{nameof(directive)} must not be null or empty", nameof(directive));
            }

            Value = string.Empty;
        }

        /// <summary>
        /// The directive value
        /// </summary>
        public string Value { get; }

        /// <inheritdoc />
        internal override Func<HttpContext, string> CreateBuilder()
        {
            if (string.IsNullOrEmpty(Value))
            {
                return ctx => Directive;
            }

            return ctx => $"{Directive} {Value}";
        }
    }
}