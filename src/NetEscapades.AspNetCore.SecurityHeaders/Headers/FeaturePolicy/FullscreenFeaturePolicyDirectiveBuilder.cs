namespace NetEscapades.AspNetCore.SecurityHeaders.Headers.FeaturePolicy
{
    /// <summary>
    /// Controls whether the current document is allowed to use
    /// <code>Element.requestFullScreen()</code>. When this policy is enabled,
    /// the returned <code>Promise</code> rejects with a <code>TypeError</code>.
    /// </summary>
    public class FullscreenFeaturePolicyDirectiveBuilder : FeaturePolicyDirectiveBuilder
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="FullscreenFeaturePolicyDirectiveBuilder"/> class.
        /// </summary>
        public FullscreenFeaturePolicyDirectiveBuilder() : base("fullscreen")
        {
        }
    }
}