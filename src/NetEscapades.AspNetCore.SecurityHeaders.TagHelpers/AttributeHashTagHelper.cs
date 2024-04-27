using System;
using System.Text;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Razor.TagHelpers;
using NetEscapades.AspNetCore.SecurityHeaders.Infrastructure;

namespace NetEscapades.AspNetCore.SecurityHeaders.TagHelpers;

/// <summary>
/// Generates an hash of style, or inline script attributes
/// </summary>
[HtmlTargetElement("*", Attributes = AttributeName)]
public class AttributeHashTagHelper : TagHelper
{
    private const string AttributeName = "asp-add-style-attribute-to-csp";
    private const string CspHashTypeAttributeName = "csp-hash-type";

    /// <summary>
    /// Add a <code>csp-hash-type</code> attribute to the element
    /// </summary>
    [HtmlAttributeName(CspHashTypeAttributeName)]
    public CSPHashType CSPHashType { get; set; } = CSPHashType.SHA256;

    /// <summary>
    /// Provides access to the <see cref="ViewContext"/>
    /// </summary>
    [ViewContext]
    public ViewContext? ViewContext { get; set; }

    /// <inheritdoc />
    public override void Process(TagHelperContext context, TagHelperOutput output)
    {
        if (ViewContext is null)
        {
            throw new InvalidOperationException("ViewContext was null");
        }

        using var sha = CryptographyAlgorithms.Create(CSPHashType);

        var styleAttributeValue = context.AllAttributes["style"];

        // TODO: properly handle Value as object (just ToString?)
        // properly handle null results from ToString()
        var content = styleAttributeValue.Value.ToString();

        var contentBytes = Encoding.UTF8.GetBytes(content!);
        var hashedBytes = sha.ComputeHash(contentBytes);
        var hash = Convert.ToBase64String(hashedBytes);

        ViewContext.HttpContext.SetStylesCSPHash(CSPHashType, hash);

        output.Attributes.RemoveAll(AttributeName);
        output.Attributes.RemoveAll(CspHashTypeAttributeName);
    }
}
