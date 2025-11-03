namespace NetEscapades.AspNetCore.SecurityHeaders.Headers.ContentSecurityPolicy;

/// <summary>
/// The <c>script-src-attr</c> directive specifies valid sources for JavaScript inline event handlers.
/// This directive only specifies valid sources for inline script event handlers like onclick.
///
/// It does not apply to other JavaScript sources that can trigger script execution, such as
/// URLs loaded directly into &lt;script&gt; elements and XSLT stylesheets. (Valid sources
/// can be specified for all JavaScript script sources using <c>script-src</c>, or just for
/// &lt;script&gt; elements using <c>script-src-elem</c>.)
/// </summary>
[CspMixin(MixinTypes.UnsafeHashes | MixinTypes.Hash | MixinTypes.UnsafeInline | MixinTypes.None | MixinTypes.ReportSample)]
public partial class ScriptSourceAttrDirectiveBuilder : CspDirectiveBuilder
{
    /// <summary>
    /// Initializes a new instance of the <see cref="ScriptSourceAttrDirectiveBuilder"/> class.
    /// </summary>
    public ScriptSourceAttrDirectiveBuilder() : base("script-src-attr")
    {
    }
}