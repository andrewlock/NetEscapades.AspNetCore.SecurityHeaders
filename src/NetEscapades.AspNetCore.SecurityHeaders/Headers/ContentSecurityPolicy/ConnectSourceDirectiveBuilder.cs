namespace NetEscapades.AspNetCore.SecurityHeaders.Headers.ContentSecurityPolicy;

/// <summary>
/// The <c>connect-src</c> directive restricts the URLs which can be loaded using script interfaces
/// The APIs that are restricted are:  &lt;a&gt; ping, Fetch, XMLHttpRequest, WebSocket, and EventSource.
/// </summary>
[CspMixin(MixinTypes.HostSource | MixinTypes.SchemeSource | MixinTypes.Self | MixinTypes.None)]
public partial class ConnectSourceDirectiveBuilder : CspDirectiveBuilder
{
    /// <summary>
    /// Initializes a new instance of the <see cref="ConnectSourceDirectiveBuilder"/> class.
    /// </summary>
    public ConnectSourceDirectiveBuilder() : base("connect-src")
    {
    }
}