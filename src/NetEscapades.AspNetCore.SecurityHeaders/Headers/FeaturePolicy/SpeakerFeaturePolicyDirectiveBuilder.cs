namespace NetEscapades.AspNetCore.SecurityHeaders.Infrastructure
{
    /// <summary>
    /// Controls access to audio output devices requested through
    /// the NavigatorUserMedia interface. If disabled then calls to
    /// <code>getUserMedia()</code> will not grant access to audio
    /// output devices in that document.
    /// </summary>
    public class SpeakerFeaturePolicyDirectiveBuilder : FeaturePolicyDirectiveBuilder
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SpeakerFeaturePolicyDirectiveBuilder"/> class.
        /// </summary>
        public SpeakerFeaturePolicyDirectiveBuilder() : base("speaker")
        {
        }
    }
}