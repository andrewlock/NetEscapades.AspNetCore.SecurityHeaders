using System.Collections.Generic;
using System.Linq;
using NetEscapades.AspNetCore.SecurityHeaders.Headers.ContentSecurityPolicy;

// ReSharper disable once CheckNamespace
namespace Microsoft.AspNetCore.Builder
{
    /// <summary>
    /// Used to build a CSP header from multiple directives.
    /// </summary>
    public class CspBuilder
    {
        private const string _directiveSeparator = "; ";

        private readonly Dictionary<string, CspDirectiveBuilderBase> _directives =
            new Dictionary<string, CspDirectiveBuilderBase>();

        /// <summary>
        /// The default-src directive serves as a fallback for the other CSP fetch directives.
        /// Valid sources include 'self', 'unsafe-inline', 'unsafe-eval', 'none', scheme such as http:,
        /// or internet hosts by name or IP address, as well as an optional URL scheme and/or port number.
        /// The site's address may include an optional leading wildcard (the asterisk character, '*'), and
        /// you may use a wildcard (again, '*') as the port number, indicating that all legal ports are valid for the source.
        /// </summary>
        /// <returns>A configured <see cref="DefaultSourceDirectiveBuilder"/></returns>
        public DefaultSourceDirectiveBuilder AddDefaultSrc() => AddDirective(new DefaultSourceDirectiveBuilder());

        /// <summary>
        /// The connect-src directive restricts the URLs which can be loaded using script interfaces
        /// The APIs that are restricted are:  &lt;a&gt; ping, Fetch, XMLHttpRequest, WebSocket, and EventSource.
        /// </summary>
        /// <returns>A configured <see cref="ConnectSourceDirectiveBuilder"/></returns>
        public ConnectSourceDirectiveBuilder AddConnectSrc() => AddDirective(new ConnectSourceDirectiveBuilder());

        /// <summary>
        /// The font-src directive specifies valid sources for fonts loaded using @font-face.
        /// </summary>
        /// <returns>A configured <see cref="FontSourceDirectiveBuilder"/></returns>
        public FontSourceDirectiveBuilder AddFontSrc() => AddDirective(new FontSourceDirectiveBuilder());

        /// <summary>
        /// The object-src directive specifies valid sources for the &lt;object&gt;, &lt;embed&gt;, and &lt;applet&gt; elements
        /// </summary>
        /// <returns>A configured <see cref="ObjectSourceDirectiveBuilder"/></returns>
        public ObjectSourceDirectiveBuilder AddObjectSrc() => AddDirective(new ObjectSourceDirectiveBuilder());

        /// <summary>
        /// The form-action directive restricts the URLs which can be used as the target of a form submissions from a given context
        /// </summary>
        /// <returns>A configured <see cref="FormActionDirectiveBuilder"/></returns>
        public FormActionDirectiveBuilder AddFormAction() => AddDirective(new FormActionDirectiveBuilder());

        /// <summary>
        /// The img-src directive specifies valid sources of images and favicons
        /// </summary>
        /// <returns>A configured <see cref="ImageSourceDirectiveBuilder"/></returns>
        public ImageSourceDirectiveBuilder AddImgSrc() => AddDirective(new ImageSourceDirectiveBuilder());

        /// <summary>
        /// The script-src directive specifies valid sources for sources for JavaScript.
        /// </summary>
        /// <returns>A configured <see cref="ScriptSourceDirectiveBuilder"/></returns>
        public ScriptSourceDirectiveBuilder AddScriptSrc() => AddDirective(new ScriptSourceDirectiveBuilder());

        /// <summary>
        /// The style-src directive specifies valid sources for sources for stylesheets.
        /// </summary>
        /// <returns>A configured <see cref="StyleSourceDirectiveBuilder"/></returns>
        public StyleSourceDirectiveBuilder AddStyleSrc() => AddDirective(new StyleSourceDirectiveBuilder());

        /// <summary>
        /// The media-src directive specifies valid sources for loading media using the &lt;audio&gt; and &lt;video&gt; elements
        /// </summary>
        /// <returns>A configured <see cref="MediaSourceDirectiveBuilder"/></returns>
        public MediaSourceDirectiveBuilder AddMediaSrc() => AddDirective(new MediaSourceDirectiveBuilder());

        /// <summary>
        /// The frame-ancestors directive specifies valid parents that may embed a page using
        /// &lt;frame&gt;, &lt;iframe&gt;, &lt;object&gt;, &lt;embed&gt;, or &lt;applet&gt;.
        /// Setting this directive to 'none' is similar to X-Frame-Options: DENY (which is also supported in older browers).
        /// </summary>
        /// <returns>A configured <see cref="FrameAncestorsDirectiveBuilder"/></returns>
        public FrameAncestorsDirectiveBuilder AddFrameAncestors() => AddDirective(new FrameAncestorsDirectiveBuilder());

