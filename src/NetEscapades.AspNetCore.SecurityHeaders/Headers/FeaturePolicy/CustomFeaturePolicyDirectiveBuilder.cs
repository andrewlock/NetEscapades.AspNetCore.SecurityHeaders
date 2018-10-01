using System;

namespace NetEscapades.AspNetCore.SecurityHeaders.Infrastructure
{
    /// <summary>
    /// Create a custom Feature Policy directive for an un-implemented directive.
    /// </summary>
    public class CustomFeaturePolicyDirectiveBuilder : FeaturePolicyDirectiveBuilder
    {
        /// <summary>
        /// Create a custom Feature Policy directive for an un-implemented directive.
        /// </summary>
        /// <param name="directive">The feature policy name, e.g. push, or vibrate</param>
        public CustomFeaturePolicyDirectiveBuilder(string directive) : base(directive)
        {
            if (string.IsNullOrEmpty(directive)) { throw new ArgumentException($"{nameof(directive)} must not be null or empty", nameof(directive)); }
        }
    }
}