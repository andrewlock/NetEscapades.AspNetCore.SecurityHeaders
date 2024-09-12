namespace NetEscapades.AspNetCore.SecurityHeaders.Headers.PermissionsPolicy;

/// <summary>
/// The HTTP <c>Permissions-Policy</c> header <c>publickey-credentials-create</c> directive
/// controls whether the current document is allowed to use the <c>Web Authentication API</c>
/// to create new WebAuthn credentials, i.e., via <c>navigator.credentials.create({publicKey})</c>.
/// Specifically, where a defined policy blocks use of this feature, the <c>Promise</c> returned
/// by <c>navigator.credentials.create({publicKey})</c> will reject with a <c>NotAllowedError DOMException</c>.
/// If the method is called cross-origin. the <c>Promise</c> will also reject with a
/// <c>NotAllowedError</c> if the feature is granted by <c>allow= on an iframe</c> and the frame
/// does not also have <c>Transient activation</c>.
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