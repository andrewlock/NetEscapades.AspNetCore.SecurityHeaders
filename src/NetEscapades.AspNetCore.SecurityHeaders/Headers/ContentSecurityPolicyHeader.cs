using System;
using Microsoft.AspNetCore.Http;

namespace NetEscapades.AspNetCore.SecurityHeaders.Infrastructure
{
    /// <summary>
    /// The header value to use for Content-SecurityHeader
    /// </summary>
    public class ContentSecurityPolicyHeader : HtmlOnlyHeaderPolicyBase
    {
        private readonly string _value;

        /// <summary>
        /// Initializes a new instance of the <see cref="ContentSecurityPolicyHeader"/> class.
        /// </summary>
        /// <param name="asReportOnly">If true, the header is added as report only</param>
        /// <param name="isUniquePerRequest">If true, the header is unique per request, and should use dynamic
        /// values (such as a nonce)</param>
        /// <param name="value">The value to apply for the header</param>
        public ContentSecurityPolicyHeader(string value, bool asReportOnly, bool isUniquePerRequest)
        {
            _value = value;
            ReportOnly = asReportOnly;
            IsUniquePerRequest = isUniquePerRequest;
        }

        /// <inheritdoc />
        public override string Header => ReportOnly ? "Content-Security-Policy-Report-Only" : "Content-Security-Policy";

        /// <summary>
        /// If true, the CSP header is addded as "Content-Security-Policy-Report-Only".
        /// If false, it's set to "Content-Security-Policy";
        /// </summary>
        public bool ReportOnly { get; }

        /// <summary>
        /// If true, the header directives are unique per request, and require
        /// runtime formatting (e.g. for use with Nonce)
        /// </summary>
        internal bool IsUniquePerRequest { get; }

        /// <inheritdoc />
        protected override string GetValue(HttpContext context) => IsUniquePerRequest ? GetFormattedValue(context) : _value;

        private string GetFormattedValue(HttpContext context)
        {
            var nonce = context.Items[Constants.DefaultNonceKey];
            return string.Format(_value, nonce);
        }

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

            return new ContentSecurityPolicyHeader(cspResult.Value, asReportOnly, cspResult.IsUniquePerRequest);
        }
    }
}