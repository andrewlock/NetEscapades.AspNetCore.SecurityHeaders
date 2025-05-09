namespace NetEscapades.AspNetCore.SecurityHeaders.Headers.ContentSecurityPolicy;

/// <summary>
/// The <c>style-src-attr</c> directive specifies valid sources for inline styles applied to individual DOM elements.
///
/// The directive does not set valid sources for &lt;style&gt; elements and &lt;link&gt; elements with rel="stylesheet".
/// These are set using <c>style-src-elem</c> (and valid sources for all styles may be set with <c>style-src</c>).
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
        SourceBuilders.Add(
            ctx => string.Join(" ", ctx.GetStyleCSPHashes()),
            $"{nameof(StyleSourceAttrDirectiveBuilder)}.{nameof(WithHashTagHelper)}");
        return this;
    }
}