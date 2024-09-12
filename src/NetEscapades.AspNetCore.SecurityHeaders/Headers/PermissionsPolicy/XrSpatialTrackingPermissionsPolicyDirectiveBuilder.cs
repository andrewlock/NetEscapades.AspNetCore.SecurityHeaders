namespace NetEscapades.AspNetCore.SecurityHeaders.Headers.PermissionsPolicy;

/// <summary>
/// The HTTP <c>Permissions-Policy</c> header <c>xr-spatial-tracking</c> directive controls whether
/// the current document is allowed to use the <c>WebXR Device API</c>.
/// Specifically, where a defined policy blocks usage of this feature:
/// <list type="bullet">
/// <item><description><c>navigator.xr.isSessionSupported()</c> and <c>navigator.xr.requestSession()</c>
/// calls will return a <c>Promise</c> that rejects with a <c>DOMException</c> of type <c>SecurityError</c>.</description></item>
/// <item><description><c>devicechange</c> events are not fired on the <c>navigator.xr</c> object.</description></item>
/// </list>
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