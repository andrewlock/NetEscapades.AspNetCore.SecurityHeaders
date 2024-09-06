namespace NetEscapades.AspNetCore.SecurityHeaders.Headers.PermissionsPolicy;

/// <summary>
/// The HTTP Permissions-Policy header local-fonts directive controls whether the current
/// document is allowed to gather data on the user's locally-installed fonts via the
/// <code>Window.queryLocalFonts()</code> method.
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