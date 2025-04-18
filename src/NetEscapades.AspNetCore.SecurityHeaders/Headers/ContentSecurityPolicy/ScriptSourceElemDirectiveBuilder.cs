namespace NetEscapades.AspNetCore.SecurityHeaders.Headers.ContentSecurityPolicy;

/// <summary>
/// The <c>script-src-elem</c> directive specifies valid sources for JavaScript &lt;script&gt; elements.
///
/// This directive only specifies valid sources in &lt;script&gt; elements (both script requests
/// and blocks). It does not apply to other JavaScript sources that can trigger script execution,
/// such as inline script event handlers (onclick), script execution methods gated on the
/// "unsafe-eval" check, and XSLT stylesheets. (Valid sources can be specified for all
/// JavaScript script sources using <c>script-src</c>, or just for inline script handlers using <c>script-src-attr</c>.)
/// </summary>
public class ScriptSourceElemDirectiveBuilder : CspDirectiveBuilder
{
    /// <summary>
    /// Initializes a new instance of the <see cref="ScriptSourceElemDirectiveBuilder"/> class.
    /// </summary>
    public ScriptSourceElemDirectiveBuilder() : base("script-src-elem")
    {
    }

    /// <summary>
    /// Requires a sample of the violating code to be included in the violation report
    /// </summary>
    /// <returns>The CSP builder for method chaining</returns>
    public ScriptSourceElemDirectiveBuilder ReportSample()
    {
        MustReportSample = true;
        Sources.Add("'report-sample'");
        return this;
    }

    /// <summary>
    /// Allow sources for content generated using the HashTagHelper.
    /// </summary>
    /// <returns>The CSP builder for method chaining</returns>
    public ScriptSourceElemDirectiveBuilder WithHashTagHelper()
    {
        // TODO: check hash algorithm is one of expected values
        SourceBuilders.Add(
            ctx => string.Join(" ", ctx.GetScriptCSPHashes()),
            $"{nameof(ScriptSourceElemDirectiveBuilder)}.{nameof(WithHashTagHelper)}");
        return this;
    }
}