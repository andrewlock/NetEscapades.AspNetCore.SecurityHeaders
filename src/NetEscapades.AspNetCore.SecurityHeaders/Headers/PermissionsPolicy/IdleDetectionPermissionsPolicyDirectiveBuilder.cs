namespace NetEscapades.AspNetCore.SecurityHeaders.Headers.PermissionsPolicy;

/// <summary>
/// The HTTP Permissions-Policy header idle-detection directive controls whether the current document is
/// allowed to use the Idle Detection API to detect when users are interacting with their devices, for
/// example to report "available"/"away" status in chat applications. Specifically, where a defined
/// policy blocks use of this feature, <code>IdleDetector.start()</code> calls will return a Promise that rejects
/// with a <code>DOMException</code> of type <code>NotAllowedError</code>
/// </summary>
public class IdleDetectionPermissionsPolicyDirectiveBuilder : PermissionsPolicyDirectiveBuilder
{
    /// <summary>
    /// Initializes a new instance of the <see cref="IdleDetectionPermissionsPolicyDirectiveBuilder"/> class.
    /// </summary>
    public IdleDetectionPermissionsPolicyDirectiveBuilder() : base("idle-detection")
    {
    }
}