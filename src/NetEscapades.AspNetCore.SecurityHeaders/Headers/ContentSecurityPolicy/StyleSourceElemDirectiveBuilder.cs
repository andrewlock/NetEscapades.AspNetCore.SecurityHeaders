namespace NetEscapades.AspNetCore.SecurityHeaders.Headers.ContentSecurityPolicy;

/// <summary>
/// The style-src-elem directive specifies valid sources for inline styles applied to individual DOM elements.
///
/// The directive does not set valid sources for &lt;style&gt; elements and &lt;link&gt; elements with rel="stylesheet".
/// These are set using style-src-elem (and valid sources for all styles may be set with style-src).
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
        Sources.Add("'report-sample'");
        return this;
    }

    /// <summary>
    /// Allow sources for content generated using the the HashTagHelper.
    /// </summary>
    /// <returns>The CSP builder for method chaining</returns>
    public StyleSourceElemDirectiveBuilder WithHashTagHelper()
    {
        // TODO: check hash algorithm is one of expected values
        SourceBuilders.Add(ctx =>
        {
            return string.Join(" ", ctx.GetStyleCSPHashes());
        });
        return this;
    }
}