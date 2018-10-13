using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Http;

namespace NetEscapades.AspNetCore.SecurityHeaders.Infrastructure
{
    /// <summary>
    /// The frame-ancestors directive specifies valid parents that may embed a page using
    /// &lt;frame&gt;, &lt;iframe&gt;, &lt;object&gt;, &lt;embed&gt;, or &lt;applet&gt;.
    /// Setting this directive to 'none' is similar to X-Frame-Options: DENY (which is also supported in older browers).
    /// </summary>
    public class FrameAncestorsDirectiveBuilder : CspDirectiveBuilderBase
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="FrameAncestorsDirectiveBuilder"/> class.
        /// </summary>
        public FrameAncestorsDirectiveBuilder() : base("frame-ancestors")
        {
        }

        /// <summary>
        /// The sources from which the directive is allowed.
        /// </summary>
        public List<string> Sources { get; } = new List<string>();

        /// <summary>
        /// If true, no sources are allowed.
        /// </summary>
        public bool BlockResources { get; set; } = false;

        /// <inheritdoc />
        internal override Func<HttpContext, string> CreateBuilder()
        {
            if (BlockResources)
            {
                return ctx => GetPolicy("'none'");
            }

            return ctx => GetPolicy(string.Join(" ", Sources));
        }

        private string GetPolicy(string value)
        {
            if (string.IsNullOrEmpty(value))
            {
                return string.Empty;
            }

            return $"{Directive} {value}";
        }

        /// <summary>
        /// Allow sources from the origin from which the protected document is being served, including the same URL scheme and port number
        /// </summary>
        /// <returns>The CSP builder for method chaining</returns>
        public FrameAncestorsDirectiveBuilder Self()
        {
            Sources.Add("'self'");
            return this;
        }

        /// <summary>
        /// Allows blob: URIs to be used as a content source
        /// </summary>
        /// <returns>The CSP builder for method chaining</returns>
        public FrameAncestorsDirectiveBuilder Blob()
        {
            Sources.Add("blob:");
            return this;
        }

        /// <summary>
        /// data: Allows data: URIs to be used as a content source.
        /// WARNING: This is insecure; an attacker can also inject arbitrary data: URIs. Use this sparingly and definitely not for scripts.
        /// </summary>
        /// <returns>The CSP builder for method chaining</returns>
        public FrameAncestorsDirectiveBuilder Data()
        {
            Sources.Add("data:");
            return this;
        }

        /// <summary>
        /// Block the resource (Refers to the empty set; that is, no URLs match)
        /// </summary>
        /// <returns>The CSP builder for method chaining</returns>
        public FrameAncestorsDirectiveBuilder None()
        {
            BlockResources = true;
            return this;
        }

        /// <summary>
        /// Allow resources from the given <paramref name="uri"/>. May be any non-empty value
        /// </summary>
        /// <param name="uri">The URI to allow.</param>
        /// <returns>The CSP builder for method chaining</returns>
        public FrameAncestorsDirectiveBuilder From(string uri)
        {
            if (string.IsNullOrWhiteSpace(uri))
            {
                throw new System.ArgumentException("Uri may not be null or empty", nameof(uri));
            }

            Sources.Add(uri);
            return this;
        }

        /// <summary>
        /// Allow resources served over https
        /// </summary>
        /// <returns>The CSP builder for method chaining</returns>
        public FrameAncestorsDirectiveBuilder OverHttps()
        {
            Sources.Add("https:");
            return this;
        }
    }
}