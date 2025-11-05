namespace NetEscapades.AspNetCore.SecurityHeaders.Headers.ContentSecurityPolicy;

/// <summary>
/// Create a custom CSP directive for an un-implemented directive.
/// You can use this builder to add non-standard directives that are not yet
/// supported by the library, are experimental, or are no longer supported.
/// </summary>
[CspMixin(MixinTypes.All)]
public partial class CustomDirectiveBuilder : CspDirectiveBuilder
{
    /// <summary>
    /// Initializes a new instance of the <see cref="CustomDirectiveBuilder"/> class.
    /// </summary>
    /// <param name="directive">The directive name, e.g. <c>default-src</c></param>
    public CustomDirectiveBuilder(string directive) : base(directive)
    {
    }
}