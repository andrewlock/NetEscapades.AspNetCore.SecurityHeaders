namespace NetEscapades.AspNetCore.SecurityHeaders.Headers.ContentSecurityPolicy;

/// <summary>
/// The <c>manifest-src</c> directive specifies which manifest can be applied to the resource.
/// </summary>
[CspMixin(MixinTypes.HostSource | MixinTypes.SchemeSource | MixinTypes.Self | MixinTypes.None)]
public partial class ManifestSourceDirectiveBuilder : CspDirectiveBuilder
{
    /// <summary>
    /// Initializes a new instance of the <see cref="ManifestSourceDirectiveBuilder"/> class.
    /// </summary>
    public ManifestSourceDirectiveBuilder() : base("manifest-src")
    {
    }
}