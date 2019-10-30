using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;

namespace NetEscapades.AspNetCore.SecurityHeaders.Headers
{
    /// <summary>
    /// The header value to use for Content-Security-Policy
    /// </summary>
    public abstract class ContentSecurityPolicyHeader : HtmlOnlyHeaderPolicyBase
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ContentSecurityPolicyHeader"/> class.
        /// </summary>
        /// <param name="reportOnly">If true, the CSP header is added as "Content-Security-Policy-Report-Only".</param>
        /// <param name="hasPerRequestValues">If true, the header directives are unique per request, and require</param>
        protected ContentSecurityPolicyHeader(bool reportOnly, bool hasPerRequestValues)
        {
            ReportOnly = reportOnly;
            HasPerRequestValues = hasPerRequestValues;
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
                ? (ContentSecurityPolicyHeader)new DynamicContentSecurityPolicyHeader(cspResult.Builder, asReportOnly)
                : new StaticContentSecurityPolicyHeader(cspResult.ConstantValue, asReportOnly);
        }

        /// <summary>
        /// A <see cref="ContentSecurityPolicyHeader"/> which has a single static value
        /// </summary>
        public class StaticContentSecurityPolicyHeader : ContentSecurityPolicyHeader
        {
            private readonly string _value;

            /// <summary>
            /// Initializes a new instance of the <see cref="ContentSecurityPolicyHeader.StaticContentSecurityPolicyHeader"/> class.
            /// </summary>
            /// <param name="asReportOnly">If true, the header is added as report only</param>
            /// <param name="value">The value to apply for the header</param>
            public StaticContentSecurityPolicyHeader(string value, bool asReportOnly)
                : base(reportOnly: asReportOnly, hasPerRequestValues: false)
            {
                _value = value;
            }

            /// <inheritdoc />
            protected override string GetValue(HttpContext context) => _value;
        }

        /// <summary>
        /// A <see cref="ContentSecurityPolicyHeader"/> which has a single static value
        /// </summary>
        public class DynamicContentSecurityPolicyHeader : ContentSecurityPolicyHeader
        {
            private readonly Func<HttpContext, string> _builder;

            /// <summary>
            /// Initializes a new instance of the <see cref="ContentSecurityPolicyHeader.DynamicContentSecurityPolicyHeader"/> class.
            /// </summary>
            /// <param name="builder">A function to generate the header's value for a request </param>
            /// <param name="asReportOnly">If true, the header is added as report only</param>
            public DynamicContentSecurityPolicyHeader(Func<HttpContext, string> builder, bool asReportOnly)
                : base(reportOnly: asReportOnly, hasPerRequestValues: true)
            {
                _builder = builder;
            }

            /// <inheritdoc />
            protected override string GetValue(HttpContext context) => _builder(context);
        }
    }
}