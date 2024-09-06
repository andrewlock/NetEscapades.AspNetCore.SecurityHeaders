namespace NetEscapades.AspNetCore.SecurityHeaders.Headers.PermissionsPolicy;

/// <summary>
/// The HTTP Permissions-Policy header screen-wake-lock directive controls whether the current document is allowed to use
/// <code>Screen Wake Lock API</code> to indicate that the device should not dim or turn off the screen. Specifically, where a defined
/// policy blocks use of this feature, <code>WakeLock.request()</code> calls will return a Promise that rejects with a <code>DOMException</code> of type <code>NotAllowedError</code>
/// </summary>
public class ScreenWakeLockPermissionsPolicyDirectiveBuilder : PermissionsPolicyDirectiveBuilder
{
    /// <summary>
    /// Initializes a new instance of the <see cref="ScreenWakeLockPermissionsPolicyDirectiveBuilder"/> class.
    /// </summary>
    public ScreenWakeLockPermissionsPolicyDirectiveBuilder() : base("screen-wake-lock")
    {
    }
}