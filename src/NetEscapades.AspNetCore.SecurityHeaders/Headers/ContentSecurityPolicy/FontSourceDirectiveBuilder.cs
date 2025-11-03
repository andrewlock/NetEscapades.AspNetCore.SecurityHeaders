namespace NetEscapades.AspNetCore.SecurityHeaders.Headers.ContentSecurityPolicy;

/// <summary>
/// The <c>font-src</c> directive specifies valid sources for fonts loaded using @font-face.
/// </summary>
[CspMixin(MixinTypes.HostSource | MixinTypes.SchemeSource | MixinTypes.Self | MixinTypes.None)]
public partial class FontSourceDirectiveBuilder : CspDirectiveBuilder
{
    /// <summary>
    /// Initializes a new instance of the <see cref="FontSourceDirectiveBuilder"/> class.
    /// </summary>
    public FontSourceDirectiveBuilder() : base("font-src")
    {
    }
}