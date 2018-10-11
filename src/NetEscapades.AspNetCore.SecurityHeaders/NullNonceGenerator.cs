namespace NetEscapades.AspNetCore.SecurityHeaders
{
    /// <summary>
    /// Always generates an empty string. SHOULD NOT BE USED IN PRODUCTION
    /// </summary>
    internal class NullNonceGenerator : INonceGenerator
    {
        /// <inheritdoc />
        public string GetNonce(int nonceBytes) => string.Empty;
    }
}