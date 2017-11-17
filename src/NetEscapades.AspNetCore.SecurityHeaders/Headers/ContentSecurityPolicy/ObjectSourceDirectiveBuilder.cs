namespace NetEscapades.AspNetCore.SecurityHeaders.Infrastructure
{
    /// <summary>
    /// The object-src directive specifies valid sources for the &lt;object&gt;, &lt;embed&gt;, and &lt;applet&gt; elements
    /// </summary>
    public class ObjectSourceDirectiveBuilder : CspDirectiveBuilder
    {
        public ObjectSourceDirectiveBuilder() : base("object-src")
        {
        }
    }
}