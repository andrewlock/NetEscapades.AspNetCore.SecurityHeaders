namespace NetEscapades.AspNetCore.SecurityHeaders.Headers.PermissionsPolicy
{
    /// <summary>
    /// Controls access to autoplay of media requested through the
    /// <code>HTMLMediaElement</code> interface. If disabled in a document,
    /// then calls to <code>play()</code> without a user gesture will
    /// reject the promise with a <code>NotAllowedError</code> DOMException
    /// object as its parameter. The autoplay attribute will be ignored.
    /// </summary>
    public class AutoplayPermissionsPolicyDirectiveBuilder : PermissionsPolicyDirectiveBuilder
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="AutoplayPermissionsPolicyDirectiveBuilder"/> class.
        /// </summary>
        public AutoplayPermissionsPolicyDirectiveBuilder() : base("autoplay")
        {
        }
    }
}
