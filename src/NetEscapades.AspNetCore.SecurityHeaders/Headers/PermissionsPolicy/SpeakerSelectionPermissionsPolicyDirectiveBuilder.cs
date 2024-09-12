namespace NetEscapades.AspNetCore.SecurityHeaders.Headers.PermissionsPolicy;

/// <summary>
/// The HTTP <c>Permissions-Policy</c> header <c>speaker-selection</c> directive controls whether the current
/// document is allowed to enumerate and select audio output devices (speakers, headphones, and so on).
/// Specifically, where a defined policy blocks use of this feature:
/// <list type="bullet">
/// <item><description><c>MediaDevices.enumerateDevices()</c> won't return devices of type audio output.</description></item>
/// <item><description><c>MediaDevices.selectAudioOutput()</c> won't display the popup for
/// selecting an audio output, and the returned <c>Promise</c> will reject with a <c>DOMException</c>
/// of type <c>NotAllowedError</c>.</description></item>
/// <item><description><c>HTMLMediaElement.setSinkId()</c> and <c>AudioContext.setSinkId()</c> will throw a <c>NotAllowedError</c> if called for an audio output.</description></item>
/// </list>
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