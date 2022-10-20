using System;
using Microsoft.AspNetCore.Http;

namespace NetEscapades.AspNetCore.SecurityHeaders.Headers.ContentSecurityPolicy;

/// <summary>
/// The report-to directive instructs the user agent to send requests to
/// an endpoint defined in a <c>Report-To</c> HTTP header. The directive
/// has no effect in and of itself, but only gains meaning in
/// combination with other reporting directives.
/// </summary>
public class ReportToDirectiveBuilder : CspDirectiveBuilderBase
{
    /// <summary>
    /// Initializes a new instance of the <see cref="ReportToDirectiveBuilder"/> class.
    /// </summary>
    /// <param name="groupName">The name of the group in the <code>Report-To</code> JSON field
    /// to send reports to</param>
    public ReportToDirectiveBuilder(string groupName) : base("report-to")
    {
        if (string.IsNullOrEmpty(groupName))
        {
            throw new ArgumentException("Value cannot be null or empty.", nameof(groupName));
        }

        GroupName = groupName;
    }

    private string GroupName { get; }

    /// <inheritdoc />
    internal override Func<HttpContext, string> CreateBuilder()
    {
        return _ => $"{Directive} {GroupName}";
    }
}