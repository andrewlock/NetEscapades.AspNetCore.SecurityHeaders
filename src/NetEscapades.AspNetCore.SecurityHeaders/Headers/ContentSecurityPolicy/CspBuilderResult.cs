namespace NetEscapades.AspNetCore.SecurityHeaders.Infrastructure
{
    /// <summary>
    /// The output from calling <see cref="CspBuilder.Build"/>
    /// </summary>
    internal struct CspBuilderResult
    {
        /// <summary>
        /// If true, the header directives are unique per request, and require
        /// runtime formatting (e.g. for use with Nonce)
        /// </summary>
        internal bool IsUniquePerRequest { get; set; }

        /// <summary>
        /// The CSP header value to use
        /// </summary>
        internal string Value { get; set; }
    }
}