namespace NetEscapades.AspNetCore.SecurityHeaders.Headers.ContentSecurityPolicy;

/// <summary>
/// The <c>font-src</c> directive specifies valid sources for fonts loaded using @font-face.
/// </summary>
public class FontSourceDirectiveBuilder : CspDirectiveBuilder
{
    /// <summary>
    /// Initializes a new instance of the <see cref="FontSourceDirectiveBuilder"/> class.
    /// </summary>
    public FontSourceDirectiveBuilder() : base("font-src")
    {
    }
}