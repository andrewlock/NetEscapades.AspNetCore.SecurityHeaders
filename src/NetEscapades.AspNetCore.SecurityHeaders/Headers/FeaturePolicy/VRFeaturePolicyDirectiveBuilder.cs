namespace NetEscapades.AspNetCore.SecurityHeaders.Infrastructure
{
    /// <summary>
    /// Controls whether the current document is allowed to use the WebVR API.
    /// If disabled then calls to <code>getVRDisplays()</code> should return a 
    /// <code>promise</code> which rejects with a <code>SecurityError</code> DOMException.
    /// </summary>
    public class VRFeaturePolicyDirectiveBuilder : FeaturePolicyDirectiveBuilder
    {
        public VRFeaturePolicyDirectiveBuilder() : base("vr")
        {
        }
    }
}