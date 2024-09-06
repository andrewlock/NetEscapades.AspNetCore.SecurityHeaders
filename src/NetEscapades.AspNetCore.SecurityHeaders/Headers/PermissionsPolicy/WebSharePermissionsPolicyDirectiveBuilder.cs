namespace NetEscapades.AspNetCore.SecurityHeaders.Headers.PermissionsPolicy;

/// <summary>
/// The HTTP Permissions-Policy header web-share directive controls whether the
/// current document is allowed to use the <code>Navigator.share()</code> method of the Web Share API
/// to share text, links, images, and other content to arbitrary destinations of the user's
/// choice. Specifically, where a defined policy blocks usage of this feature, <code>Navigator.share()</code>
/// calls will return a Promise that rejects with a <code>DOMException</code> of type <code>NotAllowedError</code>
/// </summary>
public class WebSharePermissionsPolicyDirectiveBuilder : PermissionsPolicyDirectiveBuilder
{
    /// <summary>
    /// Initializes a new instance of the <see cref="WebSharePermissionsPolicyDirectiveBuilder"/> class.
    /// </summary>
    public WebSharePermissionsPolicyDirectiveBuilder() : base("web-share")
    {
    }
}