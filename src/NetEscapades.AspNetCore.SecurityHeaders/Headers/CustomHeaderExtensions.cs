namespace NetEscapades.AspNetCore.SecurityHeaders.Infrastructure
{
    /// <summary>
    /// Extension methods for adding a <see cref="CustomHeader" /> to a <see cref="HeaderPolicyCollection" />
    /// </summary>
    public static class CustomHeaderExtensions
    {
        /// <summary>
        /// Add a custom header to all requests
        /// </summary>
        /// <param name="policies">The collection of policies</param>
        /// <param name="header">The header value to use</param>
        /// <param name="value">The value to set for the given header</param>
        public static HeaderPolicyCollection AddCustomHeader(this HeaderPolicyCollection policies, string header, string value)
        {
            return policies.ApplyPolicy(new CustomHeader(header, value));
        }
    }
}