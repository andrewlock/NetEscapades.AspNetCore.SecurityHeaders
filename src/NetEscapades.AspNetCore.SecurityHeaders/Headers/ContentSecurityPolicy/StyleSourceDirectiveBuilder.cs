namespace NetEscapades.AspNetCore.SecurityHeaders.Infrastructure
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
    }
}