namespace NetEscapades.AspNetCore.SecurityHeaders.Headers.ContentSecurityPolicy;

/// <summary>
/// The style-src-attr directive specifies valid sources for inline styles applied to individual DOM elements.
///
/// The directive does not set valid sources for &lt;style&gt; elements and &lt;link&gt; elements with rel="stylesheet".
/// These are set using style-src-elem (and valid sources for all styles may be set with style-src).
/// </summary>
public class StyleSourceAttrDirectiveBuilder : CspDirectiveBuilder
{
    /// <summary>
    /// Initializes a new instance of the <see cref="StyleSourceAttrDirectiveBuilder"/> class.
    /// </summary>
    public StyleSourceAttrDirectiveBuilder() : base("style-src-attr")
    {
    }

    /// <summary>
    /// Requires a sample of the violating code to be included in the violation report
    /// </summary>
    /// <returns>The CSP builder for method chaining</returns>
    public StyleSourceAttrDirectiveBuilder ReportSample()
    {
        MustReportSample = true;
        Sources.Add("'report-sample'");
        return this;
    }

    /// <summary>
    /// Allow sources for content generated using the HashTagHelper.
    /// </summary>
    /// <returns>The CSP builder for method chaining</returns>
    public StyleSourceAttrDirectiveBuilder WithHashTagHelper()
    {
        // TODO: check hash algorithm is one of expected values
        SourceBuilders.Add(ctx =>
        {
            return string.Join(" ", ctx.GetStyleCSPHashes());
        });
        return this;
    }
}