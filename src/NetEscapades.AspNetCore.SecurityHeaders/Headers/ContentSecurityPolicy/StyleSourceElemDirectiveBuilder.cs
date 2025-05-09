namespace NetEscapades.AspNetCore.SecurityHeaders.Headers.ContentSecurityPolicy;

/// <summary>
/// The <c>style-src-elem</c> directive specifies valid sources for stylesheet &lt;style&gt; elements
/// and &lt;link&gt; elements with rel="stylesheet".
///
/// The directive does not set valid sources for inline style attributes; these are
/// set using <c>style-src-attr</c> (and valid sources for all styles may be set with <c>style-src</c>).
/// </summary>
public class StyleSourceElemDirectiveBuilder : CspDirectiveBuilder
{
    /// <summary>
    /// Initializes a new instance of the <see cref="StyleSourceElemDirectiveBuilder"/> class.
    /// </summary>
    public StyleSourceElemDirectiveBuilder() : base("style-src-elem")
    {
    }

    /// <summary>
    /// Requires a sample of the violating code to be included in the violation report
    /// </summary>
    /// <returns>The CSP builder for method chaining</returns>
    public StyleSourceElemDirectiveBuilder ReportSample()
    {
        MustReportSample = true;
        Sources.Add("'report-sample'");
        return this;
    }

    /// <summary>
    /// Allow sources for content generated using the HashTagHelper.
    /// </summary>
    /// <returns>The CSP builder for method chaining</returns>
    public StyleSourceElemDirectiveBuilder WithHashTagHelper()
    {
        // TODO: check hash algorithm is one of expected values
        SourceBuilders.Add(
            ctx => string.Join(" ", ctx.GetStyleCSPHashes()),
            $"{nameof(StyleSourceElemDirectiveBuilder)}.{nameof(WithHashTagHelper)}");
        return this;
    }
}