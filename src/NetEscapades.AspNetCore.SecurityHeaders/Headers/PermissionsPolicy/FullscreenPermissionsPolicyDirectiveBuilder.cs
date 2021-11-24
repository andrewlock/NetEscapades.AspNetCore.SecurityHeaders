namespace NetEscapades.AspNetCore.SecurityHeaders.Headers.PermissionsPolicy;

/// <summary>
/// Controls whether the current document is allowed to use
/// <code>Element.requestFullScreen()</code>. When this policy is enabled,
/// the returned <code>Promise</code> rejects with a <code>TypeError</code>.
/// </summary>
public class FullscreenPermissionsPolicyDirectiveBuilder : PermissionsPolicyDirectiveBuilder
{
    /// <summary>
    /// Initializes a new instance of the <see cref="FullscreenPermissionsPolicyDirectiveBuilder"/> class.
    /// </summary>
    public FullscreenPermissionsPolicyDirectiveBuilder() : base("fullscreen")
    {
    }
}