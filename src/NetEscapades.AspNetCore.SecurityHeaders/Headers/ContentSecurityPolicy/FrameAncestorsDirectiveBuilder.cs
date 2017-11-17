namespace NetEscapades.AspNetCore.SecurityHeaders.Infrastructure
{
    /// <summary>
    /// The frame-ancestors directive specifies valid parents that may embed a page using 
    /// &lt;frame&gt;, &lt;iframe&gt;, &lt;object&gt;, &lt;embed&gt;, or &lt;applet&gt;.
    /// Setting this directive to 'none' is similar to X-Frame-Options: DENY (which is also supported in older browers).
    /// </summary>
    public class FrameAncestorsDirectiveBuilder : CspDirectiveBuilder
    {
        public FrameAncestorsDirectiveBuilder() : base("frame-ancestors")
        {
        }
    }
}