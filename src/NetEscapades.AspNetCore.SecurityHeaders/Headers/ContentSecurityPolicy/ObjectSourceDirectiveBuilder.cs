namespace NetEscapades.AspNetCore.SecurityHeaders.Headers.ContentSecurityPolicy;

/// <summary>
/// The <c>object-src</c> directive specifies valid sources for the &lt;object&gt;, &lt;embed&gt;, and &lt;applet&gt; elements.
/// </summary>
[CspMixin(MixinTypes.HostSource | MixinTypes.SchemeSource | MixinTypes.Self | MixinTypes.None)]
public partial class ObjectSourceDirectiveBuilder : CspDirectiveBuilder
{
    /// <summary>
    /// Initializes a new instance of the <see cref="ObjectSourceDirectiveBuilder"/> class.
    /// </summary>
    public ObjectSourceDirectiveBuilder() : base("object-src")
    {
    }
}