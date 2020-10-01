namespace NetEscapades.AspNetCore.SecurityHeaders.Headers.PermissionsPolicy
{
    /// <summary>
    /// Controls whether the current document is allowed to use the WebVR API.
    /// If disabled then calls to <code>getVRDisplays()</code> should return a
    /// <code>promise</code> which rejects with a <code>SecurityError</code> DOMException.
    /// </summary>
    public class VRPermissionsPolicyDirectiveBuilder : PermissionsPolicyDirectiveBuilder
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="VRPermissionsPolicyDirectiveBuilder"/> class.
        /// </summary>
        public VRPermissionsPolicyDirectiveBuilder() : base("vr")
        {
        }
    }
}
