namespace NetEscapades.AspNetCore.SecurityHeaders.Infrastructure
{
    /// <summary>
    /// Controls whether the current document is allowed to use Picture In Picture.
    /// If disabled in a document, then calls to <code>requestPictureInPicture()</code>
    /// will throw a <code>SecurityError</code> and <code>pictureInPictureEnabled</code>
    /// will return <code>false</code>.
    /// </summary>
    public class PictureInPictureFeaturePolicyDirectiveBuilder : FeaturePolicyDirectiveBuilder
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="PictureInPictureFeaturePolicyDirectiveBuilder"/> class.
        /// </summary>
        public PictureInPictureFeaturePolicyDirectiveBuilder() : base("picture-in-picture")
        {
        }
    }
}