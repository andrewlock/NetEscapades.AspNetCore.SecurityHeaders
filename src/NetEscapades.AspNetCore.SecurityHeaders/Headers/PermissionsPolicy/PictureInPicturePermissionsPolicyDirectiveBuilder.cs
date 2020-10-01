namespace NetEscapades.AspNetCore.SecurityHeaders.Headers.PermissionsPolicy
{
    /// <summary>
    /// Controls whether the current document is allowed to use Picture In Picture.
    /// If disabled in a document, then calls to <code>requestPictureInPicture()</code>
    /// will throw a <code>SecurityError</code> and <code>pictureInPictureEnabled</code>
    /// will return <code>false</code>.
    /// </summary>
    public class PictureInPicturePermissionsPolicyDirectiveBuilder : PermissionsPolicyDirectiveBuilder
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="PictureInPicturePermissionsPolicyDirectiveBuilder"/> class.
        /// </summary>
        public PictureInPicturePermissionsPolicyDirectiveBuilder() : base("picture-in-picture")
        {
        }
    }
}
