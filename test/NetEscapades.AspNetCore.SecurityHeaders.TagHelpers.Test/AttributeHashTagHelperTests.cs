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

using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

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
            var cspAttribute = new TagHelperAttribute("asp-add-style-attribute-to-csp");
            var tagHelperContext = GetTagHelperContext(id, tagName, new([styleAttribute, cspAttribute]));
            var tagHelper = new HashTagHelper()
            {
                CSPHashType = CSPHashType.SHA256,
                ViewContext = GetViewContext(),
            };

            var output = new TagHelperOutput(
               tagName,
               attributes: new([styleAttribute, cspAttribute]),
               getChildContentAsync: (useCachedResult, encoder) =>
               {
                   var tagHelperContent = new DefaultTagHelperContent();
                   //tagHelperContent.SetHtmlContent(scriptSnippet);
                   return Task.FromResult<TagHelperContent>(tagHelperContent);
               });

            //output.Content.SetHtmlContent(styleSnippet);

            // Act
            await tagHelper.ProcessAsync(tagHelperContext, output);

            // Assert
            Assert.Equal(tagName, output.TagName);
            Assert.Empty(output.Attributes);
            Assert.Empty(output.Content.GetContent());
        }

        [Fact]
        public async Task ProcessAsync_StyleAttribute_AddsHashToHttpContext()
        {
            // Arrange
            var id = Guid.NewGuid().ToString();
            var tagName = "style";
            var styleAttribute = new TagHelperAttribute("style", inlineStyleSnippet);
            var cspAttribute = new TagHelperAttribute("asp-add-style-attribute-to-csp");
            var tagHelperContext = GetTagHelperContext(id, tagName, new([styleAttribute, cspAttribute]));
            var tagHelper = new HashTagHelper()
            {
                CSPHashType = CSPHashType.SHA256,
                ViewContext = GetViewContext(),
            };

            var output = new TagHelperOutput(
               tagName,
               attributes: new([styleAttribute, cspAttribute]),
               getChildContentAsync: (useCachedResult, encoder) =>
               {
                   var tagHelperContent = new DefaultTagHelperContent();
                   //tagHelperContent.SetHtmlContent(styleSnippet);
                   return Task.FromResult<TagHelperContent>(tagHelperContent);
               });

            // Act
            await tagHelper.ProcessAsync(tagHelperContext, output);

            // Assert
            var hash = Assert.Single(tagHelper.ViewContext.HttpContext.GetStyleCSPHashes());
            var expected = "'sha256-Wz9o8J/ijdXtAzs95rmQ8OtBacYk6JfYTXQlM8yxIjg='";
            Assert.Equal(expected, hash);
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
    }
}
