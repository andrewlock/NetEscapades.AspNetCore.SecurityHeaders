using System;

// Keep it block scoped to make it easier to use in tests
namespace NetEscapades.AspNetCore.SecurityHeaders.Helpers
{
    /// <summary>
    /// Indicates that a given type or method is now deprecated. This may be due to library changes or
    /// changes in the web platform. The type or method may be removed at a later point
    /// </summary>
    [AttributeUsage(AttributeTargets.Method)]
    internal class DeprecatedAttribute : Attribute
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DeprecatedAttribute"/> class.
        /// </summary>
        /// <param name="message">A description of why this API is deprecated</param>
        public DeprecatedAttribute(string message)
        {
            Message = message;
        }

        /// <summary>
        /// Gets a description of why this API is deprecated
        /// </summary>
        public string Message { get; }

        /// <summary>
        /// Gets a Uri where you can read more why the API is deprecated
        /// </summary>
        public string? Url { get; } = null;
    }
}