﻿using System;
using System.Collections.Generic;
using System.Linq;
using NetEscapades.AspNetCore.SecurityHeaders.Headers.ContentSecurityPolicy;

// ReSharper disable once CheckNamespace
namespace Microsoft.AspNetCore.Builder;

/// <summary>
/// Used to build a CSP header from multiple directives.
/// </summary>
public class CspBuilder
{
    private const string _directiveSeparator = "; ";

    private readonly Dictionary<string, CspDirectiveBuilderBase> _directives =
        new Dictionary<string, CspDirectiveBuilderBase>();

    /// <summary>
    /// The <c>default-src</c> directive serves as a fallback for the other CSP fetch directives.
    /// Valid sources include 'self', 'unsafe-inline', 'unsafe-eval', 'none', scheme such as http:,
    /// or internet hosts by name or IP address, as well as an optional URL scheme and/or port number.
    /// The site's address may include an optional leading wildcard (the asterisk character, '*'), and
    /// you may use a wildcard (again, '*') as the port number, indicating that all legal ports are valid for the source.
    /// </summary>
    /// <returns>A configured <see cref="DefaultSourceDirectiveBuilder"/></returns>
    public DefaultSourceDirectiveBuilder AddDefaultSrc() => AddDirective(new DefaultSourceDirectiveBuilder());

    /// <summary>
    /// The <c>connect-src</c> directive restricts the URLs which can be loaded using script interfaces
    /// The APIs that are restricted are:  &lt;a&gt; ping, Fetch, XMLHttpRequest, WebSocket, and EventSource.
    /// </summary>
    /// <returns>A configured <see cref="ConnectSourceDirectiveBuilder"/></returns>
    public ConnectSourceDirectiveBuilder AddConnectSrc() => AddDirective(new ConnectSourceDirectiveBuilder());

    /// <summary>
    /// The <c>font-src</c> directive specifies valid sources for fonts loaded using <see href="https://developer.mozilla.org/en-US/docs/Web/CSS/@font-face">@font-face</see>.
    /// </summary>
    /// <returns>A configured <see cref="FontSourceDirectiveBuilder"/></returns>
    public FontSourceDirectiveBuilder AddFontSrc() => AddDirective(new FontSourceDirectiveBuilder());

    /// <summary>
    /// The <c>object-src</c> directive specifies valid sources for the &lt;object&gt;, &lt;embed&gt;, and &lt;applet&gt; elements
    /// </summary>
    /// <returns>A configured <see cref="ObjectSourceDirectiveBuilder"/></returns>
    public ObjectSourceDirectiveBuilder AddObjectSrc() => AddDirective(new ObjectSourceDirectiveBuilder());

    /// <summary>
    /// The <c>form-action</c> directive restricts the URLs which can be used as the target of a form submissions from a given context
    /// </summary>
    /// <returns>A configured <see cref="FormActionDirectiveBuilder"/></returns>
    public FormActionDirectiveBuilder AddFormAction() => AddDirective(new FormActionDirectiveBuilder());

    /// <summary>
    /// The <c>img-src</c> directive specifies valid sources of images and favicons>
    /// </summary>
    /// <returns>A configured <see cref="ImageSourceDirectiveBuilder"/></returns>
    public ImageSourceDirectiveBuilder AddImgSrc() => AddDirective(new ImageSourceDirectiveBuilder());

    /// <summary>
    /// The <c>script-src</c> directive specifies valid sources for sources for JavaScript.
    /// </summary>
    /// <returns>A configured <see cref="ScriptSourceDirectiveBuilder"/></returns>
    public ScriptSourceDirectiveBuilder AddScriptSrc() => AddDirective(new ScriptSourceDirectiveBuilder());

    /// <summary>
    /// The <c>script-src-attr</c> directive specifies valid sources for JavaScript inline event handlers.
    /// </summary>
    /// <returns>A configured <see cref="ScriptSourceAttrDirectiveBuilder"/></returns>
    public ScriptSourceAttrDirectiveBuilder AddScriptSrcAttr() => AddDirective(new ScriptSourceAttrDirectiveBuilder());

    /// <summary>
    /// The <c>script-src-elem</c> directive specifies valid sources for JavaScript &lt;script&gt; elements.
    /// </summary>
    /// <returns>A configured <see cref="ScriptSourceAttrDirectiveBuilder"/></returns>
    public ScriptSourceElemDirectiveBuilder AddScriptSrcElem() => AddDirective(new ScriptSourceElemDirectiveBuilder());

    /// <summary>
    /// The <c>style-src</c> directive specifies valid sources for sources for stylesheets.
    /// </summary>
    /// <returns>A configured <see cref="StyleSourceDirectiveBuilder"/></returns>
    public StyleSourceDirectiveBuilder AddStyleSrc() => AddDirective(new StyleSourceDirectiveBuilder());

