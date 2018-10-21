namespace NetEscapades.AspNetCore.SecurityHeaders.Infrastructure
{
    /// <summary>
    /// Various constant values used internally
    /// </summary>
    internal static class Constants
    {
        /// <summary>
        /// A key for the per-request nonce
        /// </summary>
        internal const string DefaultNonceKey = "NETESCAPADES_NONCE";

        /// <summary>
        /// The default number of bytes to use for generating a nonce
        /// </summary>
        internal const int DefaultBytesInNonce = 32;

        /// <summary>
        /// A key for per-request hashes for script-src
        /// </summary>
        internal const string DefaultScriptHashKey = "NETESCAPADES_HASHES_SCRIPT";

        /// <summary>
        /// A key for per-request hashes for styles-src
        /// </summary>
        internal const string DefaultStylesHashKey = "NETESCAPADES_HASHES_STYLES";
    }
}