using System.Collections;
using System.Collections.Generic;

namespace NetEscapades.AspNetCore.SecurityHeaders.Headers;

/// <summary>
/// A collection of sources for a given directive
/// </summary>
public class SourceCollection : IEnumerable<string>
{
    private readonly List<string> _sources = [];

    /// <summary>
    /// Gets the number of elements in the source collection
    /// </summary>
    public int Count => _sources.Count;

    /// <summary>
    /// Adds a source to the collection for the given directive.
    /// Calls to <see cref="Add"/> are idempotent;
    /// if the source has already been added, it will not be added again.
    /// </summary>
    /// <param name="source">The source to add</param>
    public void Add(string source)
    {
        if (!_sources.Contains(source))
        {
            _sources.Add(source);
        }
    }

    /// <summary>
    /// Removes a source from the collection for the given directive.
    /// Returns true if the source was removed. Returns false if the
    /// source was not found in the collection.
    /// </summary>
    /// <param name="source">The source to remove</param>
    /// <returns>True if the source was found and removed, false if it was not found.</returns>
    public bool Remove(string source) => _sources.Remove(source);

    /// <inheritdoc/>
    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

    /// <inheritdoc/>
    public IEnumerator<string> GetEnumerator() => _sources.GetEnumerator();
}