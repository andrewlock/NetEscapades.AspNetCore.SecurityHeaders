namespace NetEscapades.AspNetCore.SecurityHeaders.Infrastructure
{
    /// <summary>
    /// The connect-src directive restricts the URLs which can be loaded using script interfaces
    /// The APIs that are restricted are:  &lt;a&gt; ping, Fetch, XMLHttpRequest, WebSocket, and EventSource.
    /// </summary>
    public class ConnectSourceDirectiveBuilder : CspDirectiveBuilder
    {
        public ConnectSourceDirectiveBuilder() : base("connect-src")
        {
        }
    }
}