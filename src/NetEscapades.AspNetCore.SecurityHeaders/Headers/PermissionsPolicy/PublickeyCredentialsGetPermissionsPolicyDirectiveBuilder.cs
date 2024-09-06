namespace NetEscapades.AspNetCore.SecurityHeaders.Headers.PermissionsPolicy;

/// <summary>
/// The HTTP Permissions-Policy header publickey-credentials-get directive controls whether the current document is
/// allowed to access the Web Authentication API to retrieve public-key credentials, i.e., via <code>navigator.credentials.get({publicKey})</code>
/// Specifically, where a defined policy blocks the use of this feature, the Promise returned by <code>navigator.credentials.get({publicKey})</code>
/// will reject with a <code>NotAllowedError DOMException</code>
/// </summary>
public class PublickeyCredentialsGetPermissionsPolicyDirectiveBuilder : PermissionsPolicyDirectiveBuilder
{
    /// <summary>
    /// Initializes a new instance of the <see cref="PublickeyCredentialsGetPermissionsPolicyDirectiveBuilder"/> class.
    /// </summary>
    public PublickeyCredentialsGetPermissionsPolicyDirectiveBuilder() : base("publickey-credentials-get")
    {
    }
}