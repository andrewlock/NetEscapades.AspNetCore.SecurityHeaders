namespace NetEscapades.AspNetCore.SecurityHeaders.Headers.ContentSecurityPolicy;

/// <summary>
/// The <c>style-src</c> directive specifies valid sources for sources for stylesheets.
/// </summary>
[CspMixin(MixinTypes.HostSource | MixinTypes.SchemeSource | MixinTypes.Self | MixinTypes.None | MixinTypes.UnsafeEval
          | MixinTypes.UnsafeInline | MixinTypes.UnsafeHashes | MixinTypes.Hash | MixinTypes.Nonce | MixinTypes.ReportSample)]
public partial class StyleSourceDirectiveBuilder : CspDirectiveBuilder
{
    /// <summary>
    /// Initializes a new instance of the <see cref="StyleSourceDirectiveBuilder"/> class.
    /// </summary>
    public StyleSourceDirectiveBuilder() : base("style-src")
    {
    }
}