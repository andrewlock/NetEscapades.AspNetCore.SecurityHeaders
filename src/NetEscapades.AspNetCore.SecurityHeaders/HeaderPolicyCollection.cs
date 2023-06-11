using System;
using System.Collections.Generic;
using NetEscapades.AspNetCore.SecurityHeaders.Headers;

// ReSharper disable once CheckNamespace
namespace Microsoft.AspNetCore.Builder
{
    /// <summary>
    /// Defines the policies to use for customising security headers for a request.
    /// </summary>
    public class HeaderPolicyCollection : Dictionary<string, IHeaderPolicy>
    {
        /// <summary>
        /// The content types that document-based headers such as Content-Security-Policy should apply to
        /// </summary>
        internal string[]? DocumentHeaderContentTypePrefixes { get; set; } = { "text/html", "application/javascript", "text/javascript" };

        /// <summary>
        /// Apply document-based headers such as Content-Security-Policy to all responses serving the provided
        /// content-type prefixes.
        /// </summary>
        /// <param name="contentTypes">The content-type response prefixes to apply the headers to. For example, using
        /// <c>new [] { "text/" }</c> would cause all document-based headers to be applied only to responses
        /// returning content-types such as <c>"text/plain"</c>, or <c>"text/html"</c></param>
        /// <remarks>Generally, headers such as Content-Security-Policy only make sense at the document level, so
        /// they are typically only applied to HTML documents, and to JavaScript (web workers have their own
        /// policy, <see cref="!:https://developer.mozilla.org/en-US/docs/Web/API/Web_Workers_API/Using_web_workers#content_security_policy" >MDN</see>
        /// for details). However, in some cases, you may want to apply your document headers to additional
        /// content-types.</remarks>
        /// <returns>The <see cref="HeaderPolicyCollection"/> for chaining</returns>
        public HeaderPolicyCollection ApplyDocumentHeadersToContentTypes(string[] contentTypes)
        {
            DocumentHeaderContentTypePrefixes = contentTypes ?? throw new ArgumentNullException(nameof(contentTypes));

            return this;
        }

        /// <summary>
        /// Apply document-based headers such as Content-Security-Policy to all responses, regardless of content-type.
        /// </summary>
        /// <remarks>Generally, headers such as Content-Security-Policy only make sense at the document level, so
        /// they are typically only applied to HTML documents, and to JavaScript (web workers have their own
        /// policy, <see cref="!:https://developer.mozilla.org/en-US/docs/Web/API/Web_Workers_API/Using_web_workers#content_security_policy" >MDN</see>
        /// for details). However, in some cases, you may want to apply your document headers to additional
        /// content-types.</remarks>
        /// <returns>The <see cref="HeaderPolicyCollection"/> for chaining</returns>
        public HeaderPolicyCollection ApplyDocumentHeadersToAllResponses()
        {
            DocumentHeaderContentTypePrefixes = null;

            return this;
        }
    }
}