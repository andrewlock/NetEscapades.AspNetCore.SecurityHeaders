namespace NetEscapades.AspNetCore.SecurityHeaders.Headers.PermissionsPolicy
{
    /// <summary>
    /// Controls whether the current document is allowed to use synchronous XMLHttpRequest transfers.
    /// If disabled in a document, then calls to <code>send()</code> on XMLHttpRequest objects
    /// will throw a <code>NetworkError</code>.
    /// </summary>
    public class SynchronousXhrPermissionsPolicyDirectiveBuilder : PermissionsPolicyDirectiveBuilder
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SynchronousXhrPermissionsPolicyDirectiveBuilder"/> class.
        /// </summary>
        public SynchronousXhrPermissionsPolicyDirectiveBuilder() : base("sync-xhr")
        {
        }
    }
}
