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
    private const string AttributeName = "asp-add-attribute-to-csp";
    private const string CspHashTypeAttributeName = "csp-hash-type";

    /// <summary>
    /// Add a <code>asp-add-attribute-to-csp</code> attribute to the element
    /// </summary>
    [HtmlAttributeName(AttributeName)]
    public string TargetAttributeName { get; set; } = default!;

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

        var targetAttributeValue = context.AllAttributes[TargetAttributeName];

        // TODO: properly handle Value as object (just ToString?)
        // properly handle null results from ToString()
        var content = targetAttributeValue.Value.ToString();

        // the hash is calculated based on unix line endings, not windows endings, so account for that
        var unixContent = content!.Replace("\r\n", "\n");

        var contentBytes = Encoding.UTF8.GetBytes(unixContent);
        var hashedBytes = sha.ComputeHash(contentBytes);
        var hash = Convert.ToBase64String(hashedBytes);

        if (TargetAttributeName == "style")
        {
            ViewContext.HttpContext.SetStylesCSPHash(CSPHashType, hash);
        }
        else
        {
            ViewContext.HttpContext.SetScriptCSPHash(CSPHashType, hash);
        }

        output.Attributes.RemoveAll(AttributeName);
        output.Attributes.RemoveAll(CspHashTypeAttributeName);
    }
}
