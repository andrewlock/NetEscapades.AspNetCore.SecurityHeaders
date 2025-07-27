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
    public class HashTagHelperTests
    {
        const string shortSnippet = @"var msg = document.getElementById('message');";
        const string scriptSnippet = @"
    var msg = document.getElementById('message');
    msg.innerText = ""My JavaScript is running!"";
";
        
        const string styleSnippet = @"    #message {
        color: red;
    }  ";

        [Fact]
        public async Task ProcessAsync_Script_GeneratesExpectedOutput()
        {
            // Arrange
            var id = Guid.NewGuid().ToString();
            var tagName = "script";
            var tagHelperContext = GetTagHelperContext(id, tagName);
            var tagHelper = new HashTagHelper()
            {
                CSPHashType = CSPHashType.SHA256,
                ViewContext = GetViewContext(),
            };

            var output = new TagHelperOutput(
               tagName,
               attributes: new TagHelperAttributeList(),
               getChildContentAsync: (useCachedResult, encoder) =>
               {
                   var tagHelperContent = new DefaultTagHelperContent();
                   tagHelperContent.SetHtmlContent(scriptSnippet);
                   return Task.FromResult<TagHelperContent>(tagHelperContent);
               });

            output.Content.SetHtmlContent(shortSnippet);

            // Act
            await tagHelper.ProcessAsync(tagHelperContext, output);

            // Assert
            output.TagName.Should().Be(tagName);
            output.Attributes.Should().BeEmpty();
            output.Content.GetContent().Should().Be(shortSnippet);
        }

        [Fact]
        public async Task ProcessAsync_Script_AddsHashToHttpContextForOneLineSnippets()
        {
            // Arrange
            var id = Guid.NewGuid().ToString();
            var tagName = "script";
            var tagHelperContext = GetTagHelperContext(id, tagName);
            var tagHelper = new HashTagHelper()
            {
                CSPHashType = CSPHashType.SHA256,
                ViewContext = GetViewContext(),
            };

            var output = new TagHelperOutput(
               tagName,
               attributes: new TagHelperAttributeList(),
               getChildContentAsync: (useCachedResult, encoder) =>
               {
                   var tagHelperContent = new DefaultTagHelperContent();
                   tagHelperContent.SetHtmlContent(shortSnippet);
                   return Task.FromResult<TagHelperContent>(tagHelperContent);
               });

            // Act
            await tagHelper.ProcessAsync(tagHelperContext, output);

            // Assert
            var expected = "'sha256-1yLcYHZUiV92moZ6snTrg6e0yBO8emEEpUSB2wlMFz8='";
            tagHelper.ViewContext.HttpContext.GetScriptCSPHashes().Should().ContainSingle()
                .Which.Should().Be(expected);
        }

        [Fact]
        public async Task ProcessAsync_Script_AddsHashToHttpContextForMultiLineSnippets()
        {
            // Arrange
            var id = Guid.NewGuid().ToString();
            var tagName = "script";
            var tagHelperContext = GetTagHelperContext(id, tagName);
            var tagHelper = new HashTagHelper()
            {
                CSPHashType = CSPHashType.SHA256,
                ViewContext = GetViewContext(),
            };

            var output = new TagHelperOutput(
               tagName,
               attributes: new TagHelperAttributeList(),
               getChildContentAsync: (useCachedResult, encoder) =>
               {
                   var tagHelperContent = new DefaultTagHelperContent();
                   tagHelperContent.SetHtmlContent(scriptSnippet);
                   return Task.FromResult<TagHelperContent>(tagHelperContent);
               });

            // Act
            await tagHelper.ProcessAsync(tagHelperContext, output);

            // Assert
            var expected = "'sha256-Oro8tit8euyKzxqyJteBRTdQBlipvXGQWfS5epMHmUU='";
            tagHelper.ViewContext.HttpContext.GetScriptCSPHashes().Should().ContainSingle()
                .Which.Should().Be(expected);
        }

        [Fact]
        public async Task ProcessAsync_Style_GeneratesExpectedOutput()
        {
            // Arrange
            var id = Guid.NewGuid().ToString();
            var tagName = "style";
            var tagHelperContext = GetTagHelperContext(id, tagName);
            var tagHelper = new HashTagHelper()
            {
                CSPHashType = CSPHashType.SHA256,
                ViewContext = GetViewContext(),
            };

            var output = new TagHelperOutput(
               tagName,
               attributes: new TagHelperAttributeList(),
               getChildContentAsync: (useCachedResult, encoder) =>
               {
                   var tagHelperContent = new DefaultTagHelperContent();
                   tagHelperContent.SetHtmlContent(scriptSnippet);
                   return Task.FromResult<TagHelperContent>(tagHelperContent);
               });

            output.Content.SetHtmlContent(styleSnippet);

            // Act
            await tagHelper.ProcessAsync(tagHelperContext, output);

            // Assert
            output.TagName.Should().Be(tagName);
            output.Attributes.Should().BeEmpty();
            output.Content.GetContent().Should().Be(styleSnippet);
        }

        [Fact]
        public async Task ProcessAsync_Style_AddsHashToHttpContextForOneLineSnippets()
        {
            // Arrange
            var id = Guid.NewGuid().ToString();
            var tagName = "style";
            var tagHelperContext = GetTagHelperContext(id, tagName);
            var tagHelper = new HashTagHelper()
            {
                CSPHashType = CSPHashType.SHA256,
                ViewContext = GetViewContext(),
            };

            var output = new TagHelperOutput(
               tagName,
               attributes: new TagHelperAttributeList(),
               getChildContentAsync: (useCachedResult, encoder) =>
               {
                   var tagHelperContent = new DefaultTagHelperContent();
                   tagHelperContent.SetHtmlContent(styleSnippet);
                   return Task.FromResult<TagHelperContent>(tagHelperContent);
               });

            // Act
            await tagHelper.ProcessAsync(tagHelperContext, output);

            // Assert
            var expected = "'sha256-Wz9o8J/ijdXtAzs95rmQ8OtBacYk6JfYTXQlM8yxIjg='";
            tagHelper.ViewContext.HttpContext.GetStyleCSPHashes().Should().ContainSingle()
                .Which.Should().Be(expected);
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
