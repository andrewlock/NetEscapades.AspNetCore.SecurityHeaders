namespace NetEscapades.AspNetCore.SecurityHeaders.Headers.ContentSecurityPolicy;

/// <summary>
/// The <c>form-action</c> directive restricts the URLs which can be used as the target of a form submissions from a given context.
/// </summary>
[CspMixin(MixinTypes.HostSource | MixinTypes.SchemeSource | MixinTypes.Self | MixinTypes.None)]
public partial class FormActionDirectiveBuilder : CspDirectiveBuilder
{
    /// <summary>
    /// Initializes a new instance of the <see cref="FormActionDirectiveBuilder"/> class.
    /// </summary>
    public FormActionDirectiveBuilder() : base("form-action")
    {
    }
}