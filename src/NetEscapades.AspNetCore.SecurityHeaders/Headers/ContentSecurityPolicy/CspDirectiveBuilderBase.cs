namespace NetEscapades.AspNetCore.SecurityHeaders.Infrastructure
{
    /// <summary>
    /// Base class for building CSP directives.
    /// </summary>
    public abstract class CspDirectiveBuilderBase
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CspDirectiveBuilderBase"/> class.
        /// </summary>
        /// <param name="directive">The name of the directive.</param>
        public CspDirectiveBuilderBase(string directive)
        {
            Directive = directive;
        }

        /// <summary>
        /// The name of the directive.
        /// </summary>
        internal string Directive { get; }

        /// <summary>
        /// If true, the header directives are unique per request, and require
        /// runtime formatting (e.g. for use with Nonce)
        /// </summary>
        internal bool IsUniquePerRequest { get; set; } = false;

        /// <summary>
        /// Builds the complete directive policy string.
        /// </summary>
        /// <returns>The complete directive string.</returns>
        internal abstract string Build();
    }
}