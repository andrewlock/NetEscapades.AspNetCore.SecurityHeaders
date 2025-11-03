namespace NetEscapades.AspNetCore.SecurityHeaders.Headers.ContentSecurityPolicy;

/// <summary>
/// The <c>script-src</c> directive specifies valid sources for sources for JavaScript.
/// This includes not only URLs loaded directly into &lt;script&gt; elements, but also things
/// like inline script event handlers (onclick) and XSLT stylesheets which can trigger script execution.
/// </summary>
[CspMixin(MixinTypes.All)]
public partial class ScriptSourceDirectiveBuilder : CspDirectiveBuilder
{
    /// <summary>
    /// Initializes a new instance of the <see cref="ScriptSourceDirectiveBuilder"/> class.
    /// </summary>
    public ScriptSourceDirectiveBuilder() : base("script-src")
    {
    }
}