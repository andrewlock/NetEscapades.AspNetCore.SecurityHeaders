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
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Moq;

namespace NetEscapades.AspNetCore.SecurityHeaders.TagHelpers.Test;
public class NonceTagHelperTests
{
    [Test]
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
        var output = new TagHelperOutput(tagName, attributes: new TagHelperAttributeList(), getChildContentAsync: (_, _) =>
        {
            var tagHelperContent = new DefaultTagHelperContent();
            var content = tagHelperContent.SetContent("Something");
            return Task.FromResult(content);
        });
        output.Content.SetContent("Something Else");
        // Act
        await nonceTagHelper.ProcessAsync(tagHelperContext, output);
        // Assert
        output.TagName.Should().Be(tagName);
        var attribute = output.Attributes.Should().ContainSingle().Subject;
        attribute.Name.Should().Be("nonce");
        attribute.Value.Should().Be(nonceValue);
        output.Content.GetContent().Should().Be("Something Else");
    }

    [Test]
    public async Task ProcessAsync_WhenNoNonceDoesNotAdd()
    {
        // Arrange
        var id = Guid.NewGuid().ToString();
        var tagName = "script";
        var tagHelperContext = GetTagHelperContext(id, tagName);
        var nonceValue = string.Empty;
        var nonceTagHelper = new NonceTagHelper
        {
            AddNonce = true,
            ViewContext = GetViewContext(nonceValue),
        };
        var output = new TagHelperOutput(tagName, attributes: new TagHelperAttributeList(), getChildContentAsync: (_, _) =>
        {
            var tagHelperContent = new DefaultTagHelperContent();
            var content = tagHelperContent.SetContent("Something");
            return Task.FromResult(content);
        });
        output.Content.SetContent("Something Else");
        // Act
        await nonceTagHelper.ProcessAsync(tagHelperContext, output);
        // Assert
        output.TagName.Should().Be(tagName);
        output.Attributes.Should().BeEmpty();
        output.Content.GetContent().Should().Be("Something Else");
    }

    private static ViewContext GetViewContext(string nonce)
    {
        var actionContext = new ActionContext(new DefaultHttpContext(), new RouteData(), new ActionDescriptor());
        actionContext.HttpContext.Items[Infrastructure.Constants.DefaultNonceKey] = nonce;
        actionContext.HttpContext.RequestServices = new ServiceCollection().AddTransient(_ => Mock.Of<ILogger<NonceTagHelper>>()).BuildServiceProvider();
        return new ViewContext(actionContext, Mock.Of<IView>(), new ViewDataDictionary(new EmptyModelMetadataProvider(), new ModelStateDictionary()), Mock.Of<ITempDataDictionary>(), TextWriter.Null, new HtmlHelperOptions());
    }

    private static TagHelperContext GetTagHelperContext(string id = "testid", string tagName = "script")
    {
        return new TagHelperContext(tagName: tagName, allAttributes: new TagHelperAttributeList(), items: new Dictionary<object, object>(), uniqueId: id);
    }
}