        /// <summary>
        /// The frame-src directive specifies valid sources for nested browsing contexts loading
        /// using elements such as  &lt;frame&gt; and  &lt;iframe&gt;
        /// </summary>
        /// <returns>A configured <see cref="FrameSourceDirectiveBuilder"/></returns>
        public FrameSourceDirectiveBuilder AddFrameSource() => AddDirective(new FrameSourceDirectiveBuilder());

        /// <summary>
        /// The worker-src directive specifies valid sources for Worker, SharedWorker, or ServiceWorker scripts.
        /// </summary>
        /// <returns>A configured <see cref="WorkerSourceDirectiveBuilder"/></returns>
        public WorkerSourceDirectiveBuilder AddWorkerSrc() => AddDirective(new WorkerSourceDirectiveBuilder());

        /// <summary>
        /// The base-uri directive restricts the URLs which can be used in a document's
        /// &lt;base&gt; element. If this value is absent, then any URI is allowed. If this
        /// directive is absent, the user agent will use the value in the &lt;base&gt; element.
        /// </summary>
        /// <returns>A configured <see cref="BaseUriDirectiveBuilder"/></returns>
        public BaseUriDirectiveBuilder AddBaseUri() => AddDirective(new BaseUriDirectiveBuilder());

        /// <summary>
        /// The upgrade-insecure-requests directive instructs user agents to treat all of a
        /// site's insecure URLs (those served over HTTP) as though they have been
        /// replaced with secure URLs (those served over HTTPS). This directive is
        /// intended for web sites with large numbers of insecure legacy URLs that need to be rewritten.
        /// </summary>
        /// <returns>A configured <see cref="UpgradeInsecureRequestsDirectiveBuilder"/></returns>
        public UpgradeInsecureRequestsDirectiveBuilder AddUpgradeInsecureRequests() => AddDirective(new UpgradeInsecureRequestsDirectiveBuilder());

        /// <summary>
        /// The block-all-mixed-content directive prevents loading any assets using
        /// HTTP when the page is loaded using HTTPS.
        ///
        /// All mixed content resource requests are blocked, including both active
        /// and passive mixed content. This also applies to &lt;iframe&gt; documents,
        /// ensuring the entire page is mixed content free.
        ///
        /// The upgrade-insecure-requests directive is evaluated before block-all-mixed-content
        /// and If the former is set, the latter is effectively a no-op.
        /// It is recommended to set one directive or the other – not both.
        /// </summary>
        /// <returns>A configured <see cref="BlockAllMixedContentDirectiveBuilder"/></returns>
        public BlockAllMixedContentDirectiveBuilder AddBlockAllMixedContent() => AddDirective(new BlockAllMixedContentDirectiveBuilder());

        /// <summary>
        /// The report-uri directive instructs the user agent to report attempts to
        /// violate the Content Security Policy. These violation reports consist of
        /// JSON documents sent via an HTTP POST request to the specified URI.
        /// </summary>
        /// <returns>A configured <see cref="ReportUriDirectiveBuilder"/></returns>
        public ReportUriDirectiveBuilder AddReportUri() => AddDirective(new ReportUriDirectiveBuilder());

        /// <summary>
        /// Create a custom CSP directive for an un-implemented directive
        /// </summary>
        /// <param name="directive">The directive name, e.g. default-src</param>
        /// <returns>A configured <see cref="CustomDirective"/></returns>
        public CustomDirective AddCustomDirective(string directive) => AddDirective(new CustomDirective(directive));

        /// <summary>
        /// Create a custom CSP directive for an un-implemented directive
        /// </summary>
        /// <param name="directive">The directive name, e.g. default-src</param>
        /// <param name="value">The directive value</param>
        /// <returns>A configured <see cref="CustomDirective"/></returns>
        public CustomDirective AddCustomDirective(string directive, string value) => AddDirective(new CustomDirective(directive, value));

        private T AddDirective<T>(T directive) where T : CspDirectiveBuilderBase
        {
            _directives[directive.Directive] = directive;
            return directive;
        }

        /// <summary>
        /// Build the CSP directive
        /// </summary>
        /// <returns>The CSP directive as a string</returns>
        internal CspBuilderResult Build()
        {
            // build the constant values ahead of time
            var staticDirectives = _directives.Values
                .Where(x => !x.HasPerRequestValues)
                .Select(x => x.CreateBuilder().Invoke(null!))
                .Where(x => !string.IsNullOrEmpty(x));

            var constantDirective = string.Join(_directiveSeparator, staticDirectives);

            var factories = _directives.Values
                .Where(x => x.HasPerRequestValues)
                .Select(x => x.CreateBuilder())
                .ToList();

            if (factories.Count == 0)
            {
                // no dynamic values, just return the string
                return CspBuilderResult.CreateStaticResult(constantDirective);
            }

            if (!string.IsNullOrEmpty(constantDirective))
            {
                factories.Add(ctx => constantDirective);
            }

            return CspBuilderResult.CreateDynamicResult(ctx =>
                string.Join(_directiveSeparator, factories.Select(factory => factory.Invoke(ctx))));
        }
    }
}