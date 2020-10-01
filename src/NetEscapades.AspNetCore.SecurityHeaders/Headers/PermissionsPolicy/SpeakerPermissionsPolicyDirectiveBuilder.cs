namespace NetEscapades.AspNetCore.SecurityHeaders.Headers.PermissionsPolicy
{
    /// <summary>
    /// Controls access to audio output devices requested through
    /// the NavigatorUserMedia interface. If disabled then calls to
    /// <code>getUserMedia()</code> will not grant access to audio
    /// output devices in that document.
    /// </summary>
    public class SpeakerPermissionsPolicyDirectiveBuilder : PermissionsPolicyDirectiveBuilder
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SpeakerPermissionsPolicyDirectiveBuilder"/> class.
        /// </summary>
        public SpeakerPermissionsPolicyDirectiveBuilder() : base("speaker")
        {
        }
    }
}
