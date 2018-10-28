namespace NetEscapades.AspNetCore.SecurityHeaders.Headers.ContentSecurityPolicy
{
    /// <summary>
    /// The form-action directive restricts the URLs which can be used as the target of a form submissions from a given context.
    /// </summary>
    public class FormActionDirectiveBuilder : CspDirectiveBuilder
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="FormActionDirectiveBuilder"/> class.
        /// </summary>
        public FormActionDirectiveBuilder() : base("form-action")
        {
        }
    }
}