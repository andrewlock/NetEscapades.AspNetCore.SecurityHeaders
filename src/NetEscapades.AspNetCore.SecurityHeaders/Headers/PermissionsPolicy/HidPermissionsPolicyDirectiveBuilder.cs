namespace NetEscapades.AspNetCore.SecurityHeaders.Headers.PermissionsPolicy;

/// <summary>
/// The HTTP Permissions-Policy header hid directive controls whether the current document
/// is allowed to use the <code>WebHID API</code> to connect to uncommon or exotic human interface devices
/// such as alternative keyboards or gamepads. Specifically, where a defined policy blocks WebHID usage,
/// the <code>Navigator.hid</code> property will not be available.
/// </summary>
public class HidPermissionsPolicyDirectiveBuilder : PermissionsPolicyDirectiveBuilder
{
    /// <summary>
    /// Initializes a new instance of the <see cref="HidPermissionsPolicyDirectiveBuilder"/> class.
    /// </summary>
    public HidPermissionsPolicyDirectiveBuilder() : base("hid")
    {
    }
}