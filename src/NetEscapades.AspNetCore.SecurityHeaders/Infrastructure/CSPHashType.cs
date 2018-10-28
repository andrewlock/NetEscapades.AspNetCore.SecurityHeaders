namespace NetEscapades.AspNetCore.SecurityHeaders.Infrastructure
{
    /// <summary>
    /// The algorithms available for CSP hashing
    /// </summary>
    public enum CSPHashType
    {
        /// <summary>
        /// Use the SHA256 algorithm for CSP hashing
        /// </summary>
        SHA256 = 0,

        /// <summary>
        /// Use the SHA384 for CSP hashing
        /// </summary>
        SHA384 = 1,

        /// <summary>
        /// Use the SHA512 for CSP hashing
        /// </summary>
        SHA512 = 2,
    }
}