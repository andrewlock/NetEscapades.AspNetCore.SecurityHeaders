namespace NetEscapades.AspNetCore.SecurityHeaders.Headers.PermissionsPolicy;

/// <summary>
/// The HTTP <c>Permissions-Policy</c> header <c>hid</c> directive controls whether the current document is allowed to use the <c>WebHID API</c> to connect to uncommon or exotic human interface devices such as alternative keyboards or gamepads.
/// Specifically, where a defined policy blocks WebHID usage, the <c>Navigator.hid</c> property will not be available.
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