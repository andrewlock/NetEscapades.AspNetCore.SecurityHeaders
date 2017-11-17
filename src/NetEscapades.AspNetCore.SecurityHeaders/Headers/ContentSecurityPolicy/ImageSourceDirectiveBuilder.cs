namespace NetEscapades.AspNetCore.SecurityHeaders.Infrastructure
{
    /// <summary>
    /// The img-src directive specifies valid sources of images and favicons
    /// </summary>
    public class ImageSourceDirectiveBuilder : CspDirectiveBuilder
    {
        public ImageSourceDirectiveBuilder() : base("img-src")
        {
        }
    }
}