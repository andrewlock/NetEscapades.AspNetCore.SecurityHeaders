namespace NetEscapades.AspNetCore.SecurityHeaders.Infrastructure
{
    /// <summary>
    /// The default-src directive serves as a fallback for the other CSP fetch directives.
    /// Valid sources include 'self', 'unsafe-inline', 'unsafe-eval', 'none', scheme such as http:,
    /// or internet hosts by name or IP address, as well as an optional URL scheme and/or port number. 
    /// The site's address may include an optional leading wildcard (the asterisk character, '*'), and 
    /// you may use a wildcard (again, '*') as the port number, indicating that all legal ports are valid for the source.
    /// </summary>
    public class DefaultSourceDirectiveBuilder : CspDirectiveBuilder
    {
        public DefaultSourceDirectiveBuilder() : base("default-src")
        {
        }
    }
}