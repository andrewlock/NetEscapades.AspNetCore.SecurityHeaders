using System;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Razor.TagHelpers;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace NetEscapades.AspNetCore.SecurityHeaders.TagHelpers
{
    /// <summary>
    /// Tag helper for adding a nonce to script and style tags.
    /// </summary>
    [HtmlTargetElement("script", Attributes = "asp-add-nonce")]
    [HtmlTargetElement("style", Attributes = "asp-add-nonce")]
    [HtmlTargetElement("frame", Attributes = "asp-add-nonce")]
    [HtmlTargetElement("iframe", Attributes = "asp-add-nonce")]
    [HtmlTargetElement("img", Attributes = "asp-add-nonce")]
    [HtmlTargetElement("audio", Attributes = "asp-add-nonce")]
    [HtmlTargetElement("video", Attributes = "asp-add-nonce")]
    [HtmlTargetElement("object", Attributes = "asp-add-nonce")]
    [HtmlTargetElement("applet", Attributes = "asp-add-nonce")]
    [HtmlTargetElement("embed", Attributes = "asp-add-nonce")]
    [HtmlTargetElement("base", Attributes = "asp-add-nonce")]
    [HtmlTargetElement("link", Attributes = "asp-add-nonce")]
    public class NonceTagHelper : TagHelper
    {
        /// <summary>
        /// Add a <code>nonce</code> attribute to the element
        /// </summary>
        [HtmlAttributeName("asp-add-nonce")]
        public bool AddNonce { get; set; }

        /// <summary>
        /// Provides access to the <see cref="ViewContext"/>
        /// </summary>
        [ViewContext]
        public ViewContext? ViewContext { get; set; }

        /// <inheritdoc />
        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            if (AddNonce)
            {
                if (ViewContext is null)
                {
                    throw new InvalidOperationException("ViewContext was null");
                }

                var nonce = ViewContext.HttpContext.GetNonce();

                if (string.IsNullOrEmpty(nonce))
                {
                    var logger = ViewContext.HttpContext.RequestServices.GetRequiredService<ILogger<NonceTagHelper>>();
                    logger.LogInformation("Nonce attribute not set, nonce value for request not set");
                }
                else
                {
                    output.Attributes.Add("nonce", nonce);
                }
            }
        }
    }
}