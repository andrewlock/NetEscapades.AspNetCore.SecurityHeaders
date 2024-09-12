namespace NetEscapades.AspNetCore.SecurityHeaders.Headers.PermissionsPolicy;

/// <summary>
/// The HTTP <c>Permissions-Policy</c> header <c>web-share</c> directive controls whether the
/// current document is allowed to use the <c>Navigator.share()</c> method of the Web Share API
/// to share text, links, images, and other content to arbitrary destinations of the
/// user's choice. Specifically, where a defined policy blocks usage of this feature,
/// <c>Navigator.share()</c> calls will return a <c>Promise</c> that rejects with a <c>DOMException</c> of type
/// <c>NotAllowedError</c>.
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