namespace NetEscapades.AspNetCore.SecurityHeaders.Infrastructure.Constants
{
    /// <summary>
    /// X-Frame-Options-related constants.
    /// </summary>
    public static class FrameOptionsConstants
    {
        /// <summary>
        /// The header value for X-Frame-Options
        /// </summary>
        public static readonly string Header = "X-Frame-Options";

        /// <summary>
        /// The page cannot be displayed in a frame, regardless of the site attempting to do so.
        /// </summary>
        public static readonly string Deny = "DENY";

        /// <summary>
        /// The page can only be displayed in a frame on the same origin as the page itself.
        /// </summary>
        public static readonly string SameOrigin = "SAMEORIGIN";

        /// <summary>
        /// The page can only be displayed in a frame on the specified origin. {0} specifies the format string
        /// </summary>
        public static readonly string AllowFromUri = "ALLOW-FROM {0}";
    }
}