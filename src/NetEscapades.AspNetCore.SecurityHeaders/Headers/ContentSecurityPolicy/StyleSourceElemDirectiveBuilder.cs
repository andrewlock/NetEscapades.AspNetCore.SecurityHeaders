namespace NetEscapades.AspNetCore.SecurityHeaders.Headers.ContentSecurityPolicy;

/// <summary>
/// The <c>style-src-elem</c> directive specifies valid sources for stylesheet &lt;style&gt; elements
/// and &lt;link&gt; elements with rel="stylesheet".
///
/// The directive does not set valid sources for inline style attributes; these are
/// set using <c>style-src-attr</c> (and valid sources for all styles may be set with <c>style-src</c>).
/// </summary>
[CspMixin(MixinTypes.HostSource | MixinTypes.SchemeSource | MixinTypes.Self | MixinTypes.None | MixinTypes.UnsafeEval
          | MixinTypes.UnsafeInline | MixinTypes.Hash | MixinTypes.Nonce | MixinTypes.ReportSample)]
public partial class StyleSourceElemDirectiveBuilder : CspDirectiveBuilder
{
    /// <summary>
    /// Initializes a new instance of the <see cref="StyleSourceElemDirectiveBuilder"/> class.
    /// </summary>
    public StyleSourceElemDirectiveBuilder() : base("style-src-elem")
    {
    }
}