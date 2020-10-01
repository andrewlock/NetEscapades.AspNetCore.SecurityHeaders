namespace NetEscapades.AspNetCore.SecurityHeaders.Headers.PermissionsPolicy
{
    /// <summary>
    /// Controls whether the current document is allowed to use the WebUSB API.
    /// If disabled then calls to <code>getDevices()</code> should return a
    /// <code>promise</code> which rejects with a <code>SecurityError</code> DOMException.
    /// </summary>
    public class UsbPermissionsPolicyDirectiveBuilder : PermissionsPolicyDirectiveBuilder
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="UsbPermissionsPolicyDirectiveBuilder"/> class.
        /// </summary>
        public UsbPermissionsPolicyDirectiveBuilder() : base("usb")
        {
        }
    }
}
