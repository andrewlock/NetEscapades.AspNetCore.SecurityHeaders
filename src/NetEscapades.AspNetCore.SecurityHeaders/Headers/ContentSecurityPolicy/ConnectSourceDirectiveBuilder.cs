namespace NetEscapades.AspNetCore.SecurityHeaders.Headers.ContentSecurityPolicy
{
    /// <summary>
    /// The connect-src directive restricts the URLs which can be loaded using script interfaces
    /// The APIs that are restricted are:  &lt;a&gt; ping, Fetch, XMLHttpRequest, WebSocket, and EventSource.
    /// </summary>
    public class ConnectSourceDirectiveBuilder : CspDirectiveBuilder
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ConnectSourceDirectiveBuilder"/> class.
        /// </summary>
        public ConnectSourceDirectiveBuilder() : base("connect-src")
        {
        }
    }
}