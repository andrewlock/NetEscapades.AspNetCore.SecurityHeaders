namespace NetEscapades.AspNetCore.SecurityHeaders.Headers.ContentSecurityPolicy;

/// <summary>
/// The <c>worker-src</c> directive specifies valid sources for Worker, SharedWorker, or ServiceWorker scripts.
/// </summary>
public class WorkerSourceDirectiveBuilder : CspDirectiveBuilder
{
    /// <summary>
    /// Initializes a new instance of the <see cref="WorkerSourceDirectiveBuilder"/> class.
    /// </summary>
    public WorkerSourceDirectiveBuilder() : base("worker-src")
    {
    }
}