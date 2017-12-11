namespace NetEscapades.AspNetCore.SecurityHeaders.Infrastructure
{
    /// <summary>
    /// The script-src directive specifies valid sources for sources for JavaScript.
    /// This includes not only URLs loaded directly into &lt;script&gt; elements, but also things
    /// like inline script event handlers (onclick) and XSLT stylesheets which can trigger script execution.
    /// </summary>
    public class ScriptSourceDirectiveBuilder : CspDirectiveBuilder
    {
        public ScriptSourceDirectiveBuilder() : base("script-src")
        {
        }

        /// <summary>
        /// Requires a sample of the violating code to be included in the violation report
        /// </summary>
        /// <returns>The CSP builder for method chaining</returns>
        public ScriptSourceDirectiveBuilder ReportSample()
        {
            Sources.Add("'report-sample'");
            return this;
        }
    }
}