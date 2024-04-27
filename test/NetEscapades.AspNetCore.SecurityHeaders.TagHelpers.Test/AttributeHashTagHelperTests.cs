using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Abstractions;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewEngines;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Razor.TagHelpers;
using Microsoft.AspNetCore.Routing;
using Moq;
using NetEscapades.AspNetCore.SecurityHeaders.Infrastructure;
using Xunit;

namespace NetEscapades.AspNetCore.SecurityHeaders.TagHelpers.Test
{
    public class AttributeHashTagHelperTests
    {
        const string inlineStyleSnippet = "color: red";

        [Fact]
        public async Task ProcessAsync_StyleAttribute_GeneratesExpectedOutput()
        {
            // Arrange
            var id = Guid.NewGuid().ToString();
            var tagName = "div";
            var styleAttribute = new TagHelperAttribute("style", inlineStyleSnippet);
            var cspAttribute = new TagHelperAttribute("asp-add-attribute-to-csp", "style");
            var fixture = CreateFixture(id, tagName, new([styleAttribute, cspAttribute]));
            var tagHelper = new AttributeHashTagHelper()
            {
                CSPHashType = CSPHashType.SHA256,
                ViewContext = GetViewContext(),
            };

            // Act
            await tagHelper.ProcessAsync(fixture.Context, fixture.Output);

            // Assert
            Assert.Equal(tagName, fixture.Output.TagName);
            Assert.Equal([styleAttribute], fixture.Output.Attributes);
            Assert.Empty(fixture.Output.Content.GetContent());
        }

        [Fact]
        public async Task ProcessAsync_StyleAttribute_AddsHashToHttpContext()
        {
            // Arrange
            var id = Guid.NewGuid().ToString();
            var tagName = "div";
            var styleAttribute = new TagHelperAttribute("style", inlineStyleSnippet);
            var cspAttribute = new TagHelperAttribute("asp-add-attribute-to-csp", "style");
            var fixture = CreateFixture(id, tagName, new([styleAttribute, cspAttribute]));
            var tagHelper = new AttributeHashTagHelper()
            {
                CSPHashType = CSPHashType.SHA256,
                ViewContext = GetViewContext(),
            };

            // Act
            await tagHelper.ProcessAsync(fixture.Context, fixture.Output);

            // Assert
            var hash = Assert.Single(tagHelper.ViewContext.HttpContext.GetStyleCSPHashes());
            var expected = "'sha256-NerDAUWfwD31YdZHveMrq0GLjsNFMwxLpZl0dPUeCcw='";
            Assert.Equal(expected, hash);
        }

        private static Fixture CreateFixture(string id, string tagName, TagHelperAttributeList attributes)
        {
            return new Fixture
            {
                Context = GetTagHelperContext(id, tagName, attributes),
                Output = GetTagHelperOutput(id, tagName, attributes)
            };
        }

        private static ViewContext GetViewContext()
        {
            var actionContext = new ActionContext(new DefaultHttpContext(), new RouteData(), new ActionDescriptor());
            return new ViewContext(actionContext,
                                   Mock.Of<IView>(),
                                   new ViewDataDictionary(new EmptyModelMetadataProvider(), new ModelStateDictionary()),
                                   Mock.Of<ITempDataDictionary>(),
                                   TextWriter.Null,
                                   new HtmlHelperOptions());
        }

        private static TagHelperContext GetTagHelperContext(string id, string tagName, TagHelperAttributeList attributes)
        {
            return new TagHelperContext(
                tagName: tagName,
                allAttributes: attributes,
                items: new Dictionary<object, object>(),
                uniqueId: id);
        }

        private static TagHelperOutput GetTagHelperOutput(string id, string tagName, TagHelperAttributeList attributes)
        {
            return new TagHelperOutput(
               tagName,
               attributes: attributes,
               getChildContentAsync: (useCachedResult, encoder) =>
               {
                   var tagHelperContent = new DefaultTagHelperContent();
                   return Task.FromResult<TagHelperContent>(tagHelperContent);
               });
        }

        private class Fixture
        {
            public TagHelperContext Context { get; set; } = default!;
            public TagHelperOutput Output { get; set; } = default!;
        }
    }
}
