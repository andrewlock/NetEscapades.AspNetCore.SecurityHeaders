namespace NetEscapades.AspNetCore.SecurityHeaders.Infrastructure
{
    /// <summary>
    /// The font-src directive specifies valid sources for fonts loaded using @font-face.
    /// </summary>
    public class FontSourceDirectiveBuilder : CspDirectiveBuilder
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="FontSourceDirectiveBuilder"/> class.
        /// </summary>
        public FontSourceDirectiveBuilder() : base("font-src")
        {
        }
    }
}