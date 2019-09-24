using NetEscapades.AspNetCore.SecurityHeaders.Headers;

// ReSharper disable once CheckNamespace
namespace Microsoft.AspNetCore.Builder
{
    /// <summary>
    /// Extension methods for adding a <see cref="ServerHeader" /> to a <see cref="HeaderPolicyCollection" />
    /// </summary>
    public static class ServerHeaderExtensions
    {
        /// <summary>
        /// Removes the Server header from all responses
        /// </summary>
        /// <param name="policies">The collection of policies</param>
        /// <returns>The <see cref="HeaderPolicyCollection"/> for method chaining</returns>
        public static HeaderPolicyCollection RemoveServerHeader(this HeaderPolicyCollection policies)
        {
            return policies.ApplyPolicy(new ServerHeader(string.Empty));
        }
    }
}