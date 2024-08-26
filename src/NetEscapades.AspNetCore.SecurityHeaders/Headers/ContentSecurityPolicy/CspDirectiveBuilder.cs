using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Http;

namespace NetEscapades.AspNetCore.SecurityHeaders.Headers.ContentSecurityPolicy;

/// <summary>
/// Used to build a CSP directive that has a standard set of sources.
/// </summary>
public class CspDirectiveBuilder : CspDirectiveBuilderBase
{
    private const string Separator = " ";

    /// <summary>
    /// Initializes a new instance of the <see cref="CspDirectiveBuilder"/> class.
    /// </summary>
    /// <param name="directive">The name of the directive</param>
    public CspDirectiveBuilder(string directive) : base(directive)
    {
    }

    /// <summary>
    /// The sources from which the directive is allowed.
    /// </summary>
    public SourceCollection Sources { get; } = new();

    /// <summary>
    /// A collection of functions which is used to generate the sources for which a directive is
    /// allowed for a given request
    /// </summary>
    internal SourceBuilderCollection SourceBuilders { get; } = new();

    /// <summary>
    /// If true, the header directives are unique per request, and require
    /// runtime formatting (e.g. for use with Nonce).
    /// </summary>
    internal override bool HasPerRequestValues => SourceBuilders.Any();

    /// <summary>
    /// If true, no sources are allowed.
    /// </summary>
    public bool BlockResources { get; set; } = false;

    /// <summary>
    /// If true, adds the 'report-sample' to the directive.
    /// </summary>
    internal bool MustReportSample { get; set; }

    /// <inheritdoc />
    internal override Func<HttpContext, string> CreateBuilder()
    {
        if (BlockResources)
        {
            return MustReportSample
                ? _ => GetPolicy("'report-sample' 'none'")
                : _ => GetPolicy("'none'");
        }

        var sources = string.Join(Separator, Sources);

        if (!HasPerRequestValues)
        {
            var directive = GetPolicy(sources);
            return _ => directive;
        }

        // Copy, so calls to CreateBuilder are idempotent
        var builders = SourceBuilders.ToList();

        if (!string.IsNullOrEmpty(sources))
        {
            // insert the constant sources first, just for aesthetics
            builders.Insert(0, ctx => sources);
        }

        return ctx =>
        {
            var dynamicSources = builders
                .Select(builder => builder.Invoke(ctx))
                .Where(str => !string.IsNullOrEmpty(str));
            return GetPolicy(string.Join(Separator, dynamicSources));
        };
    }

    private string GetPolicy(string value)
    {
        if (string.IsNullOrEmpty(value))
        {
            return string.Empty;
        }

        return $"{Directive} {value}";
    }
}