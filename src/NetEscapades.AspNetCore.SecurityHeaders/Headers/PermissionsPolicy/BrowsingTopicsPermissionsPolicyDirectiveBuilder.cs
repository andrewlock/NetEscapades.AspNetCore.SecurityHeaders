namespace NetEscapades.AspNetCore.SecurityHeaders.Headers.PermissionsPolicy;

/// <summary>
/// Controls whether the current document is allowed to use the <code>Topics API</code>
/// If disabled then calling the <code>Document.browsingTopics()</code> method or
/// sending a request with a <code>Sec-Browsing-Topics</code> header will fail with a
/// <code>NotAllowedError</code>.
/// </summary>
public class BrowsingTopicsPermissionsPolicyDirectiveBuilder : PermissionsPolicyDirectiveBuilder
{
    /// <summary>
    /// Initializes a new instance of the <see cref="BrowsingTopicsPermissionsPolicyDirectiveBuilder"/> class.
    /// </summary>
    public BrowsingTopicsPermissionsPolicyDirectiveBuilder() : base("browsing-topics")
    {
    }
}