    /// <summary>
    /// The <c>style-src-attr</c> directive specifies valid sources for inline styles applied to individual DOM elements.
    /// </summary>
    /// <returns>A configured <see cref="StyleSourceAttrDirectiveBuilder"/></returns>
    public StyleSourceAttrDirectiveBuilder AddStyleSrcAttr() => AddDirective(new StyleSourceAttrDirectiveBuilder());

    /// <summary>
    /// The <c>style-src-elem</c> directive specifies valid sources for stylesheet &lt;style&gt; elements
    /// and &lt;link&gt; elements with rel="stylesheet".
    /// </summary>
    /// <returns>A configured <see cref="StyleSourceAttrDirectiveBuilder"/></returns>
    public StyleSourceElemDirectiveBuilder AddStyleSrcElem() => AddDirective(new StyleSourceElemDirectiveBuilder());

    /// <summary>
    /// The <c>media-src</c> directive specifies valid sources for loading media using the &lt;audio&gt; and &lt;video&gt; elements
    /// </summary>
    /// <returns>A configured <see cref="MediaSourceDirectiveBuilder"/></returns>
    public MediaSourceDirectiveBuilder AddMediaSrc() => AddDirective(new MediaSourceDirectiveBuilder());

    /// <summary>
    /// The <c>manifest-src</c> directive specifies which manifest can be applied to the resource.
    /// </summary>
    /// <returns>A configured <see cref="ManifestSourceDirectiveBuilder"/></returns>
    public ManifestSourceDirectiveBuilder AddManifestSrc() => AddDirective(new ManifestSourceDirectiveBuilder());

    /// <summary>
    /// The <c>frame-ancestors</c> directive specifies valid parents that may embed a page using
    /// &lt;frame&gt;, &lt;iframe&gt;, &lt;object&gt;, &lt;embed&gt;, or &lt;applet&gt;.
    /// Setting this directive to 'none' is similar to X-Frame-Options: DENY (which is also supported in older browsers).
    /// </summary>
    /// <returns>A configured <see cref="FrameAncestorsDirectiveBuilder"/></returns>
    public FrameAncestorsDirectiveBuilder AddFrameAncestors() => AddDirective(new FrameAncestorsDirectiveBuilder());

    /// <summary>
    /// The <c>frame-src</c> directive specifies valid sources for nested browsing contexts loading
    /// using elements such as  &lt;frame&gt; and  &lt;iframe&gt;
    /// </summary>
    /// <returns>A configured <see cref="FrameSourceDirectiveBuilder"/></returns>
    public FrameSourceDirectiveBuilder AddFrameSrc() => AddDirective(new FrameSourceDirectiveBuilder());

    /// <summary>
    /// The <c>worker-src</c> directive specifies valid sources for Worker, SharedWorker, or ServiceWorker scripts.
    /// </summary>
    /// <returns>A configured <see cref="WorkerSourceDirectiveBuilder"/></returns>
    public WorkerSourceDirectiveBuilder AddWorkerSrc() => AddDirective(new WorkerSourceDirectiveBuilder());

    /// <summary>
    /// The <c>base-uri</c> directive restricts the URLs which can be used in a document's
    /// &lt;base&gt; element. If this value is absent, then any URI is allowed. If this
    /// directive is absent, the user agent will use the value in the &lt;base&gt; element.
    /// </summary>
    /// <returns>A configured <see cref="BaseUriDirectiveBuilder"/></returns>
    public BaseUriDirectiveBuilder AddBaseUri() => AddDirective(new BaseUriDirectiveBuilder());

    /// <summary>
    /// The <c>sandbox</c> directive enables a sandbox for the requested resource similar
    /// to the &lt;script&gt; sandbox attribute. It applies restrictions to a
    /// page's actions including preventing popups, preventing the execution
    /// of plugins and scripts, and enforcing a same-origin policy.
    /// </summary>
    /// <returns>A configured <see cref="SandboxDirectiveBuilder"/></returns>
    public SandboxDirectiveBuilder AddSandbox() => AddDirective(new SandboxDirectiveBuilder());

    /// <summary>
    /// The <c>upgrade-insecure-requests</c> directive instructs user agents to treat all of a
    /// site's insecure URLs (those served over HTTP) as though they have been
    /// replaced with secure URLs (those served over HTTPS). This directive is
    /// intended for web sites with large numbers of insecure legacy URLs that need to be rewritten.
    /// </summary>
    /// <returns>A configured <see cref="UpgradeInsecureRequestsDirectiveBuilder"/></returns>
    public UpgradeInsecureRequestsDirectiveBuilder AddUpgradeInsecureRequests() => AddDirective(new UpgradeInsecureRequestsDirectiveBuilder());

