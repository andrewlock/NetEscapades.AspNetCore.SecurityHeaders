namespace NetEscapades.AspNetCore.SecurityHeaders.Headers.PermissionsPolicy
{
    /// <summary>
    /// Controls whether the current document is allowed to use audio
    /// input devices. When this policy is enabled, the <code>Promise</code>
    /// returned by <code>MediaDevices.getUserMedia()</code> will
    /// reject with a <code>NotAllowedError</code>.
    /// </summary>
    public class MicrophonePermissionsPolicyDirectiveBuilder : PermissionsPolicyDirectiveBuilder
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="MicrophonePermissionsPolicyDirectiveBuilder"/> class.
        /// </summary>
        public MicrophonePermissionsPolicyDirectiveBuilder() : base("microphone")
        {
        }
    }
}
