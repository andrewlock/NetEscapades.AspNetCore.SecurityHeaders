namespace NetEscapades.AspNetCore.SecurityHeaders.Headers.PermissionsPolicy;

/// <summary>
/// The HTTP Permissions-Policy header window-management directive controls whether or not the current
/// document is allowed to use the <code>Window Management API</code> to manage windows on multiple displays. Where
/// this policy forbids use of the API: The Promise returned by the <code>Window.getScreenDetails()</code> method
/// will reject with a <code>NotAllowedError</code> exception. The <code>Window.screen.isExtended</code> property will always return false.
/// </summary>
public class WindowManagementPermissionsPolicyDirectiveBuilder : PermissionsPolicyDirectiveBuilder
{
    /// <summary>
    /// Initializes a new instance of the <see cref="WindowManagementPermissionsPolicyDirectiveBuilder"/> class.
    /// </summary>
    public WindowManagementPermissionsPolicyDirectiveBuilder() : base("window-management")
    {
    }
}