namespace NetEscapades.AspNetCore.SecurityHeaders.Headers.ContentSecurityPolicy;

/// <summary>
/// The script-src-attr directive specifies valid sources for JavaScript inline event handlers.
/// This directive only specifies valid sources for inline script event handlers like onclick.
///
/// It does not apply to other JavaScript sources that can trigger script execution, such as
/// URLs loaded directly into &lt;script&gt; elements and XSLT stylesheets. (Valid sources
/// can be specified for all JavaScript script sources using script-src, or just for
/// &lt;script&gt; elements using script-src-elem.)
/// </summary>
public class ScriptSourceAttrDirectiveBuilder : CspDirectiveBuilder
{
    /// <summary>
    /// Initializes a new instance of the <see cref="ScriptSourceAttrDirectiveBuilder"/> class.
    /// </summary>
    public ScriptSourceAttrDirectiveBuilder() : base("script-src-attr")
    {
    }

    /// <summary>
    /// Requires a sample of the violating code to be included in the violation report
    /// </summary>
    /// <returns>The CSP builder for method chaining</returns>
    public ScriptSourceAttrDirectiveBuilder ReportSample()
    {
        MustReportSample = true;
        Sources.Add("'report-sample'");
        return this;
    }

    /// <summary>
    /// Allow sources for content generated using the HashTagHelper.
    /// </summary>
    /// <returns>The CSP builder for method chaining</returns>
    public ScriptSourceAttrDirectiveBuilder WithHashTagHelper()
    {
        // TODO: check hash algorithm is one of expected values
        SourceBuilders.Add(ctx =>
        {
            return string.Join(" ", ctx.GetScriptCSPHashes());
        });
        return this;
    }
}