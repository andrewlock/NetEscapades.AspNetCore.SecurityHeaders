using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Http;

namespace NetEscapades.AspNetCore.SecurityHeaders.Headers;

/// <summary>
/// A collection of source builders for a given directive
/// </summary>
internal class SourceBuilderCollection
{
    private HashSet<string>? _keys;
    private List<Func<HttpContext, string>>? _sourceBuilders;

    /// <summary>
    /// Adds a source builder to the collection for the given directive
    /// Calls to <see cref="Add"/> are idempotent for a given <paramref name="key"/>
    /// if the builder with the provided key has already been added, it will not be added again.
    /// </summary>
    /// <param name="builder">The builder to add</param>
    /// <param name="key">The key for the builder used to determine uniqueness</param>
    public void Add(Func<HttpContext, string> builder, string key)
    {
        _keys ??= [];
        if (_keys.Add(key))
        {
            _sourceBuilders ??= [];
            _sourceBuilders.Add(builder);
        }
    }

    /// <summary>
    /// Creates a copy of the list of builders
    /// </summary>
    /// <returns>A copy of the list of builders</returns>
    public List<Func<HttpContext, string>> ToList() => _sourceBuilders?.ToList() ?? [];

    /// <summary>
    /// Have any builders been added?
    /// </summary>
    /// <returns>True if any builders have been added</returns>
    public bool Any() => _sourceBuilders?.Count > 0;
}