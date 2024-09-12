namespace NetEscapades.AspNetCore.SecurityHeaders.Headers.PermissionsPolicy;

/// <summary>
/// The HTTP <c>Permissions-Policy</c> header <c>identity-credentials-get</c> directive controls whether the current document
/// is allowed to use the <c>Federated Credential Management API (FedCM)</c>, and more specifically the <c>navigator.credentials.get()</c>
/// method with an <c>identity</c> option. Where this policy forbids use of the API, the <c>Promise</c> returned by the <c>get()</c>
/// call will reject with a <c>NotAllowedError DOMException</c>.
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