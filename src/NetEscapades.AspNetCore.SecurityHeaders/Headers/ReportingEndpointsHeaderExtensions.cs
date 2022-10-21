using System;
using NetEscapades.AspNetCore.SecurityHeaders.Headers;

// ReSharper disable once CheckNamespace
namespace Microsoft.AspNetCore.Builder;

/// <summary>
/// Extension methods for adding a <see cref="ReportingEndpointsHeader" /> to a <see cref="HeaderPolicyCollection" />
/// </summary>
public static class ReportingEndpointsHeaderExtensions
{
    /// <summary>
    /// Add a Reporting-Endpoints header to all requests
    /// </summary>
    /// <param name="policies">The collection of policies</param>
    /// <param name="configure">Configure the Reporting-Endpoints header</param>
    /// <returns>The <see cref="HeaderPolicyCollection"/> for method chaining</returns>
    public static HeaderPolicyCollection AddReportingEndpoints(this HeaderPolicyCollection policies, Action<ReportingEndpointsHeaderBuilder> configure)
    {
        return policies.ApplyPolicy(ReportingEndpointsHeader.Build(configure));
    }
}