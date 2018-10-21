namespace NetEscapades.AspNetCore.SecurityHeaders.Headers.ContentSecurityPolicy
{
    /// <summary>
    /// The style-src directive specifies valid sources for sources for stylesheets.
    /// </summary>
    public class StyleSourceDirectiveBuilder : CspDirectiveBuilder
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="StyleSourceDirectiveBuilder"/> class.
        /// </summary>
        public StyleSourceDirectiveBuilder() : base("style-src")
        {
        }

        /// <summary>
        /// Allow sources for content generated using the the HashTagHelper.
        /// </summary>
        /// <returns>The CSP builder for method chaining</returns>
        public StyleSourceDirectiveBuilder WithHashTagHelper()
        {
            // TODO: check hash algorithm is one of expected values
            SourceBuilders.Add(ctx =>
            {
                return string.Join(" ", ctx.GetStyleCSPHashes());
            });
            return this;
        }
    }
}