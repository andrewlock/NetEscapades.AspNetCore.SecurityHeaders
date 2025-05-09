﻿namespace NetEscapades.AspNetCore.SecurityHeaders.Headers.ContentSecurityPolicy;

/// <summary>
/// The <c>style-src</c> directive specifies valid sources for sources for stylesheets.
/// </summary>
public class StyleSourceDirectiveBuilder : CspDirectiveBuilder
{
    /// <summary>
    /// Initializes a new instance of the <see cref="StyleSourceDirectiveBuilder"/> class.
    /// </summary>
    public StyleSourceDirectiveBuilder() : base("style-src")
    {
    }

    /// <summary>
    /// Requires a sample of the violating code to be included in the violation report
    /// </summary>
    /// <returns>The CSP builder for method chaining</returns>
    public StyleSourceDirectiveBuilder ReportSample()
    {
        MustReportSample = true;
        Sources.Add("'report-sample'");
        return this;
    }

    /// <summary>
    /// Allow sources for content generated using the HashTagHelper.
    /// </summary>
    /// <returns>The CSP builder for method chaining</returns>
    public StyleSourceDirectiveBuilder WithHashTagHelper()
    {
        // TODO: check hash algorithm is one of expected values
        SourceBuilders.Add(
            ctx => string.Join(" ", ctx.GetStyleCSPHashes()),
            $"{nameof(StyleSourceDirectiveBuilder)}.{nameof(WithHashTagHelper)}");
        return this;
    }
}