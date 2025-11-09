using Microsoft.AspNetCore.Http;

namespace NetEscapades.AspNetCore.SecurityHeaders.Headers.ContentSecurityPolicy;

/// <summary>
/// The <c>frame-ancestors</c> directive specifies valid parents that may embed a page using
/// &lt;frame&gt;, &lt;iframe&gt;, &lt;object&gt;, &lt;embed&gt;, or &lt;applet&gt;.
/// Setting this directive to 'none' is similar to X-Frame-Options: DENY (which is also supported in older browers).
/// </summary>
[CspMixin(MixinTypes.HostSource | MixinTypes.SchemeSource | MixinTypes.Self | MixinTypes.None)]
public partial class FrameAncestorsDirectiveBuilder : CspDirectiveBuilderBase
{
    /// <summary>
    /// Initializes a new instance of the <see cref="FrameAncestorsDirectiveBuilder"/> class.
    /// </summary>
    public FrameAncestorsDirectiveBuilder() : base("frame-ancestors")
    {
    }

    /// <summary>
    /// The sources from which the directive is allowed.
    /// </summary>
    public SourceCollection Sources { get; } = new();

    /// <summary>
    /// If true, no sources are allowed.
    /// </summary>
    public bool BlockResources { get; set; } = false;

    /// <inheritdoc />
    internal override Func<HttpContext, string> CreateBuilder()
    {
        if (BlockResources)
        {
            return ctx => GetPolicy("'none'");
        }

        return ctx => GetPolicy(string.Join(" ", Sources));
    }

    private string GetPolicy(string value)
    {
        if (string.IsNullOrEmpty(value))
        {
            return string.Empty;
        }

        return $"{Directive} {value}";
    }
}