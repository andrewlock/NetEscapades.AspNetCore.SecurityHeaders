using System.Collections.Generic;
using FluentAssertions;
using NetEscapades.AspNetCore.SecurityHeaders.Headers;
using Xunit;

namespace NetEscapades.AspNetCore.SecurityHeaders.Test;

public class SourceCollectionTests
{
    [Fact]
    public void AddsSourceInExpectedOrder()
    {
        string[] expected = ["Source 1", "Source 2", "Source 3"]; 
        var col = new SourceCollection();
        foreach (var source in expected)
        {
            col.Add(source);
        }

        VerifyAsExpected(col, expected);
    }

    [Fact]
    public void DuplicateSourcesAreNotAdded()
    {
        var col = new SourceCollection();
        col.Add("Source 1");
        col.Add("Source 1");
        col.Add("Source 2");
        col.Add("Source 3");
        col.Add("Source 3");

        VerifyAsExpected(col, ["Source 1", "Source 2", "Source 3"]);
    }

    [Fact]
    public void AddRageAddsSourceInExpectedOrder()
    {
        List<string> sources = ["Source 1", "Source 2", "Source 3"]; 
        var col = new SourceCollection();
        col.AddRange(sources);

        VerifyAsExpected(col, sources);
    }

    [Fact]
    public void AddRangeDuplicateSourcesAreNotAdded()
    {
        string[] expected = ["Source 1", "Source 2", "Source 3"]; 
        var col = new SourceCollection();
        col.AddRange([
            "Source 1",
            "Source 1",
            "Source 2",
            "Source 3",
            "Source 3",
        ]);

        VerifyAsExpected(col, expected);
    }

    [Fact]
    public void RemoveRemovesSources()
    {
        string[] expected = ["Source 1", "Source 2", "Source 3"]; 
        var col = new SourceCollection();
        col.Add("To move");
        foreach (var source in expected)
        {
            col.Add(source);
        }

        col.Remove("To move");

        VerifyAsExpected(col, expected);
    }

    private static void VerifyAsExpected(SourceCollection collection, IList<string> expected)
    {
        var i = 0;
        foreach (var source in collection)
        {
            source.Should().Be(expected[i]);
            i++;
        }
    }
}