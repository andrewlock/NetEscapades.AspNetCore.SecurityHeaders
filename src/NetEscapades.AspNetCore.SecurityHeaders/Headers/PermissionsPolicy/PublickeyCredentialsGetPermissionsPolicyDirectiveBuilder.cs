namespace NetEscapades.AspNetCore.SecurityHeaders.Headers.PermissionsPolicy;

/// <summary>
/// The HTTP <c>Permissions-Policy</c> header <c>publickey-credentials-get</c> directive controls whether the
/// current document is allowed to access the <c>Web Authentication API</c> to retrieve public-key
/// credentials, i.e., via <c>navigator.credentials.get({publicKey})</c>. Specifically, where a defined
/// policy blocks the use of this feature, the <c>Promise</c> returned by <c>navigator.credentials.get({publicKey})</c>
/// will reject with a <c>NotAllowedError DOMException</c>.
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