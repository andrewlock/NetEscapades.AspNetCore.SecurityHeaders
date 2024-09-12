namespace NetEscapades.AspNetCore.SecurityHeaders.Headers.PermissionsPolicy;

/// <summary>
/// The HTTP <c>Permissions-Policy</c> header <c>window-management</c> directive controls whether or not the
/// current document is allowed to use the <c>Window Management API</c> to manage windows on multiple displays.
/// Where this policy forbids use of the API:
/// <list type="bullet">
/// <item><description>The <c>Promise</c> returned by the <c>Window.getScreenDetails()</c> method will reject
/// with a <c>NotAllowedError</c> exception.</description></item>
/// <item><description>The <c>Window.screen.isExtended</c> property will always return
/// <c>false</c>.</description></item>
/// </list>
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