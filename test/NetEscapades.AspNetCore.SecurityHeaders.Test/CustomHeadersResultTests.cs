using FluentAssertions;

namespace NetEscapades.AspNetCore.SecurityHeaders.Infrastructure;
public class CustomHeadersResultTests
{
    [Test]
    public void Default_Constructor()
    {
        // Arrange & Act
        var result = new CustomHeadersResult();
        result.SetHeaders.Should().BeEmpty();
        result.RemoveHeaders.Should().BeEmpty();
    }
}