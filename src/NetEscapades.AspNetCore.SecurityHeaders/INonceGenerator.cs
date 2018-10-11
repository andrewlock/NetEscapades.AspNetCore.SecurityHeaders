namespace NetEscapades.AspNetCore.SecurityHeaders
{
    /// <summary>
    /// Used to generate nonce values
    /// </summary>
    internal interface INonceGenerator
    {
        /// <summary>
        /// Generate a nonce
        /// </summary>
        /// <param name="nonceBytes">The number of bytes used to generate a nonce</param>
        /// <returns>The nonce as a string</returns>
        string GetNonce(int nonceBytes);
    }
}