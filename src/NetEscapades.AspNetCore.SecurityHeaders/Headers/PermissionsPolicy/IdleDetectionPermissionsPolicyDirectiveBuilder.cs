namespace NetEscapades.AspNetCore.SecurityHeaders.Headers.PermissionsPolicy;

/// <summary>
/// The HTTP <c>Permissions-Policy</c> header <c>idle-detection</c> directive controls whether the current document is
/// allowed to use the <c>Idle Detection API</c> to detect when users are interacting with their devices, for example
/// to report "available"/"away" status in chat applications. Specifically, where a defined policy blocks use
/// of this feature, <c>IdleDetector.start()</c> calls will return a <c>Promise</c> that rejects with a <c>DOMException</c> of
/// type <c>NotAllowedError</c>.
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