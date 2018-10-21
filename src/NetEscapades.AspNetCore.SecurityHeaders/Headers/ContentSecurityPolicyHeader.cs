using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using NetEscapades.AspNetCore.SecurityHeaders.Headers.ContentSecurityPolicy;

namespace NetEscapades.AspNetCore.SecurityHeaders.Headers
{
    /// <summary>
    /// The header value to use for Content-SecurityHeader
    /// </summary>
    public class ContentSecurityPolicyHeader : HtmlOnlyHeaderPolicyBase
    {
        private readonly string _value;
        private readonly Func<HttpContext, string> _builder;

        /// <summary>
        /// Initializes a new instance of the <see cref="ContentSecurityPolicyHeader"/> class.
        /// </summary>
        /// <param name="asReportOnly">If true, the header is added as report only</param>
        /// <param name="value">The value to apply for the header</param>
        public ContentSecurityPolicyHeader(string value, bool asReportOnly)
        {
            _value = value;
            ReportOnly = asReportOnly;
            HasPerRequestValues = false;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ContentSecurityPolicyHeader"/> class.
        /// </summary>
        /// <param name="builder">A function to generate the header's value for a request </param>
        /// <param name="asReportOnly">If true, the header is added as report only</param>
        public ContentSecurityPolicyHeader(Func<HttpContext, string> builder, bool asReportOnly)
        {
            _builder = builder;
            ReportOnly = asReportOnly;
            HasPerRequestValues = true;
        }

        /// <inheritdoc />
        public override string Header => ReportOnly ? "Content-Security-Policy-Report-Only" : "Content-Security-Policy";

        /// <summary>
        /// If true, the CSP header is added as "Content-Security-Policy-Report-Only".
        /// If false, it's set to "Content-Security-Policy";
        /// </summary>
        internal bool ReportOnly { get; }

        /// <summary>
        /// If true, the header directives are unique per request, and require
        /// runtime formatting (e.g. for use with Nonce)
        /// </summary>
        internal bool HasPerRequestValues { get; }

        /// <inheritdoc />
        protected override string GetValue(HttpContext context) => HasPerRequestValues ? _builder(context) : _value;

        /// <summary>
        /// Configure a content security policy
        /// </summary>
        /// <param name="configure">Configure the CSP</param>
        /// <param name="asReportOnly">If true, the header is added as report only</param>
        /// <returns>The configured <see cref="ContentSecurityPolicyHeader "/></returns>
        public static ContentSecurityPolicyHeader Build(Action<CspBuilder> configure, bool asReportOnly)
        {
            var builder = new CspBuilder();

            configure(builder);

            var cspResult = builder.Build();

            return cspResult.HasPerRequestValues
                ? new ContentSecurityPolicyHeader(cspResult.Builder, asReportOnly)
                : new ContentSecurityPolicyHeader(cspResult.ConstantValue, asReportOnly);
        }
    }
}