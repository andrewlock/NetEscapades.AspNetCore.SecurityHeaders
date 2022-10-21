using System;
using System.Collections.Generic;
using System.Linq;

namespace NetEscapades.AspNetCore.SecurityHeaders.Headers;

/// <summary>
/// Used to build a Reporting-Endpoints header.
/// </summary>
public class ReportingEndpointsHeaderBuilder
{
    private const string Separator = ", ";
    private const string DefaultName = "default";

    private readonly List<KeyValuePair<string, string>> _endpoints = new();

    /// <summary>
    /// Adds a reporting endpoint with the name <c>default</c> and url <paramref name="url"/>.
    /// The name of the endpoint is used to identify the endpoint in other security headers,
    /// such as the <c>Content-Security-Policy</c> <c>report-to</c> directive. The <c>default</c>
    /// endpoint must be defined to receive some reports such as deprecation and intervention reports.
    /// </summary>
    /// <param name="url">The url for the endpoint. Must be a valid, absolute, path</param>
    /// <returns>The <see cref="ReportingEndpointsHeaderBuilder"/> for method chaining</returns>
    public ReportingEndpointsHeaderBuilder AddDefaultEndpoint(string url) => AddEndpoint(DefaultName, url);

    /// <summary>
    /// Adds a reporting endpoint with the name <c>default</c> and url <paramref name="url"/>.
    /// The name of the endpoint is used to identify the endpoint in other security headers,
    /// such as the <c>Content-Security-Policy</c> <c>report-to</c> directive. The <c>default</c>
    /// endpoint must be defined to receive some reports such as deprecation and intervention reports.
    /// </summary>
    /// <param name="url">The url for the endpoint. Must be a valid, absolute, path</param>
    /// <returns>The <see cref="ReportingEndpointsHeaderBuilder"/> for method chaining</returns>
    public ReportingEndpointsHeaderBuilder AddDefaultEndpoint(Uri url) => AddEndpoint(DefaultName, url);

    /// <summary>
    /// Adds a reporting endpoint with the defined <paramref name="name"/> and <paramref name="url"/>
    /// </summary>
    /// <param name="name">The name of the endpoint. This value is used to identify the endpoint
    /// in other security headers, such as the <c>Content-Security-Policy</c> <c>report-to</c> directive</param>
    /// <param name="url">The url for the endpoint. Must be a valid, absolute, path</param>
    /// <returns>The <see cref="ReportingEndpointsHeaderBuilder"/> for method chaining</returns>
    public ReportingEndpointsHeaderBuilder AddEndpoint(string name, string url)
    {
        if (string.IsNullOrEmpty(name))
        {
            throw new ArgumentNullException(nameof(name));
        }

        if (string.IsNullOrEmpty(url))
        {
            throw new ArgumentNullException(nameof(url));
        }

        if (!Uri.TryCreate(url, UriKind.Absolute, out var uri) || !IsValid(uri))
        {
            throw new ArgumentException($"The provided url {url} was not a valid absolute URL", nameof(url));
        }

        if (string.Equals(uri.PathAndQuery, "/", StringComparison.Ordinal) && !url.EndsWith("/", StringComparison.Ordinal))
        {
            // endpoints must have a path that starts with /
            url += "/";
        }

        AddToList(name, url);
        return this;
    }

    /// <summary>
    /// Adds a reporting endpoint with the defined <paramref name="name"/> and <paramref name="url"/>
    /// </summary>
    /// <param name="name">The name of the endpoint. This value is used to identify the endpoint
    /// in other security headers, such as the <c>Content-Security-Policy</c> <c>report-to</c> directive</param>
    /// <param name="url">The url for the endpoint. Must be a valid, absolute, path</param>
    /// <returns>The <see cref="ReportingEndpointsHeaderBuilder"/> for method chaining</returns>
    public ReportingEndpointsHeaderBuilder AddEndpoint(string name, Uri url)
    {
        if (string.IsNullOrEmpty(name))
        {
            throw new ArgumentNullException(nameof(name));
        }

        if (url is null)
        {
            throw new ArgumentNullException(nameof(url));
        }

        if (!IsValid(url))
        {
            throw new ArgumentException($"The provided url '{url}' was not a valid absolute URL", nameof(url));
        }

        AddToList(name, url.ToString());
        return this;
    }

    private void AddToList(string name, string uri)
    {
        foreach (var endpoint in _endpoints)
        {
            if (string.Equals(endpoint.Key, name, StringComparison.Ordinal))
            {
                throw new InvalidOperationException($"Can't add endpoint '{name}' with url '{uri}', as endpoint is already defined as '{endpoint.Value}'");
            }
        }

        _endpoints.Add(new KeyValuePair<string, string>(name, uri));
    }

    /// <summary>
    /// Build the Reporting-Endpoints header value
    /// </summary>
    /// <returns>The <see cref="ReportingEndpointsHeader"/> value</returns>
    internal string Build()
    {
        return string.Join(Separator, _endpoints.Select(x => $"{x.Key}=\"{x.Value}\""));
    }

    private static bool IsValid(Uri uri)
        => uri.IsAbsoluteUri && !string.IsNullOrEmpty(uri.Host) && !string.IsNullOrEmpty(uri.Scheme);
}