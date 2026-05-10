using NetEscapades.AspNetCore.SecurityHeaders.Headers;

// ReSharper disable once CheckNamespace
namespace Microsoft.AspNetCore.Builder;

/// <summary>
/// Extension methods for adding a <see cref="ClearSiteDataHeader" /> to a <see cref="HeaderPolicyCollection" />.
/// </summary>
/// <seealso href="https://developer.mozilla.org/en-US/docs/Web/HTTP/Reference/Headers/Clear-Site-Data"/>
public static class ClearSiteDataHeaderExtensions
{
    /// <summary>
    /// Add a <c>Clear-Site-Data</c> header to all responses, using the supplied builder
    /// action to choose which data the browser should clear. The header is only honoured
    /// by browsers in secure (HTTPS) contexts and is suppressed on plain HTTP responses,
    /// so the typical use is to apply it to a named policy attached to a specific endpoint
    /// (for example a logout endpoint) via <see cref="EndpointConventionBuilderExtensions"/>.
    /// </summary>
    /// <param name="policies">The collection of policies</param>
    /// <param name="configure">Configure the <c>Clear-Site-Data</c> directives.</param>
    /// <returns>The <see cref="HeaderPolicyCollection"/> for method chaining</returns>
    public static HeaderPolicyCollection AddClearSiteData(this HeaderPolicyCollection policies, Action<ClearSiteDataBuilder> configure)
    {
        return policies.ApplyPolicy(ClearSiteDataHeader.Build(configure));
    }

    /// <summary>
    /// Add a <c>Clear-Site-Data</c> header clearing the standard set of data that a
    /// browser should drop on logout: <c>"cache"</c>, <c>"cookies"</c>, <c>"storage"</c>,
    /// and <c>"executionContexts"</c>. The header is only honoured by browsers in secure
    /// (HTTPS) contexts.
    /// </summary>
    /// <param name="policies">The collection of policies</param>
    /// <returns>The <see cref="HeaderPolicyCollection"/> for method chaining</returns>
    public static HeaderPolicyCollection AddClearSiteDataForLogout(this HeaderPolicyCollection policies)
    {
        return policies.AddClearSiteData(builder => builder
            .Cache()
            .Cookies()
            .Storage()
            .ExecutionContexts());
    }

    /// <summary>
    /// Add a <c>Clear-Site-Data</c> header with the wildcard value <c>"*"</c>, instructing
    /// the browser to clear all data types for the origin of the response. The header is
    /// only honoured by browsers in secure (HTTPS) contexts.
    /// </summary>
    /// <param name="policies">The collection of policies</param>
    /// <returns>The <see cref="HeaderPolicyCollection"/> for method chaining</returns>
    public static HeaderPolicyCollection AddClearSiteDataAll(this HeaderPolicyCollection policies)
    {
        return policies.AddClearSiteData(builder => builder.All());
    }
}
