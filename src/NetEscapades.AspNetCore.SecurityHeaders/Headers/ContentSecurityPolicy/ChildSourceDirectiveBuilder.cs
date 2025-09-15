namespace NetEscapades.AspNetCore.SecurityHeaders.Headers.ContentSecurityPolicy;

/// <summary>
/// The <c>child</c> directive specifies valid sources for web workers and
/// nested browsing contexts loaded using elements such as &lt;frame&gt; and &lt;iframe&gt;
/// For workers, non-compliant requests are treated as fatal network errors by the user agent.
/// </summary>
public class ChildSourceDirectiveBuilder : CspDirectiveBuilder
{
    /// <summary>
    /// Initializes a new instance of the <see cref="ChildSourceDirectiveBuilder"/> class.
    /// </summary>
    public ChildSourceDirectiveBuilder() : base("child-src")
    {
    }
}