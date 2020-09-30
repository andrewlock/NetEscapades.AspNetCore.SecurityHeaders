using System;

namespace NetEscapades.AspNetCore.SecurityHeaders.Headers.PermissionsPolicy
{
    /// <summary>
    /// Create a custom Permission Policy directive for an un-implemented directive.
    /// </summary>
    public class CustomPermissionsPolicyDirectiveBuilder : PermissionsPolicyDirectiveBuilder
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CustomPermissionsPolicyDirectiveBuilder"/> class.
        /// Create a custom Permission Policy directive for an un-implemented directive.
        /// </summary>
        /// <param name="directive">The feature policy name, e.g. push, or vibrate</param>
        public CustomPermissionsPolicyDirectiveBuilder(string directive) : base(directive)
        {
            if (string.IsNullOrEmpty(directive))
            {
                throw new ArgumentException($"{nameof(directive)} must not be null or empty", nameof(directive));
            }
        }
    }
}
