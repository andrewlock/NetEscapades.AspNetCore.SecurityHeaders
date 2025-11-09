// Keep it block scoped to make it easier to use in tests
namespace NetEscapades.AspNetCore.SecurityHeaders.Helpers
{
    /// <summary>
    /// Indicates that a given method is insecure. This may be due to library changes or
    /// changes in the web platform. The type or method may be removed at a later point
    /// </summary>
    [AttributeUsage(AttributeTargets.Method)]
    internal class InsecureApiAttribute : Attribute
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="InsecureApiAttribute"/> class.
        /// </summary>
        /// <param name="message">A description of why this API is insecure</param>
        public InsecureApiAttribute(string message)
        {
            Message = message;
        }

        /// <summary>
        /// Gets a description of why this API is insecure
        /// </summary>
        public string Message { get; }

        /// <summary>
        /// Gets a Uri where you can read more why the API is insecure
        /// </summary>
        public string? Url { get; } = null;
    }
}