namespace NetEscapades.AspNetCore.SecurityHeaders.Infrastructure
{
    /// <summary>
    /// The upgrade-insecure-requests directive instructs user agents to treat all of a
    /// site's insecure URLs (those served over HTTP) as though they have been
    /// replaced with secure URLs (those served over HTTPS). This directive is
    /// intended for web sites with large numbers of insecure legacy URLs that need to be rewritten.
    /// </summary>
    public class UpgradeInsecureRequestsDirectiveBuilder : CspDirectiveBuilderBase
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="UpgradeInsecureRequestsDirectiveBuilder"/> class.
        /// </summary>
        public UpgradeInsecureRequestsDirectiveBuilder() : base("upgrade-insecure-requests")
        {
        }

        /// <inheritdoc />
        internal override string Build()
        {
            return Directive;
        }
    }
}