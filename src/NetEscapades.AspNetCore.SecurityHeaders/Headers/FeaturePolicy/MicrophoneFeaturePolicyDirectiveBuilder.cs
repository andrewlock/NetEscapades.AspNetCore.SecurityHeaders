namespace NetEscapades.AspNetCore.SecurityHeaders.Headers.FeaturePolicy
{
    /// <summary>
    /// Controls whether the current document is allowed to use audio
    /// input devices. When this policy is enabled, the <code>Promise</code>
    /// returned by <code>MediaDevices.getUserMedia()</code> will
    /// reject with a <code>NotAllowedError</code>.
    /// </summary>
    public class MicrophoneFeaturePolicyDirectiveBuilder : FeaturePolicyDirectiveBuilder
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="MicrophoneFeaturePolicyDirectiveBuilder"/> class.
        /// </summary>
        public MicrophoneFeaturePolicyDirectiveBuilder() : base("microphone")
        {
        }
    }
}