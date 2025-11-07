namespace NetEscapades.AspNetCore.SecurityHeaders.Headers.ContentSecurityPolicy;

/// <summary>
/// The <c>style-src-attr</c> directive specifies valid sources for inline styles applied to individual DOM elements.
///
/// The directive does not set valid sources for &lt;style&gt; elements and &lt;link&gt; elements with rel="stylesheet".
/// These are set using <c>style-src-elem</c> (and valid sources for all styles may be set with <c>style-src</c>).
/// </summary>
[CspMixin(MixinTypes.UnsafeInline | MixinTypes.UnsafeHashes | MixinTypes.Hash | MixinTypes.ReportSample | MixinTypes.None)]
public partial class StyleSourceAttrDirectiveBuilder : CspDirectiveBuilder
{
    /// <summary>
    /// Initializes a new instance of the <see cref="StyleSourceAttrDirectiveBuilder"/> class.
    /// </summary>
    public StyleSourceAttrDirectiveBuilder() : base("style-src-attr")
    {
    }
}