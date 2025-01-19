using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Http;

namespace NetEscapades.AspNetCore.SecurityHeaders.Headers.ContentSecurityPolicy;

/// <summary>
/// The <c>require-trusted-types-for</c> directive instructs user agents
/// to control the data passed to DOM XSS sink functions, like
/// <see href="https://developer.mozilla.org/en-US/docs/Web/API/Element/innerHTML">Element.innerHTML</see>
/// setter.
///
/// When used, those functions only accept non-spoofable, typed values created by Trusted Type
/// policies, and reject strings. Together with <see cref="TrustedTypesDirectiveBuilder"/> which
/// guards creation of Trusted Type policies, this allows authors to define rules guarding
/// writing values to the DOM and thus reducing the DOM XSS attack surface to small,
/// isolated parts of the web application codebase, facilitating their monitoring and code review.
/// </summary>
public class RequireTrustedTypesForDirectiveBuilder : CspDirectiveBuilderBase
{
    private const string Separator = " ";

    /// <summary>
    /// Initializes a new instance of the <see cref="RequireTrustedTypesForDirectiveBuilder"/> class.
    /// </summary>
    public RequireTrustedTypesForDirectiveBuilder() : base("require-trusted-types-for")
    {
    }

    /// <summary>
    /// The sink groups tokens to apply.
    /// </summary>
    private List<string> SinkGroups { get; } = new(1); // using a single capacity because currently only 1 group is defined

    /// <summary>
    /// Mark the <c>script</c> sink group as requiring trusted values
    /// </summary>
    /// <returns>The CSP builder for method chaining</returns>
    public RequireTrustedTypesForDirectiveBuilder Script()
    {
        SinkGroups.Add("'script'");
        return this;
    }

    /// <summary>
    /// Adds a custom sink group to the required-trusted-types-for directive.
    /// Useful for adding experimental sink group.
    /// </summary>
    /// <returns>The CSP builder for method chaining</returns>
    /// <param name="sinkGroup">The sink group to add</param>
    public RequireTrustedTypesForDirectiveBuilder CustomSinkGroup(string sinkGroup)
    {
        SinkGroups.Add(sinkGroup);
        return this;
    }

    /// <inheritdoc />
    internal override Func<HttpContext, string> CreateBuilder()
    {
        // Technically, no sink groups is a misconfiguration
        // TODO: report this in ILogger somehow
        var sinkGroups = string.Join(Separator, SinkGroups);
        var result = string.IsNullOrEmpty(sinkGroups) ? Directive : $"{Directive} {sinkGroups}";

        return _ => result;
    }
}