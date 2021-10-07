using Microsoft.AspNetCore.Builder;
using System;
using Xunit;

namespace NetEscapades.AspNetCore.SecurityHeaders.Test
{
    public class CustomHeaderExtensionsTests
    {
        [Fact]
        public void AddCustomHeader_WhenNullHeader_IncludesHeaderInTheMessage()
        {
            // https://github.com/andrewlock/NetEscapades.AspNetCore.SecurityHeaders/issues/35
            var collection = new HeaderPolicyCollection();

            Assert.Throws<ArgumentNullException>(
                "header", () => collection.AddCustomHeader(null!, "asdf"));
        }

        [Fact]
        public void AddCustomHeader_WhenNullValue_DoesntThrow()
        {
            var collection = new HeaderPolicyCollection();
            collection.AddCustomHeader("header", "value");
        }
    }
}
