using System.Collections.Generic;

namespace NetEscapades.AspNetCore.SecurityHeaders.Infrastructure
{
    /// <summary>
    /// Results returned by <see cref="ICustomHeaderService"/> 
    /// </summary>
    public class CustomHeadersResult
    {
        /// <summary>
        /// A dictionary of Header, Value pairs that should be added to the request
        /// </summary>
        public IDictionary<string, string> SetHeaders { get; } = new Dictionary<string, string>();

        /// <summary>
        /// A hashset of Headers that should be removed from the request
        /// </summary>
        public ISet<string> RemoveHeaders { get; } = new HashSet<string>();
    }
}