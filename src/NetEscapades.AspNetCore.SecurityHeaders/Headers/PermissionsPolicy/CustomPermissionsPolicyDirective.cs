using System;

namespace NetEscapades.AspNetCore.SecurityHeaders.Headers.PermissionsPolicy
{
    /// <summary>
    /// Create a custom Permission Policy directive for an un-implemented directive.
    /// </summary>
    public class CustomPermissionsPolicyDirective : PermissionsPolicyDirectiveBuilderBase
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CustomPermissionsPolicyDirective"/> class.
        /// Create a custom Permission Policy directive for an un-implemented directive.
        /// </summary>
        /// <param name="directive">The feature policy name, e.g. push, or vibrate</param>
        /// <param name="value">The feature value, e.g. 'self', *, 'none'</param>
        public CustomPermissionsPolicyDirective(string directive, string value) : base(directive)
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
                return $"{Directive}=()";
            }

            return $"{Directive}={Value}";
        }
    }
}
