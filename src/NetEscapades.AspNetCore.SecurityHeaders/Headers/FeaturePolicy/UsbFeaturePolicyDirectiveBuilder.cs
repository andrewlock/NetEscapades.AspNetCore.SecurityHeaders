namespace NetEscapades.AspNetCore.SecurityHeaders.Infrastructure
{
    /// <summary>
    /// Controls whether the current document is allowed to use the WebUSB API.
    /// If disabled then calls to <code>getDevices()</code> should return a 
    /// <code>promise</code> which rejects with a <code>SecurityError</code> DOMException.
    /// </summary>
    public class UsbFeaturePolicyDirectiveBuilder : FeaturePolicyDirectiveBuilder
    {
        public UsbFeaturePolicyDirectiveBuilder() : base("usb")
        {
        }
    }
}