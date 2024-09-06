namespace NetEscapades.AspNetCore.SecurityHeaders.Headers.PermissionsPolicy;

/// <summary>
/// The HTTP Permissions-Policy header speaker-selection directive controls whether the current document is allowed to enumerate and select audio output devices
/// (speakers, headphones, and so on). Specifically, where a defined policy blocks use of this feature: <code>MediaDevices.enumerateDevices()</code> won't return devices of
/// type audio output. <code>MediaDevices.selectAudioOutput()</code> won't display the popup for selecting an audio output, and the returned Promise will reject with a <code>DOMException</code>
/// of type <code>NotAllowedError</code>. <code>HTMLMediaElement.setSinkId()</code> and <code>AudioContext.setSinkId()</code> will throw a <code>NotAllowedError</code> if called for an audio output.
/// </summary>
public class SpeakerSelectionPermissionsPolicyDirectiveBuilder : PermissionsPolicyDirectiveBuilder
{
    /// <summary>
    /// Initializes a new instance of the <see cref="SpeakerSelectionPermissionsPolicyDirectiveBuilder"/> class.
    /// </summary>
    public SpeakerSelectionPermissionsPolicyDirectiveBuilder() : base("speaker-selection")
    {
    }
}