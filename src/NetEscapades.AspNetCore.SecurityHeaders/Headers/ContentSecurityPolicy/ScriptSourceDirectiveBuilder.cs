namespace NetEscapades.AspNetCore.SecurityHeaders.Headers.ContentSecurityPolicy;

/// <summary>
/// The script-src directive specifies valid sources for sources for JavaScript.
/// This includes not only URLs loaded directly into &lt;script&gt; elements, but also things
/// like inline script event handlers (onclick) and XSLT stylesheets which can trigger script execution.
/// </summary>
public class ScriptSourceDirectiveBuilder : CspDirectiveBuilder
{
    /// <summary>
    /// Initializes a new instance of the <see cref="ScriptSourceDirectiveBuilder"/> class.
    /// </summary>
    public ScriptSourceDirectiveBuilder() : base("script-src")
    {
    }

    /// <summary>
    /// Requires a sample of the violating code to be included in the violation report
    /// </summary>
    /// <returns>The CSP builder for method chaining</returns>
    public ScriptSourceDirectiveBuilder ReportSample()
    {
        MustReportSample = true;
        Sources.Add("'report-sample'");
        return this;
    }

    /// <summary>
    /// Allow sources for content generated using the HashTagHelper.
    /// </summary>
    /// <returns>The CSP builder for method chaining</returns>
    public ScriptSourceDirectiveBuilder WithHashTagHelper()
    {
        // TODO: check hash algorithm is one of expected values
        SourceBuilders.Add(ctx =>
        {
            return string.Join(" ", ctx.GetScriptCSPHashes());
        });
        return this;
    }
}