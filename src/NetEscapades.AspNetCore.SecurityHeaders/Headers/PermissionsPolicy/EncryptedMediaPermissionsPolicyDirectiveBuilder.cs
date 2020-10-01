namespace NetEscapades.AspNetCore.SecurityHeaders.Headers.PermissionsPolicy
{
    /// <summary>
    /// Controls whether encrypted media extensions are available. If disabled
    /// The promise returned by <code>requestMediaKeySystemAccess()</code> must
    /// return a promise which rejects with a <code>SecurityError</code> DOMException
    /// object as its parameter.
    /// </summary>
    public class EncryptedMediaPermissionsPolicyDirectiveBuilder : PermissionsPolicyDirectiveBuilder
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="EncryptedMediaPermissionsPolicyDirectiveBuilder"/> class.
        /// </summary>
        public EncryptedMediaPermissionsPolicyDirectiveBuilder() : base("encrypted-media")
        {
        }
    }
}
