using System;
using System.Linq;
using System.Text;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Razor.TagHelpers;
using NetEscapades.AspNetCore.SecurityHeaders.Infrastructure;

namespace NetEscapades.AspNetCore.SecurityHeaders.TagHelpers;

/// <summary>
/// Generates an hash of style, or inline script attributes
/// </summary>
[HtmlTargetElement("*", Attributes = AttributePrefix + "*")]
public class AttributeHashTagHelper : TagHelper
{
    private const string AttributePrefix = "asp-add-";
    private const string AttributeSuffix = "-to-csp";

    private const CSPHashType DefaultHashType = CSPHashType.SHA256;

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

        var cspAttributes = context
            .AllAttributes
            .Where(a =>
                a.Name.StartsWith(AttributePrefix)
                && a.Name.EndsWith(AttributeSuffix)
                && a.Name != HashTagHelper.AttributeName)
            .ToList();

        foreach (var cspAttribute in cspAttributes)
        {
            ProcessAttribute(ViewContext, context, output, cspAttribute);
        }
    }

    private void ProcessAttribute(
        ViewContext viewContext,
        TagHelperContext context,
        TagHelperOutput output,
        TagHelperAttribute cspAttribute)
    {
        var targetAttributeName = ParseTargetAttributeName(cspAttribute.Name);
        var cspHashType = cspAttribute.ValueStyle != HtmlAttributeValueStyle.Minimized
            ? ParseHashType(cspAttribute.Value.ToString())
            : DefaultHashType;

        var sha = CryptographyAlgorithms.Create(cspHashType);

        var targetAttributeValue = context.AllAttributes[targetAttributeName];
        if (targetAttributeValue is not null)
        {
            var content = targetAttributeValue.Value.ToString();

            // the hash is calculated based on unix line endings, not windows endings, so account for that
            var unixContent = content!.Replace("\r\n", "\n");

            var contentBytes = Encoding.UTF8.GetBytes(unixContent);
            var hashedBytes = sha.ComputeHash(contentBytes);
            var hash = Convert.ToBase64String(hashedBytes);

            if (targetAttributeName == "style")
            {
                viewContext.HttpContext.SetStylesCSPHash(cspHashType, hash);
            }
            else
            {
                viewContext.HttpContext.SetScriptCSPHash(cspHashType, hash);
            }
        }

        output.Attributes.RemoveAll(cspAttribute.Name);
    }

    private string ParseTargetAttributeName(string cspAttributeName)
        => cspAttributeName
            .Replace(AttributePrefix, string.Empty)
            .Replace(AttributeSuffix, string.Empty);

    private CSPHashType ParseHashType(string? hashTypeText)
        => Enum.TryParse<CSPHashType>(hashTypeText, out var hashType)
            ? hashType
            : DefaultHashType;
}
