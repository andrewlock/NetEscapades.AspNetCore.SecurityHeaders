namespace NetEscapades.AspNetCore.SecurityHeaders.Infrastructure
{
    /// <summary>
    /// Controls access to video input devices requested through the
    /// NavigatorUserMedia interface. If disabled in a document, then calls
    /// to <code>getUserMedia()</code> will not grant access to video input
    /// devices in that document.
    /// </summary>
    public class CameraFeaturePolicyDirectiveBuilder : FeaturePolicyDirectiveBuilder
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CameraFeaturePolicyDirectiveBuilder"/> class.
        /// </summary>
        public CameraFeaturePolicyDirectiveBuilder() : base("camera")
        {
        }
    }
}