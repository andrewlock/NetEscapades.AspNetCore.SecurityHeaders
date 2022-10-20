using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Http;

namespace NetEscapades.AspNetCore.SecurityHeaders.Headers.ContentSecurityPolicy;

/// <summary>
/// The sandbox directive enables a sandbox for the requested resource similar
/// to the &lt;script&gt; sandbox attribute. It applies restrictions to a
/// page's actions including preventing popups, preventing the execution
/// of plugins and scripts, and enforcing a same-origin policy.
/// </summary>
public class SandboxDirectiveBuilder : CspDirectiveBuilderBase
{
    private const string Separator = " ";

    /// <summary>
    /// Initializes a new instance of the <see cref="SandboxDirectiveBuilder"/> class.
    /// </summary>
    public SandboxDirectiveBuilder() : base("sandbox")
    {
    }

    /// <summary>
    /// The sandbox tokens to apply.
    /// </summary>
    private List<string> Tokens { get; } = new();

    /// <summary>
    /// Allows for downloads after the user clicks a button or link.
    /// </summary>
    /// <returns>The CSP builder for method chaining</returns>
    public SandboxDirectiveBuilder AllowDownloads()
    {
        Tokens.Add("allow-downloads");
        return this;
    }

    /// <summary>
    /// Allows the page to submit forms. If this keyword is not used, this operation is not allowed.
    /// </summary>
    /// <returns>The CSP builder for method chaining</returns>
    public SandboxDirectiveBuilder AllowForms()
    {
        Tokens.Add("allow-forms");
        return this;
    }

    /// <summary>
    /// Allows the page to open modal windows.
    /// </summary>
    /// <returns>The CSP builder for method chaining</returns>
    public SandboxDirectiveBuilder AllowModals()
    {
        Tokens.Add("allow-modals");
        return this;
    }

    /// <summary>
    /// Allows the page to disable the ability to lock the screen orientation.
    /// </summary>
    /// <returns>The CSP builder for method chaining</returns>
    public SandboxDirectiveBuilder AllowOrientationLock()
    {
        Tokens.Add("allow-orientation-lock");
        return this;
    }

    /// <summary>
    /// Allows the page to use the Pointer Lock API.
    /// </summary>
    /// <returns>The CSP builder for method chaining</returns>
    public SandboxDirectiveBuilder AllowPointerLock()
    {
        Tokens.Add("allow-pointer-lock");
        return this;
    }

    /// <summary>
    /// Allows popups (like from <c>window.open</c>, <c>target="_blank"</c>, <c>showModalDialog</c>).
    /// If this keyword is not used, that functionality will silently fail.
    /// </summary>
    /// <returns>The CSP builder for method chaining</returns>
    public SandboxDirectiveBuilder AllowPopups()
    {
        Tokens.Add("allow-popups");
        return this;
    }

    /// <summary>
    /// Allows a sandboxed document to open new windows without forcing the sandboxing
    /// flags upon them. This will allow, for example, a third-party advertisement
    /// to be safely sandboxed without forcing the same restrictions upon the
    /// page the ad links to.
    /// </summary>
    /// <returns>The CSP builder for method chaining</returns>
    public SandboxDirectiveBuilder AllowPopupsToEscapeSandbox()
    {
        Tokens.Add("allow-popups-to-escape-sandbox");
        return this;
    }

    /// <summary>
    /// Allows embedders to have control over whether an iframe can start a presentation session.
    /// </summary>
    /// <returns>The CSP builder for method chaining</returns>
    public SandboxDirectiveBuilder AllowPresentation()
    {
        Tokens.Add("allow-presentation");
        return this;
    }

    /// <summary>
    /// Allows the content to be treated as being from its normal origin.
    /// If this keyword is not used, the embedded content is treated as being from a unique origin.
    /// </summary>
    /// <returns>The CSP builder for method chaining</returns>
    public SandboxDirectiveBuilder AllowSameOrigin()
    {
        Tokens.Add("allow-same-origin");
        return this;
    }

    /// <summary>
    /// Allows the page to run scripts (but not create pop-up windows).
    /// If this keyword is not used, this operation is not allowed.
    /// </summary>
    /// <returns>The CSP builder for method chaining</returns>
    public SandboxDirectiveBuilder AllowScripts()
    {
        Tokens.Add("allow-scripts");
        return this;
    }

    /// <summary>
    /// Allows the page to navigate (load) content to the top-level
    /// browsing context. If this keyword is not used, this operation is not allowed.
    /// </summary>
    /// <returns>The CSP builder for method chaining</returns>
    public SandboxDirectiveBuilder AllowTopNavigation()
    {
        Tokens.Add("allow-top-navigation");
        return this;
    }

    /// <summary>
    /// Lets the resource navigate the top-level browsing context,
    /// but only if initiated by a user gesture
    /// </summary>
    /// <returns>The CSP builder for method chaining</returns>
    public SandboxDirectiveBuilder AllowTopNavigationByUserActivation()
    {
        Tokens.Add("allow-top-navigation-by-user-activation");
        return this;
    }

    /// <summary>
    /// Allows navigations toward non-fetch schemes to be
    /// handed off to external software.
    /// </summary>
    /// <returns>The CSP builder for method chaining</returns>
    public SandboxDirectiveBuilder AllowTopNavigationToCustomProtocols()
    {
        Tokens.Add("allow-top-navigation-to-custom-protocols");
        return this;
    }

    /// <summary>
    /// Adds a custom token to the sandbox directive. Useful for adding
    /// experimental tokens.
    /// </summary>
    /// <returns>The CSP builder for method chaining</returns>
    /// <param name="token">The token to add</param>
    public SandboxDirectiveBuilder AddCustomToken(string token)
    {
        Tokens.Add(token);
        return this;
    }

    /// <inheritdoc />
    internal override Func<HttpContext, string> CreateBuilder()
    {
        var sources = string.Join(Separator, Tokens);
        var result = string.IsNullOrEmpty(sources) ? Directive : $"{Directive} {sources}";

        return _ => result;
    }
}