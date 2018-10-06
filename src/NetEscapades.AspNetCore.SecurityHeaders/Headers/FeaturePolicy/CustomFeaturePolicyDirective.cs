using System;

namespace NetEscapades.AspNetCore.SecurityHeaders.Infrastructure
{
    /// <summary>
    /// Create a custom Feature Policy directive for an un-implemented directive.
    /// </summary>
    public class CustomFeaturePolicyDirective : FeaturePolicyDirectiveBuilderBase
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CustomFeaturePolicyDirective"/> class.
        /// Create a custom Feature Policy directive for an un-implemented directive.
        /// </summary>
        /// <param name="directive">The feature policy name, e.g. push, or vibrate</param>
        /// <param name="value">The feature value, e.g. 'self', *, 'none'</param>
        public CustomFeaturePolicyDirective(string directive, string value) : base(directive)
        {
            if (string.IsNullOrEmpty(directive))
            {
                throw new ArgumentException($"{nameof(directive)} must not be null or empty", nameof(directive));
            }

            Value = value;
        }

        /// <summary>
        /// The directive value
        /// </summary>
        public string Value { get; }

        /// <inheritdoc />
        internal override string Build()
        {
            if (string.IsNullOrEmpty(Value))
            {
                return Directive;
            }

            return $"{Directive} {Value}";
        }
    }
}