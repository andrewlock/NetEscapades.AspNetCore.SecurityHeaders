// ReSharper disable once CheckNamespace
namespace Microsoft.AspNetCore.Builder;

/// <summary>
/// Used to build a <c>Clear-Site-Data</c> header value from one or more directives.
/// Browsers ignore the header outside secure (HTTPS) contexts.
/// </summary>
/// <seealso href="https://developer.mozilla.org/en-US/docs/Web/HTTP/Reference/Headers/Clear-Site-Data"/>
public class ClearSiteDataBuilder
{
    private readonly HashSet<string> _directives = new(StringComparer.Ordinal);
    private bool _all;

    /// <summary>
    /// The server signals that the client should remove locally cached data (the browser cache)
    /// for the origin of the response URL. Depending on the browser, this may also clear
    /// pre-rendered pages, the back/forward cache, script caches, WebGL shader caches,
    /// and address bar suggestions.
    /// </summary>
    /// <returns>The <see cref="ClearSiteDataBuilder"/> for method chaining</returns>
    public ClearSiteDataBuilder Cache()
    {
        _directives.Add("\"cache\"");
        return this;
    }

    /// <summary>
    /// The server signals that the client should remove all cookies for the origin of the response URL.
    /// HTTP authentication credentials are also cleared. This affects the entire registered domain,
    /// including subdomains — for example, a response from <c>https://example.com</c> will also
    /// clear cookies for <c>https://stage.example.com</c>.
    /// </summary>
    /// <returns>The <see cref="ClearSiteDataBuilder"/> for method chaining</returns>
    public ClearSiteDataBuilder Cookies()
    {
        _directives.Add("\"cookies\"");
        return this;
    }

    /// <summary>
    /// The server signals that the client should remove all DOM storage for the origin of the
    /// response URL. This includes <c>localStorage</c> and <c>sessionStorage</c> (cleared via
    /// the corresponding <c>clear()</c> calls), IndexedDB (each database is deleted),
    /// service worker registrations (each is unregistered), Web SQL databases (deprecated),
    /// FileSystem API data, and plugin data.
    /// </summary>
    /// <returns>The <see cref="ClearSiteDataBuilder"/> for method chaining</returns>
    public ClearSiteDataBuilder Storage()
    {
        _directives.Add("\"storage\"");
        return this;
    }

    /// <summary>
    /// The server signals that the client should reload all browsing contexts for the origin
    /// of the response (equivalent to calling <c>Location.reload</c> in each one).
    /// </summary>
    /// <returns>The <see cref="ClearSiteDataBuilder"/> for method chaining</returns>
    public ClearSiteDataBuilder ExecutionContexts()
    {
        _directives.Add("\"executionContexts\"");
        return this;
    }

    /// <summary>
    /// The server signals that the client should clear all types of data for the origin of the
    /// response. If new data types are added to the <c>Clear-Site-Data</c> spec in future,
    /// they will also be covered by this directive. Emitted as the wildcard <c>"*"</c>;
    /// supersedes any other directives configured on this builder.
    /// </summary>
    /// <returns>The <see cref="ClearSiteDataBuilder"/> for method chaining</returns>
    public ClearSiteDataBuilder All()
    {
        _all = true;
        return this;
    }

    /// <summary>
    /// Add a custom directive to the <c>Clear-Site-Data</c> header. Useful for opting in to
    /// directives that this library does not yet model directly — for example experimental
    /// or newly-standardised tokens such as <c>prefetchCache</c>, <c>prerenderCache</c>, or
    /// <c>clientHints</c>. The directive name is emitted as provided; ensure you include
    /// surrounding quotes if required by the directive.
    /// </summary>
    /// <param name="directiveName">The directive name to add (with surrounding quotes if required), e.g. <c>"prefetchCache"</c>.</param>
    /// <returns>The <see cref="ClearSiteDataBuilder"/> for method chaining</returns>
    /// <exception cref="ArgumentException"><paramref name="directiveName"/> is null, empty, or whitespace.</exception>
    public ClearSiteDataBuilder CustomDirective(string directiveName)
    {
        if (string.IsNullOrWhiteSpace(directiveName))
        {
            throw new ArgumentException("Directive name must not be null, empty, or whitespace.", nameof(directiveName));
        }

        _directives.Add(directiveName);
        return this;
    }

    /// <summary>
    /// Build the <c>Clear-Site-Data</c> header value from the configured directives.
    /// </summary>
    /// <returns>The header value as a comma-separated list of quoted tokens.</returns>
    /// <exception cref="InvalidOperationException">No directives have been configured.</exception>
    internal string Build()
    {
        if (_all)
        {
            return "\"*\"";
        }

        if (_directives.Count == 0)
        {
            throw new InvalidOperationException(
                "Clear-Site-Data requires at least one directive. " +
                "Call a method on the builder to specify the data to clear.");
        }

        return string.Join(", ", _directives);
    }
}
