using System;
using Xunit;

namespace NetEscapades.AspNetCore.SecurityHeaders.Infrastructure
{
    public class SecurityHeadersResultTests
    {
        [Fact]
        public void Default_Constructor()
        {
            // Arrange & Act
            var result = new SecurityHeadersPolicy();

            Assert.Null(result.XFramesOptions);
            Assert.Null(result.XssProtection);
            Assert.Null(result.XContentTypeOptions);
            Assert.Null(result.StrictTransportSecurity);
        }
    }
}