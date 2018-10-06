using System;

namespace NetEscapades.AspNetCore.SecurityHeaders.Infrastructure
{
    /// <summary>
    /// Create a custom Feature Policy directive for an un-implemented directive.
    /// </summary>
    public class CustomFeaturePolicyDirective : FeaturePolicyDirectiveBuilderBase
    {
        /// <summary>
        /// Create a custom Feature Policy directive for an un-implemented directive.
        /// </summary>
        /// <param name="directive">The feature policy name, e.g. push, or vibrate</param>
        /// <param name="value">The feature value, e.g. 'self', *, 'none'</param>
        public CustomFeaturePolicyDirective(string directive, string value) : base(directive)
        {
            if (string.IsNullOrEmpty(directive)) { throw new ArgumentException($"{nameof(directive)} must not be null or empty", nameof(directive)); }

            Value = value;
        }

        public string Value { get; }

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