namespace NetEscapades.AspNetCore.SecurityHeaders.Headers.ContentSecurityPolicy;

/// <summary>
/// The <c>media-src</c> directive specifies valid sources for loading media using the &lt;audio&gt; and &lt;video&gt; elements.
/// </summary>
[CspMixin(MixinTypes.HostSource | MixinTypes.SchemeSource | MixinTypes.Self | MixinTypes.None)]
public partial class MediaSourceDirectiveBuilder : CspDirectiveBuilder
{
    /// <summary>
    /// Initializes a new instance of the <see cref="MediaSourceDirectiveBuilder"/> class.
    /// </summary>
    public MediaSourceDirectiveBuilder() : base("media-src")
    {
    }
}