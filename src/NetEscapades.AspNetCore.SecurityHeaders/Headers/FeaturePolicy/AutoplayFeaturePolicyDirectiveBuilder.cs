namespace NetEscapades.AspNetCore.SecurityHeaders.Infrastructure
{
    /// <summary>
    /// Controls access to autoplay of media requested through the
    /// <code>HTMLMediaElement</code> interface. If disabled in a document,
    /// then calls to <code>play()</code> without a user gesture will
    /// reject the promise with a <code>NotAllowedError</code> DOMException
    /// object as its parameter. The autoplay attribute will be ignored.
    /// </summary>
    public class AutoplayFeaturePolicyDirectiveBuilder : FeaturePolicyDirectiveBuilder
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="AutoplayFeaturePolicyDirectiveBuilder"/> class.
        /// </summary>
        public AutoplayFeaturePolicyDirectiveBuilder() : base("autoplay")
        {
        }
    }
}