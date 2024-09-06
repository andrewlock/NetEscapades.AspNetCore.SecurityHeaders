namespace NetEscapades.AspNetCore.SecurityHeaders.Headers.PermissionsPolicy;

/// <summary>
/// The HTTP Permissions-Policy header publickey-credentials-create directive controls
/// whether the current document is allowed to use the <code>Web Authentication API</code> to create
/// new WebAuthn credentials, i.e., via <code>navigator.credentials.create({publicKey})</code>
/// Specifically, where a defined policy blocks use of this feature, the Promise returned
/// by <code>navigator.credentials.create({publicKey})</code> will reject with a <code>NotAllowedError DOMException</code>
/// If the method is called cross-origin. the Promise will also reject with a <code>NotAllowedError</code>
/// if the feature is granted by allow= on an iframe and the frame does not also have Transient activation.
/// </summary>
public class PublickeyCredentialsCreatePermissionsPolicyDirectiveBuilder : PermissionsPolicyDirectiveBuilder
{
    /// <summary>
    /// Initializes a new instance of the <see cref="PublickeyCredentialsCreatePermissionsPolicyDirectiveBuilder"/> class.
    /// </summary>
    public PublickeyCredentialsCreatePermissionsPolicyDirectiveBuilder() : base("publickey-credentials-create")
    {
    }
}