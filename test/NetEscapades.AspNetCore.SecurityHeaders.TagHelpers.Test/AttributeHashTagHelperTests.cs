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
        const string inlineMultiLineStyleSnippet = @"
color: red;
background: blue;
";

        const string inlineScriptSnippet = "myScript()";

        [Fact]
        public async Task ProcessAsync_StyleAttribute_GeneratesExpectedOutput()
        {
            // Arrange
            var id = Guid.NewGuid().ToString();
            var tagName = "div";
            var styleAttribute = new TagHelperAttribute("style", inlineStyleSnippet);
            var cspAttribute = new TagHelperAttribute("asp-add-csp-for-style");
            var fixture = CreateFixture(id, tagName, styleAttribute, cspAttribute);
            var tagHelper = new AttributeHashTagHelper()
            {
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
            var cspAttribute = new TagHelperAttribute("asp-add-csp-for-style");
            var fixture = CreateFixture(id, tagName, styleAttribute, cspAttribute);
            var tagHelper = new AttributeHashTagHelper()
            {
                ViewContext = GetViewContext(),
            };

            // Act
            await tagHelper.ProcessAsync(fixture.Context, fixture.Output);

            // Assert
            var hash = Assert.Single(tagHelper.ViewContext.HttpContext.GetStyleCSPHashes());
            var expected = "'sha256-NerDAUWfwD31YdZHveMrq0GLjsNFMwxLpZl0dPUeCcw='";
            Assert.Equal(expected, hash);
        }

        [Fact]
        public async Task ProcessAsync_StyleAttributeWithExplicitHashType_AddsHashToHttpContext()
        {
            // Arrange
            var id = Guid.NewGuid().ToString();
            var tagName = "div";
            var styleAttribute = new TagHelperAttribute("style", inlineStyleSnippet);
            var cspAttribute = new TagHelperAttribute("asp-add-csp-for-style", "SHA384");
            var fixture = CreateFixture(id, tagName, styleAttribute, cspAttribute);
            var tagHelper = new AttributeHashTagHelper()
            {
                ViewContext = GetViewContext(),
            };

            // Act
            await tagHelper.ProcessAsync(fixture.Context, fixture.Output);

            // Assert
            var hash = Assert.Single(tagHelper.ViewContext.HttpContext.GetStyleCSPHashes());
            var expected = "'sha384-YoSV9pxydVBLyyDpluNe9tQWgtUWlnzHS/zCvuNc30tEu0YwLQPRgNAXk+h06DXU'";
            Assert.Equal(expected, hash);
        }

        [Fact]
        public async Task ProcessAsync_StyleAttributeWithMultiLine_AddsHashToHttpContext()
        {
            // Arrange
            var id = Guid.NewGuid().ToString();
            var tagName = "div";
            var styleAttribute = new TagHelperAttribute("style", inlineMultiLineStyleSnippet);
            var cspAttribute = new TagHelperAttribute("asp-add-csp-for-style");
            var fixture = CreateFixture(id, tagName, styleAttribute, cspAttribute);
            var tagHelper = new AttributeHashTagHelper()
            {
                ViewContext = GetViewContext(),
            };

            // Act
            await tagHelper.ProcessAsync(fixture.Context, fixture.Output);

            // Assert
            var hash = Assert.Single(tagHelper.ViewContext.HttpContext.GetStyleCSPHashes());
            var expected = "'sha256-ly/Q8sGjROqYelSQCwIsD00L09JdMcVcMFTDyK7N7GM='";
            Assert.Equal(expected, hash);
        }

        [Fact]
        public async Task ProcessAsync_StyleAttributeTargetingNonExistingAttribute_DoesntAddAndCleansUp()
        {
            // Arrange
            var id = Guid.NewGuid().ToString();
            var tagName = "div";
            var cspAttribute = new TagHelperAttribute("asp-add-csp-for-style");
            var fixture = CreateFixture(id, tagName, cspAttribute);
            var tagHelper = new AttributeHashTagHelper()
            {
                ViewContext = GetViewContext(),
            };

            // Act
            await tagHelper.ProcessAsync(fixture.Context, fixture.Output);

            // Assert
            Assert.Equal(tagName, fixture.Output.TagName);
            Assert.Empty(fixture.Output.Attributes);
            Assert.Empty(fixture.Output.Content.GetContent());
        }

        [Fact]
        public async Task ProcessAsync_InlineScriptAttribute_GeneratesExpectedOutput()
        {
            // Arrange
            var id = Guid.NewGuid().ToString();
            var tagName = "div";
            var inlineScriptAttribute = new TagHelperAttribute("onclick", inlineScriptSnippet);
            var cspAttribute = new TagHelperAttribute("asp-add-csp-for-onclick");
            var fixture = CreateFixture(id, tagName, inlineScriptAttribute, cspAttribute);
            var tagHelper = new AttributeHashTagHelper()
            {
                ViewContext = GetViewContext(),
            };

            // Act
            await tagHelper.ProcessAsync(fixture.Context, fixture.Output);

            // Assert
            Assert.Equal(tagName, fixture.Output.TagName);
            Assert.Equal([inlineScriptAttribute], fixture.Output.Attributes);
            Assert.Empty(fixture.Output.Content.GetContent());
        }

        [Fact]
        public async Task ProcessAsync_InlineScriptAttribute_AddsHashToHttpContext()
        {
            // Arrange
            var id = Guid.NewGuid().ToString();
            var tagName = "div";
            var inlineScriptAttribute = new TagHelperAttribute("onclick", inlineScriptSnippet);
            var cspAttribute = new TagHelperAttribute("asp-add-csp-for-onclick");
            var fixture = CreateFixture(id, tagName, inlineScriptAttribute, cspAttribute);
            var tagHelper = new AttributeHashTagHelper()
            {
                ViewContext = GetViewContext(),
            };

            // Act
            await tagHelper.ProcessAsync(fixture.Context, fixture.Output);

            // Assert
            var hash = Assert.Single(tagHelper.ViewContext.HttpContext.GetScriptCSPHashes());
            var expected = "'sha256-1lzfyKjJuCLGsHTaOB3al0SElf3ats68l7XOAdrWd+E='";
            Assert.Equal(expected, hash);
        }

        [Fact]
        public async Task ProcessAsync_MultipleAttributes_AddsAllHashesToHttpContext()
        {
            // Arrange
            var id = Guid.NewGuid().ToString();
            var tagName = "div";

            var styleAttribute = new TagHelperAttribute("style", inlineStyleSnippet);
            var inlineScriptAttribute = new TagHelperAttribute("onclick", inlineScriptSnippet);
            var cspStyleAttribute = new TagHelperAttribute("asp-add-csp-for-style");
            var cspScriptAttribute = new TagHelperAttribute("asp-add-csp-for-onclick");

            var fixture = CreateFixture(id, tagName,
                styleAttribute,
                inlineScriptAttribute,
                cspStyleAttribute,
                cspScriptAttribute
            );

            var tagHelper = new AttributeHashTagHelper()
            {
                ViewContext = GetViewContext(),
            };

            // Act
            await tagHelper.ProcessAsync(fixture.Context, fixture.Output);

            // Assert
            var styleHash = Assert.Single(tagHelper.ViewContext.HttpContext.GetStyleCSPHashes());
            var scriptHash = Assert.Single(tagHelper.ViewContext.HttpContext.GetScriptCSPHashes());
            var expectedStyleHash = "'sha256-NerDAUWfwD31YdZHveMrq0GLjsNFMwxLpZl0dPUeCcw='";
            var expectedScriptHash = "'sha256-1lzfyKjJuCLGsHTaOB3al0SElf3ats68l7XOAdrWd+E='";

            Assert.Equal(expectedStyleHash, styleHash);
            Assert.Equal(expectedScriptHash, scriptHash);
        }

        private static Fixture CreateFixture(string id, string tagName, params TagHelperAttribute[] attributes)
        {
            return new Fixture
            {
                Context = GetTagHelperContext(id, tagName, new(attributes)),
                Output = GetTagHelperOutput(id, tagName, new(attributes))
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
