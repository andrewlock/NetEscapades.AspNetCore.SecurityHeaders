namespace NetEscapades.AspNetCore.SecurityHeaders.Infrastructure
{
    /// <summary>
    /// Controls whether the current document is allowed to use 
    /// <code>Element.requestFullScreen()</code>. When this policy is enabled,
    /// the returned <code>Promise</code> rejects with a <code>TypeError</code>.
    /// </summary>
    public class FullscreenFeaturePolicyDirectiveBuilder : FeaturePolicyDirectiveBuilder
    {
        public FullscreenFeaturePolicyDirectiveBuilder() : base("fullscreen")
        {
        }
    }
}