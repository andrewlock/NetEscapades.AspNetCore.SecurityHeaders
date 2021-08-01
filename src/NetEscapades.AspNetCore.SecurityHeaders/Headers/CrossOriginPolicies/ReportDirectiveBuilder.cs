using System;
using Microsoft.AspNetCore.Http;

namespace NetEscapades.AspNetCore.SecurityHeaders.Headers.CrossOriginPolicies
{
    /// <summary>
    /// The report-to directive instructs the user agent to report attempts to
    /// violate the Cross Origin Policy. These violation reports consist of
    /// JSON documents sent via an HTTP POST request to the specified reporting endpoint.
    /// </summary>
    public class ReportDirectiveBuilder : CrossOriginPolicyDirectiveBuilderBase
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ReportDirectiveBuilder"/> class.
        /// </summary>
        public ReportDirectiveBuilder() : base("report-to")
        {
            Endpoint = string.Empty;
        }

        private string Endpoint { get; set; }

        /// <inheritdoc />
        internal override Func<HttpContext, string> CreateBuilder()
        {
            if (string.IsNullOrEmpty(Endpoint))
            {
                // TODO warn they added a report uri but no uri
                return ctx => string.Empty;
            }

            return ctx => $"{Directive}=\"{Endpoint}\"";
        }

        /// <summary>
        /// Sets the reporting endpoint to post the report to.
        /// </summary>
        /// <param name="endpoint">The reporting endpoint to post the report to.</param>
        /// <returns>A configured ReportDirectiveBuilder object.</returns>
        public CrossOriginPolicyDirectiveBuilderBase To(string endpoint)
        {
            if (string.IsNullOrWhiteSpace(endpoint))
            {
                throw new System.ArgumentException("Endpoint may not be null or empty", nameof(endpoint));
            }

            Endpoint = endpoint;
            return this;
        }
    }
}