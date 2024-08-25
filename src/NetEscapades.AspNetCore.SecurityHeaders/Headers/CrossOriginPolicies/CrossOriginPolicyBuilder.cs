using System.Collections.Generic;
using System.Linq;
using NetEscapades.AspNetCore.SecurityHeaders.Headers.CrossOriginPolicies;

// ReSharper disable once CheckNamespace
namespace Microsoft.AspNetCore.Builder;

/// <summary>
/// Used to build a Cross Origin Policy header from multiple directives.
/// </summary>
public abstract class CrossOriginPolicyBuilder
{
    private const string _directiveSeparator = "; ";

    private readonly Dictionary<string, CrossOriginPolicyDirectiveBuilderBase> _directives =
        new Dictionary<string, CrossOriginPolicyDirectiveBuilderBase>();

    /// <summary>
    /// The report-to directive instructs the user agent to report attempts to
    /// violate the Cross Origin Policy. These violation reports consist of
    /// JSON documents sent via an HTTP POST request to the specified reporting endpoint.
    /// </summary>
    /// <returns>A configured <see cref="ReportDirectiveBuilder"/></returns>
    public ReportDirectiveBuilder AddReport() => AddDirective(new ReportDirectiveBuilder());

    /// <summary>
    /// Adds a directive for the cross origin policy
    /// </summary>
    /// <typeparam name="T">The type of the directive</typeparam>
    /// <param name="directive">The directive corresponding the concrete implementation of the cross origin policy.</param>
    /// <returns>A configured <see cref="CrossOriginPolicyDirectiveBuilderBase"/> implementation.</returns>
    protected T AddDirective<T>(T directive) where T : CrossOriginPolicyDirectiveBuilderBase
    {
        _directives[directive.Directive] = directive;
        return directive;
    }

    /// <summary>
    /// Build the Cross Origin Policy directive
    /// </summary>
    /// <returns>The Cross Origin Policy directive as a string</returns>
    internal CrossOriginPolicyBuilderResult Build()
    {
        // build the constant values ahead of time
        var staticDirectives = _directives.Values
            .Select(x => x.CreateBuilder().Invoke(null!))
            .Where(x => !string.IsNullOrEmpty(x));

        var constantDirective = string.Join(_directiveSeparator, staticDirectives);

        return CrossOriginPolicyBuilderResult.CreateStaticResult(constantDirective);
    }
}