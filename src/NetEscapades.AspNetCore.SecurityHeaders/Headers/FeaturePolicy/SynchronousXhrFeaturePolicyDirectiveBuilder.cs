namespace NetEscapades.AspNetCore.SecurityHeaders.Infrastructure
{
    /// <summary>
    /// Controls whether the current document is allowed to use synchronous XMLHttpRequest transfers.
    /// If disabled in a document, then calls to <code>send()</code> on XMLHttpRequest objects
    /// will throw a <code>NetworkError</code>.
    /// </summary>
    public class SynchronousXhrFeaturePolicyDirectiveBuilder : FeaturePolicyDirectiveBuilder
    {
        public SynchronousXhrFeaturePolicyDirectiveBuilder() : base("sync-xhr")
        {
        }
    }
}