using Microsoft.AspNetCore.Builder;
using System;
using FluentAssertions;

namespace NetEscapades.AspNetCore.SecurityHeaders.Test;
public class CustomHeaderExtensionsTests
{
    [Test]
    public void AddCustomHeader_WhenNullHeader_IncludesHeaderInTheMessage()
    {
        // https://github.com/andrewlock/NetEscapades.AspNetCore.SecurityHeaders/issues/35
        var collection = new HeaderPolicyCollection();
        var add = () => collection.AddCustomHeader(null !, "asdf");
        add.Should().Throw<ArgumentNullException>();
    }

    [Test]
    public void AddCustomHeader_WhenNullValue_DoesntThrow()
    {
        var collection = new HeaderPolicyCollection();
        collection.AddCustomHeader("header", "value");
    }
}