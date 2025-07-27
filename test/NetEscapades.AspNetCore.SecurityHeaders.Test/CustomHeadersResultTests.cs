using FluentAssertions;
using Xunit;

namespace NetEscapades.AspNetCore.SecurityHeaders.Infrastructure;

public class CustomHeadersResultTests
{
    [Fact]
    public void Default_Constructor()
    {
        // Arrange & Act
        var result = new CustomHeadersResult();

        result.SetHeaders.Should().BeEmpty();
        result.RemoveHeaders.Should().BeEmpty();
    }
}