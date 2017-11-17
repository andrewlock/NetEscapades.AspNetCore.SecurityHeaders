namespace NetEscapades.AspNetCore.SecurityHeaders.Infrastructure
{

    /// <summary>
    /// The report-uri directive instructs the user agent to report attempts to
    /// violate the Content Security Policy. These violation reports consist of 
    /// JSON documents sent via an HTTP POST request to the specified URI.
    /// </summary>
    public class ReportUriDirectiveBuilder : CspDirectiveBuilderBase
    {
        public ReportUriDirectiveBuilder() : base("report-uri"){}
        internal override string Build()
        {
            if (string.IsNullOrEmpty(Uri))
            {
                // TODO warn they added a report uri but no uri
                return string.Empty;
            }
            return $"{Directive} {Uri}";
        }

        private string Uri { get; set; }

        /// <summary>
        /// The Uri where to post the report.
        /// </summary>
        /// <param name="uri">The URI to post the report to.</param>
        /// <returns>Nothing, as can't be chained</returns>
        public CspDirectiveBuilderBase To(string uri)
        {
            if (string.IsNullOrWhiteSpace(uri))
            {
                throw new System.ArgumentException("Uri may not be null or empty", nameof(uri));
            }
            Uri = uri;
            return this;
        }
    }
}