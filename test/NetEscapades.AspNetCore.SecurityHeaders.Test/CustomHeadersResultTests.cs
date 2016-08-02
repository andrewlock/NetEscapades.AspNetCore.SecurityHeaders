using Xunit;

namespace NetEscapades.AspNetCore.SecurityHeaders.Infrastructure
{
    public class CustomHeadersResultTests
    {
        [Fact]
        public void Default_Constructor()
        {
            // Arrange & Act
            var result = new CustomHeadersResult();

            Assert.Empty(result.SetHeaders);
            Assert.Empty(result.RemoveHeaders);
        }
    }
}