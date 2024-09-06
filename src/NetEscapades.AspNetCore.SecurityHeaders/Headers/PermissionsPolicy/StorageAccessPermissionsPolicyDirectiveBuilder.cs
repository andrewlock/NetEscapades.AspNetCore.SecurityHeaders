namespace NetEscapades.AspNetCore.SecurityHeaders.Headers.PermissionsPolicy;

/// <summary>
/// The HTTP Permissions-Policy header storage-access directive controls whether a document loaded in a third-party
/// context (i.e. embedded in an iframe) is allowed to use the <code>Storage Access API</code> to request access to unpartitioned
/// cookies. This is relevant to user agents that by default block access to unpartitioned cookies by sites loaded
/// in a third-party context to improve privacy (for example, to prevent tracking). Specifically, where a defined
/// policy blocks use of this feature, <code>Document.requestStorageAccess()</code> calls will return a Promise that rejects
/// with a <code>DOMException</code> of type <code>NotAllowedError</code>
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