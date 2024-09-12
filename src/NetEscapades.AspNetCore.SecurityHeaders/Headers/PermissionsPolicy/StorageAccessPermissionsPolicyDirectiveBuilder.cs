namespace NetEscapades.AspNetCore.SecurityHeaders.Headers.PermissionsPolicy;

/// <summary>
/// The HTTP <c>Permissions-Policy</c> header <c>storage-access</c> directive controls whether
/// a document loaded in a third-party context (i.e. embedded in an <c>iframe</c>) is
/// allowed to use the <c>Storage Access API</c> to request access to unpartitioned cookies.
/// This is relevant to user agents that by default block access to unpartitioned
/// cookies by sites loaded in a third-party context to improve privacy
/// (for example, to prevent tracking). Specifically, where a defined policy
/// blocks use of this feature, <c>Document.requestStorageAccess()</c> calls will
/// return a <c>Promise</c> that rejects with a <c>DOMException</c> of type <c>NotAllowedError</c>.
/// </summary>
public class StorageAccessPermissionsPolicyDirectiveBuilder : PermissionsPolicyDirectiveBuilder
{
    /// <summary>
    /// Initializes a new instance of the <see cref="StorageAccessPermissionsPolicyDirectiveBuilder"/> class.
    /// </summary>
    public StorageAccessPermissionsPolicyDirectiveBuilder() : base("storage-access")
    {
    }
}