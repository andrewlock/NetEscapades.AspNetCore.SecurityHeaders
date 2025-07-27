using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using FluentAssertions;
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
            fixture.Output.TagName.Should().Be(tagName);
            fixture.Output.Attributes.Should().BeEquivalentTo([styleAttribute]);
            fixture.Output.Content.GetContent().Should().BeEmpty();
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
            var expected = "'sha256-NerDAUWfwD31YdZHveMrq0GLjsNFMwxLpZl0dPUeCcw='";
            var hash = tagHelper.ViewContext.HttpContext.GetStyleCSPHashes().Should().ContainSingle()
                .Which.Should().Be(expected);
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
            var expected = "'sha384-YoSV9pxydVBLyyDpluNe9tQWgtUWlnzHS/zCvuNc30tEu0YwLQPRgNAXk+h06DXU'";
            tagHelper.ViewContext.HttpContext.GetStyleCSPHashes().Should().ContainSingle()
                .Which.Should().Be(expected);
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
            var expected = "'sha256-ly/Q8sGjROqYelSQCwIsD00L09JdMcVcMFTDyK7N7GM='";
            tagHelper.ViewContext.HttpContext.GetStyleCSPHashes().Should().ContainSingle()
                .Which.Should().Be(expected);
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
            fixture.Output.TagName.Should().Be(tagName);
            fixture.Output.Attributes.Should().BeEmpty();
            fixture.Output.Content.GetContent().Should().BeEmpty();
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
            fixture.Output.TagName.Should().Be(tagName);
            fixture.Output.Attributes.Should().BeEquivalentTo([inlineScriptAttribute]);
            fixture.Output.Content.GetContent().Should().BeEmpty();
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
            var expected = "'sha256-1lzfyKjJuCLGsHTaOB3al0SElf3ats68l7XOAdrWd+E='";
            tagHelper.ViewContext.HttpContext.GetScriptCSPHashes().Should().ContainSingle()
                .Which.Should().Be(expected);
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
            var expectedStyleHash = "'sha256-NerDAUWfwD31YdZHveMrq0GLjsNFMwxLpZl0dPUeCcw='";
            var expectedScriptHash = "'sha256-1lzfyKjJuCLGsHTaOB3al0SElf3ats68l7XOAdrWd+E='";
            tagHelper.ViewContext.HttpContext.GetStyleCSPHashes().Should().ContainSingle()
                .Which.Should().Be(expectedStyleHash);
            tagHelper.ViewContext.HttpContext.GetScriptCSPHashes().Should().ContainSingle()
                .Which.Should().Be(expectedScriptHash);
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
