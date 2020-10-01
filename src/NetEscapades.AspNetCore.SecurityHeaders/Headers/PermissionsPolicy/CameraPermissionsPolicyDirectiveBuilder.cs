namespace NetEscapades.AspNetCore.SecurityHeaders.Headers.PermissionsPolicy
{
    /// <summary>
    /// Controls access to video input devices requested through the
    /// NavigatorUserMedia interface. If disabled in a document, then calls
    /// to <code>getUserMedia()</code> will not grant access to video input
    /// devices in that document.
    /// </summary>
    public class CameraPermissionsPolicyDirectiveBuilder : PermissionsPolicyDirectiveBuilder
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CameraPermissionsPolicyDirectiveBuilder"/> class.
        /// </summary>
        public CameraPermissionsPolicyDirectiveBuilder() : base("camera")
        {
        }
    }
}
