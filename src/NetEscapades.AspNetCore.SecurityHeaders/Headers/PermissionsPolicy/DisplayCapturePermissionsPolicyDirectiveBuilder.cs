namespace NetEscapades.AspNetCore.SecurityHeaders.Headers.PermissionsPolicy;

/// <summary>
/// Controls whether or not the current document is permitted to
/// use the <code>getDisplayMedia()</code> method to capture screen contents.
/// When this policy is disabled, the promise returned by <code>getDisplayMedia()</code>
/// will reject with a <code>NotAllowedError DOMException</code>  if permission is not
/// obtained to capture the display's contents.
/// </summary>
public class DisplayCapturePermissionsPolicyDirectiveBuilder : PermissionsPolicyDirectiveBuilder
{
    /// <summary>
    /// Initializes a new instance of the <see cref="DisplayCapturePermissionsPolicyDirectiveBuilder"/> class.
    /// </summary>
    public DisplayCapturePermissionsPolicyDirectiveBuilder() : base("display-capture")
    {
    }
}