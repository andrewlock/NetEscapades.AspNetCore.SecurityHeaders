namespace NetEscapades.AspNetCore.SecurityHeaders.Infrastructure
{
    /// <summary>
    /// The media-src directive specifies valid sources for loading media using the &lt;audio&gt; and &lt;video&gt; elements
    /// </summary>
    public class MediaSourceDirectiveBuilder : CspDirectiveBuilder
    {
        public MediaSourceDirectiveBuilder() : base("media-src")
        {
        }
    }
}