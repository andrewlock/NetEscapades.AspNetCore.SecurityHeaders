namespace NetEscapades.AspNetCore.SecurityHeaders.Infrastructure
{
    /// <summary>
    /// The font-src directive specifies valid sources for fonts loaded using @font-face.
    /// </summary>
    public class FontSourceDirectiveBuilder : CspDirectiveBuilder
    {
        public FontSourceDirectiveBuilder() : base("font-src")
        {
        }
    }
}