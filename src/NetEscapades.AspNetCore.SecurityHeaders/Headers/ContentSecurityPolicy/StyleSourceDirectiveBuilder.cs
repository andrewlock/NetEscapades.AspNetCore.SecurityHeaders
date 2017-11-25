namespace NetEscapades.AspNetCore.SecurityHeaders.Infrastructure
{
    /// <summary>
    /// The style-src directive specifies valid sources for sources for stylesheets.
    /// </summary>
    public class StyleSourceDirectiveBuilder : CspDirectiveBuilder
    {
        public StyleSourceDirectiveBuilder() : base("style-src")
        {
        }
    }
}