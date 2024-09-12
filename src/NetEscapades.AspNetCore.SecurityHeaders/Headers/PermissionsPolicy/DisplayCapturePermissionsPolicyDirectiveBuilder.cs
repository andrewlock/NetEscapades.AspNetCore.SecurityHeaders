namespace NetEscapades.AspNetCore.SecurityHeaders.Headers.PermissionsPolicy;

/// <summary>
/// The HTTP <c>Permissions-Policy</c> header <c>display-capture</c> directive controls whether or not the document is permitted to use <c>Screen Capture API</c>, that is, <c>getDisplayMedia()</c> to capture the screen's contents.
/// If <c>display-capture</c> is disabled in a document, the document will not be able to initiate screen capture via <c>getDisplayMedia()</c> and will throw a <c>NotAllowedError</c> exception.
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