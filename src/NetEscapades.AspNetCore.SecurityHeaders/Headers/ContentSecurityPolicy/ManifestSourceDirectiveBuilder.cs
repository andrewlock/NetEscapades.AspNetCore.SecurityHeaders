namespace NetEscapades.AspNetCore.SecurityHeaders.Headers.ContentSecurityPolicy
{
    /// <summary>
    /// The manifest-src directive specifies which manifest can be applied to the resource.
    /// </summary>
    public class ManifestSourceDirectiveBuilder : CspDirectiveBuilder
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ManifestSourceDirectiveBuilder"/> class.
        /// </summary>
        public ManifestSourceDirectiveBuilder() : base("manifest-src")
        {
        }
    }
}
