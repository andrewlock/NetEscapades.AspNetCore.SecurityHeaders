namespace NetEscapades.AspNetCore.SecurityHeaders.Headers.PermissionsPolicy;

/// <summary>
/// The HTTP Permissions-Policy header identity-credentials-get directive controls whether
/// the current document is allowed to use the <code>Federated Credential Management API (FedCM)</code>
/// and more specifically the <code>navigator.credentials.get()</code> method with an identity option.
/// Where this policy forbids use of the API, the Promise returned by the <code>get()</code> call
/// will reject with a <code>NotAllowedError DOMException</code>
/// </summary>
public class IdentityCredentialsGetPermissionsPolicyDirectiveBuilder : PermissionsPolicyDirectiveBuilder
{
    /// <summary>
    /// Initializes a new instance of the <see cref="IdentityCredentialsGetPermissionsPolicyDirectiveBuilder"/> class.
    /// </summary>
    public IdentityCredentialsGetPermissionsPolicyDirectiveBuilder() : base("identity-credentials-get")
    {
    }
}