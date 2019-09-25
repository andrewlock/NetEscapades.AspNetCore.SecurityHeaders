using System;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Razor.TagHelpers;
using NetEscapades.AspNetCore.SecurityHeaders.Infrastructure;

namespace NetEscapades.AspNetCore.SecurityHeaders.TagHelpers
{
    /// <summary>
    /// Generates a Hash of the content of the script
    /// </summary>
    [HtmlTargetElement("script", Attributes = AttributeName)]
    [HtmlTargetElement("style", Attributes = AttributeName)]
    public class HashTagHelper : TagHelper
    {
        private const string AttributeName = "asp-add-content-to-csp";
        private const string CspHashTypeAttributeName = "csp-hash-type";

        /// <summary>
        /// Add a <code>nonce</code> attribute to the element
        /// </summary>
        [HtmlAttributeName(CspHashTypeAttributeName)]
        public CSPHashType CSPHashType { get; set; } = CSPHashType.SHA256;

        /// <summary>
        /// Provides access to the <see cref="ViewContext"/>
        /// </summary>
        [ViewContext]
        public ViewContext? ViewContext { get; set; }

        /// <inheritdoc />
        public override async Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
        {
            if (ViewContext is null)
            {
                throw new InvalidOperationException("ViewContext was null");
            }

            using (var sha = CryptographyAlgorithms.Create(CSPHashType))
            {
                var childContent = await output.GetChildContentAsync();

                // the hash is calculated based on unix line endings, not windows endings, so account for that
                var content = childContent.GetContent().Replace("\r\n", "\n");
                var contentBytes = Encoding.UTF8.GetBytes(content);
                var hashedBytes = sha.ComputeHash(contentBytes);
                var hash = Convert.ToBase64String(hashedBytes);

                output.Attributes.RemoveAll(AttributeName);
                output.Attributes.RemoveAll(CspHashTypeAttributeName);

                if (context.TagName == "script")
                {
                    ViewContext.HttpContext.SetScriptCSPHash(CSPHashType, hash);
                }
                else if (context.TagName == "style")
                {
                    ViewContext.HttpContext.SetStylesCSPHash(CSPHashType, hash);
                }
                else
                {
                    throw new InvalidOperationException("Unexpected tag name: " + context.TagName);
                }
            }
        }
    }
}