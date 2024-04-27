using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Razor.TagHelpers;

using NetEscapades.AspNetCore.SecurityHeaders.Infrastructure;

namespace NetEscapades.AspNetCore.SecurityHeaders.TagHelpers;

/// <summary>
/// Generates an hash of style, or inline script attributes
/// </summary>
public class AttributeHashTagHelper : TagHelper
{
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
        base.Process(context, output);
    }
}
