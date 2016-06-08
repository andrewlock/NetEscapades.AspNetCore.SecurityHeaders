namespace NetEscapades.AspNetCore.SecurityHeaders.Infrastructure
{
    public static class XFrameOptionsHeaderExtensions
    {
        /// <summary>
        /// Add X-Frame-Options DENY to all requests.
        /// The page cannot be displayed in a frame, regardless of the site attempting to do so
        /// </summary>
        /// <param name="policies">The collection of policies</param>
        public static HeaderPolicyCollection AddFrameOptionsDeny(this HeaderPolicyCollection policies)
        {
            return policies.ApplyPolicy(XFrameOptionsHeader.Deny());
        }

        /// <summary>
        /// Add X-Frame-Options SAMEORIGIN to all requests.
        /// The page can only be displayed in a frame on the same origin as the page itself.
        /// </summary>
        /// <param name="policies">The collection of policies</param>
        public static HeaderPolicyCollection AddFrameOptionsSameOrigin(this HeaderPolicyCollection policies)
        {
            return policies.ApplyPolicy(XFrameOptionsHeader.SameOrigin());
        }

        /// <summary>
        /// Add X-Frame-Options ALLOW-FROM {uri} to all requests, where the uri is provided
        /// The page can only be displayed in a frame on the specified origin.
        /// </summary>
        /// <param name="policies">The collection of policies</param>
        /// <param name="uri">The uri of the origin in which the page may be displayed in a frame</param>
        public static HeaderPolicyCollection AddFrameOptionsSameOrigin(this HeaderPolicyCollection policies, string uri)
        {
            return policies.ApplyPolicy(XFrameOptionsHeader.AllowFromUri(uri));
        }
    }
}