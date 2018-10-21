namespace NetEscapades.AspNetCore.SecurityHeaders.Headers.ContentSecurityPolicy
{
    /// <summary>
    /// The object-src directive specifies valid sources for the &lt;object&gt;, &lt;embed&gt;, and &lt;applet&gt; elements.
    /// </summary>
    public class ObjectSourceDirectiveBuilder : CspDirectiveBuilder
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ObjectSourceDirectiveBuilder"/> class.
        /// </summary>
        public ObjectSourceDirectiveBuilder() : base("object-src")
        {
        }
    }
}