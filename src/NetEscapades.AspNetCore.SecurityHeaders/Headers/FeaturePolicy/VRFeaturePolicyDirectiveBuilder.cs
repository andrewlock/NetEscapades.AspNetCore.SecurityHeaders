namespace NetEscapades.AspNetCore.SecurityHeaders.Headers.FeaturePolicy
{
    /// <summary>
    /// Controls whether the current document is allowed to use the WebVR API.
    /// If disabled then calls to <code>getVRDisplays()</code> should return a
    /// <code>promise</code> which rejects with a <code>SecurityError</code> DOMException.
    /// </summary>
    public class VRFeaturePolicyDirectiveBuilder : FeaturePolicyDirectiveBuilder
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="VRFeaturePolicyDirectiveBuilder"/> class.
        /// </summary>
        public VRFeaturePolicyDirectiveBuilder() : base("vr")
        {
        }
    }
}