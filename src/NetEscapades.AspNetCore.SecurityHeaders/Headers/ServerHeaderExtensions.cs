namespace NetEscapades.AspNetCore.SecurityHeaders.Infrastructure
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
        public static HeaderPolicyCollection RemoveServerHeader(this HeaderPolicyCollection policies)
        {
            return policies.ApplyPolicy(ServerHeader.Remove());
        }
    }

}