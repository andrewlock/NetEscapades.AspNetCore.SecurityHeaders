using System;
using Microsoft.AspNetCore.Http;

namespace NetEscapades.AspNetCore.SecurityHeaders.Infrastructure
{
    /// <summary>
    /// The header value to use for Content-SecurityHeader
    /// </summary>
    public class ContentSecurityPolicyHeader : HtmlOnlyHeaderPolicyBase
    {
        /// <summary>
        /// Create a new policy
        /// </summary>
        /// <param name="asReportOnly">If true, the header is added as report only</param>
        /// <param name="value">The value to apply for the header</param>
        public ContentSecurityPolicyHeader(string value, bool asReportOnly) : base(value)
        {
            ReportOnly = asReportOnly;
        }

        /// <inheritdoc />
        public override string Header => ReportOnly? "Content-Security-Policy-Report-Only" : "Content-Security-Policy";

        /// <summary>
        /// If true, the CSP header is addded as "Content-Security-Policy-Report-Only".
        /// If false, it's set to "Content-Security-Policy";
        /// </summary>
        public bool ReportOnly { get; }

        /// <summary>
        /// Configure a content security policy
        /// </summary>
        /// <param name="configure">Configure the CSP</param>
        /// <param name="asReportOnly">If true, the header is added as report only</param>
        /// <returns></returns>
        public static ContentSecurityPolicyHeader Build(Action<CspBuilder> configure, bool asReportOnly)
        {
            var builder = new CspBuilder();

            configure(builder);

            return new ContentSecurityPolicyHeader(builder.Build(), asReportOnly);
        }
    }
}