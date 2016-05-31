using System;
using Xunit;

namespace NetEscapades.AspNetCore.SecurityHeaders.Infrastructure
{
    public class SecurityHeadersPolicyTests
    {
        [Fact]
        public void Default_Constructor()
        {
            // Arrange & Act
            var result = new SecurityHeadersResult();

            Assert.Empty(result.SetHeaders);
            Assert.Empty(result.RemoveHeaders);
        }
    }
}