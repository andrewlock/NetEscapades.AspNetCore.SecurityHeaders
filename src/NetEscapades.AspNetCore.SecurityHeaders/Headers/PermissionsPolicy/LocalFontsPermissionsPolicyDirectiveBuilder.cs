namespace NetEscapades.AspNetCore.SecurityHeaders.Headers.PermissionsPolicy;

/// <summary>
/// The HTTP <c>Permissions-Policy</c> header <c>local-fonts</c> directive controls whether the current
/// document is allowed to gather data on the user's locally-installed fonts via the
/// <c>Window.queryLocalFonts()</c> method. Specifically, where a defined policy blocks use
/// of this feature, <c>Promises</c> returned by <c>Window.queryLocalFonts()</c> will reject with a
/// <c>DOMException</c> of type <c>SecurityError</c>.
/// </summary>
public class LocalFontsPermissionsPolicyDirectiveBuilder : PermissionsPolicyDirectiveBuilder
{
    /// <summary>
    /// Initializes a new instance of the <see cref="LocalFontsPermissionsPolicyDirectiveBuilder"/> class.
    /// </summary>
    public LocalFontsPermissionsPolicyDirectiveBuilder() : base("local-fonts")
    {
    }
}