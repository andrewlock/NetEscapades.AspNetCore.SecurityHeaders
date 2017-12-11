namespace NetEscapades.AspNetCore.SecurityHeaders.Infrastructure
{
    /// <summary>
    /// The base-uri directive restricts the URLs which can be used in a document's 
    /// &lt;base&gt; element. If this value is absent, then any URI is allowed. If this 
    /// directive is absent, the user agent will use the value in the &lt;base&gt; element.
    /// </summary>
    public class BaseUriDirectiveBuilder : CspDirectiveBuilder
    {
        public BaseUriDirectiveBuilder() : base("base-uri")
        {
        }
    }
}