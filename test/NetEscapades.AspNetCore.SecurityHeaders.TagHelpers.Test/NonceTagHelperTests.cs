using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Abstractions;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewEngines;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Razor.TagHelpers;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Moq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Xunit;

namespace NetEscapades.AspNetCore.SecurityHeaders.TagHelpers.Test
{
    public class NonceTagHelperTests
    {
        [Fact]
        public async Task ProcessAsync_GeneratesExpectedOutput()
        {
            // Arrange
            var id = Guid.NewGuid().ToString();
            var tagName = "script";
            var tagHelperContext = GetTagHelperContext(id, tagName);
            var nonceValue = "FHDK1293843673GFNGB2";
            var nonceTagHelper = new NonceTagHelper()
            {
                AddNonce = true,
                ViewContext = GetViewContext(nonceValue),
            };

            var output = new TagHelperOutput(
               tagName,
               attributes: new TagHelperAttributeList(),
               getChildContentAsync: (useCachedResult, encoder) =>
               {
                   var tagHelperContent = new DefaultTagHelperContent();
                   tagHelperContent.SetContent("Something");
                   return Task.FromResult<TagHelperContent>(tagHelperContent);
               });
            output.Content.SetContent("Something Else");

            // Act
            await nonceTagHelper.ProcessAsync(tagHelperContext, output);

            // Assert
            Assert.Equal(tagName, output.TagName);
            Assert.Single(output.Attributes);
            var attribute = Assert.Single(output.Attributes, attr => attr.Name.Equals("nonce"));
            Assert.Equal(nonceValue, attribute.Value);
            Assert.Equal("Something Else", output.Content.GetContent());
        }

        [Fact]
        public async Task ProcessAsync_WhenNoNonceDoesNotAdd()
        {
            // Arrange
            var id = Guid.NewGuid().ToString();
            var tagName = "script";
            var tagHelperContext = GetTagHelperContext(id, tagName);
            string nonceValue = null;
            var nonceTagHelper = new NonceTagHelper()
            {
                AddNonce = true,
                ViewContext = GetViewContext(nonceValue),
            };

            var output = new TagHelperOutput(
               tagName,
               attributes: new TagHelperAttributeList(),
               getChildContentAsync: (useCachedResult, encoder) =>
               {
                   var tagHelperContent = new DefaultTagHelperContent();
                   tagHelperContent.SetContent("Something");
                   return Task.FromResult<TagHelperContent>(tagHelperContent);
               });
            output.Content.SetContent("Something Else");

            // Act
            await nonceTagHelper.ProcessAsync(tagHelperContext, output);

            // Assert
            Assert.Equal(tagName, output.TagName);
            Assert.Empty(output.Attributes);
            Assert.Equal("Something Else", output.Content.GetContent());
        }

        private static ViewContext GetViewContext(string nonce)
        {
            var actionContext = new ActionContext(new DefaultHttpContext(), new RouteData(), new ActionDescriptor());
            if (!string.IsNullOrEmpty(nonce))
            {
                actionContext.HttpContext.SetNonce(nonce);
            }

            actionContext.HttpContext.RequestServices = new ServiceCollection()
                .AddTransient(provider => Mock.Of<ILogger<NonceTagHelper>>())
                .BuildServiceProvider();

            return new ViewContext(actionContext,
                                   Mock.Of<IView>(),
                                   new ViewDataDictionary(new EmptyModelMetadataProvider(), new ModelStateDictionary()),
                                   Mock.Of<ITempDataDictionary>(),
                                   TextWriter.Null,
                                   new HtmlHelperOptions());
        }

        private static TagHelperContext GetTagHelperContext(string id = "testid", string tagName = "script")
        {
            return new TagHelperContext(
                tagName: tagName,
                allAttributes: new TagHelperAttributeList(),
                items: new Dictionary<object, object>(),
                uniqueId: id);
        }
    }
}
