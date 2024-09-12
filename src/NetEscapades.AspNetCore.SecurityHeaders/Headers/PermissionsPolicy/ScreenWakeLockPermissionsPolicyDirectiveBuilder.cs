namespace NetEscapades.AspNetCore.SecurityHeaders.Headers.PermissionsPolicy;

/// <summary>
/// The HTTP <c>Permissions-Policy</c> header <c>screen-wake-lock</c> directive controls whether the current document
/// is allowed to use <c>Screen Wake Lock API</c> to indicate that the device should not dim or turn off the screen.
/// Specifically, where a defined policy blocks use of this feature, <c>WakeLock.request()</c> calls will return a
/// <c>Promise</c> that rejects with a <c>DOMException</c> of type <c>NotAllowedError</c>.
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