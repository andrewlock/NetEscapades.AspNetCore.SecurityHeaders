namespace NetEscapades.AspNetCore.SecurityHeaders.Infrastructure
{
    /// <summary>
    /// The frame-src directive specifies valid sources for nested browsing contexts loading 
    /// using elements such as  &lt;frame&gt; and  &lt;iframe&gt;
    /// </summary>
    public class FrameSourceDirectiveBuilder : CspDirectiveBuilder
    {
        public FrameSourceDirectiveBuilder() : base("frame-src")
        {
        }
    }
}