    /// <summary>
    /// The <c>block-all-mixed-content</c> directive prevents loading any assets using
    /// HTTP when the page is loaded using HTTPS.
    ///
    /// All mixed content resource requests are blocked, including both active
    /// and passive mixed content. This also applies to &lt;iframe&gt; documents,
    /// ensuring the entire page is mixed content free.
    ///
    /// The <c>upgrade-insecure-requests</c> directive is evaluated before <c>block-all-mixed-content</c>
    /// and If the former is set, the latter is effectively a no-op.
    /// It is recommended to set one directive or the other – not both.
    /// </summary>
    /// <returns>A configured <see cref="BlockAllMixedContentDirectiveBuilder"/></returns>
    public BlockAllMixedContentDirectiveBuilder AddBlockAllMixedContent() => AddDirective(new BlockAllMixedContentDirectiveBuilder());

    /// <summary>
    /// The <c>report-uri</c> directive instructs the user agent to report attempts to
    /// violate the Content Security Policy. These violation reports consist of
    /// JSON documents sent via an HTTP POST request to the specified URI.
    /// </summary>
    /// <returns>A configured <see cref="ReportUriDirectiveBuilder"/></returns>
    /// <remarks>NOTE: this directive has been deprecated in favour of <c>Report-To</c>.
    /// Use <see cref="AddReportTo"/> instead.</remarks>
    public ReportUriDirectiveBuilder AddReportUri() => AddDirective(new ReportUriDirectiveBuilder());

    /// <summary>
    /// The <c>report-to</c> directive instructs the user agent to send requests to
    /// an endpoint defined in a <c>Report-To</c> HTTP header. The directive
    /// has no effect in and of itself, but only gains meaning in
    /// combination with other reporting directives.
    /// </summary>
    /// <param name="groupName">The name of the group in the <code>Report-To</code> JSON field
    /// to send reports to</param>
    /// <returns>A configured <see cref="ReportToDirectiveBuilder"/></returns>
    public ReportToDirectiveBuilder AddReportTo(string groupName) => AddDirective(new ReportToDirectiveBuilder(groupName));

    /// <summary>
    /// The <c>require-trusted-types-for</c> directive instructs user agents
    /// to control the data passed to DOM XSS sink functions, like
    /// <see href="https://developer.mozilla.org/en-US/docs/Web/API/Element/innerHTML">Element.innerHTML</see>
    /// setter.
    ///
    /// When used, those functions only accept non-spoofable, typed values created by Trusted Type
    /// policies, and reject strings. Together with <see cref="AddTrustedTypes"/> which
    /// guards creation of Trusted Type policies, this allows authors to define rules guarding
    /// writing values to the DOM and thus reducing the DOM XSS attack surface to small,
    /// isolated parts of the web application codebase, facilitating their monitoring and code review.
    /// </summary>
    /// <returns>A configured <see cref="RequireTrustedTypesForDirectiveBuilder"/></returns>
    public RequireTrustedTypesForDirectiveBuilder AddRequireTrustedTypesFor() => AddDirective(new RequireTrustedTypesForDirectiveBuilder());

    /// <summary>
    /// The <c>trusted-types</c> directive instructs user agents
    /// to restrict the creation of Trusted Types policies - functions that build non-spoofable,
    /// typed values intended to be passed to DOM XSS sinks in place of strings.
    ///
    /// Together with <see cref="AddRequireTrustedTypesFor"/> this allows authors
    /// to define rules guarding writing values to the DOM and thus reducing the DOM XSS attack
    /// surface to small, isolated parts of the web application codebase, facilitating their
    /// monitoring and code review. This directive declares an allowlist of trusted type policy names
    /// created with <c>trustedTypes.createPolicy</c> from Trusted Types API.
    /// </summary>
    /// <returns>A configured <see cref="RequireTrustedTypesForDirectiveBuilder"/></returns>
    public TrustedTypesDirectiveBuilder AddTrustedTypes() => AddDirective(new TrustedTypesDirectiveBuilder());

    /// <summary>
    /// Create a custom CSP directive for an un-implemented directive
    /// </summary>
    /// <param name="directive">The directive name, e.g. <c>default-src</c></param>
    /// <returns>A configured <see cref="CustomDirective"/></returns>
    public CustomDirective AddCustomDirective(string directive) => AddDirective(new CustomDirective(directive));

    /// <summary>
    /// Create a custom CSP directive for an un-implemented directive
    /// </summary>
    /// <param name="directive">The directive name, e.g. <c>default-src</c></param>
    /// <param name="value">The directive value</param>
    /// <returns>A configured <see cref="CustomDirective"/></returns>
    public CustomDirective AddCustomDirective(string directive, string value) => AddDirective(new CustomDirective(directive, value));

    /// <summary>
    /// Create a custom CSP directive for an un-implemented directive that uses standard or per-request values such as nonce
    /// To create a custom CSP directive with a fixed value, use <see cref="AddCustomDirective(string)"/>
    /// </summary>
    /// <param name="directive">The directive name, e.g. <c>default-src</c></param>
    /// <remarks>If you need a directive without a value, use <see cref="AddCustomDirective(string)"/> instead</remarks>
    /// <returns>A configured <see cref="CspDirectiveBuilder"/></returns>
    public CspDirectiveBuilder AddCustomDirectiveBuilder(string directive) => AddDirective(new CspDirectiveBuilder(directive));

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