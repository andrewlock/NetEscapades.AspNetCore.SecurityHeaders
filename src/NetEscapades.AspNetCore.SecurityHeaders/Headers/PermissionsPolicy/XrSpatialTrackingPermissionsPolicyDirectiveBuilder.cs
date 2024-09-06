namespace NetEscapades.AspNetCore.SecurityHeaders.Headers.PermissionsPolicy;

/// <summary>
/// The HTTP Permissions-Policy header xr-spatial-tracking directive controls whether the current document
/// is allowed to use the <code>WebXR Device API</code> Specifically, where a defined policy blocks usage of this feature:
/// <code>navigator.xr.isSessionSupported()</code> and <code>navigator.xr.requestSession()</code> calls will return a Promise that rejects
/// with a <code>DOMException</code> of type <code>SecurityError</code> devicechange events are not fired on the navigator.xr object.
/// </summary>
public class XrSpatialTrackingPermissionsPolicyDirectiveBuilder : PermissionsPolicyDirectiveBuilder
{
    /// <summary>
    /// Initializes a new instance of the <see cref="XrSpatialTrackingPermissionsPolicyDirectiveBuilder"/> class.
    /// </summary>
    public XrSpatialTrackingPermissionsPolicyDirectiveBuilder() : base("xr-spatial-tracking")
    {
    }
}