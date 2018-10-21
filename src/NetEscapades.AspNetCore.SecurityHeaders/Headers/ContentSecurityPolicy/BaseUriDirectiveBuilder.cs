namespace NetEscapades.AspNetCore.SecurityHeaders.Headers.ContentSecurityPolicy
{
    /// <summary>
    /// The base-uri directive restricts the URLs which can be used in a document's
    /// &lt;base&gt; element. If this value is absent, then any URI is allowed. If this
    /// directive is absent, the user agent will use the value in the &lt;base&gt; element.
    /// </summary>
    public class BaseUriDirectiveBuilder : CspDirectiveBuilder
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="BaseUriDirectiveBuilder"/> class.
        /// </summary>
        public BaseUriDirectiveBuilder()
            : base("base-uri")
        {
        }
    }
}