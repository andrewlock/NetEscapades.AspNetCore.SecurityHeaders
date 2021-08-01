using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;

namespace NetEscapades.AspNetCore.SecurityHeaders.Headers
{
    /// <summary>
    /// The header value to use for Cross-Origin-Embedder-Policy
    /// </summary>
    public abstract class CrossOriginEmbedderPolicyHeader : HtmlOnlyHeaderPolicyBase
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CrossOriginEmbedderPolicyHeader"/> class.
        /// </summary>
        /// <param name="reportOnly">If true, the Cross-Origin-Embedder-Policy header is added as "Cross-Origin-Embedder-Policy-Report-Only".</param>
        protected CrossOriginEmbedderPolicyHeader(bool reportOnly)
        {
            ReportOnly = reportOnly;
        }

        /// <inheritdoc />
        public override string Header => ReportOnly ? "Cross-Origin-Embedder-Policy-Report-Only" : "Cross-Origin-Embedder-Policy";

        /// <summary>
        /// If true, the COOP header is added as "Cross-Origin-Embedder-Policy-Report-Only".
        /// If false, it's set to "Cross-Origin-Embedder-Policy";
        /// </summary>
        internal bool ReportOnly { get; }

        /// <summary>
        /// Configure a Cross Origin Embedder Policy
        /// </summary>
        /// <param name="configure">Configure the Cross Origin Embedder Policy</param>
        /// <param name="asReportOnly">If true, the header is added as report only</param>
        /// <returns>The configured <see cref="CrossOriginEmbedderPolicyHeader "/></returns>
        public static CrossOriginEmbedderPolicyHeader Build(Action<CrossOriginEmbedderPolicyBuilder> configure, bool asReportOnly)
        {
            var builder = new CrossOriginEmbedderPolicyBuilder();

            configure(builder);

            var coopResult = builder.Build();

            return new StaticCrossOriginEmbedderPolicyHeader(coopResult.ConstantValue, asReportOnly);
        }

        /// <summary>
        /// A <see cref="CrossOriginEmbedderPolicyHeader"/> which has a single static value
        /// </summary>
        public class StaticCrossOriginEmbedderPolicyHeader : CrossOriginEmbedderPolicyHeader
        {
            private readonly string _value;

            /// <summary>
            /// Initializes a new instance of the <see cref="CrossOriginEmbedderPolicyHeader.StaticCrossOriginEmbedderPolicyHeader"/> class.
            /// </summary>
            /// <param name="asReportOnly">If true, the header is added as report only</param>
            /// <param name="value">The value to apply for the header</param>
            public StaticCrossOriginEmbedderPolicyHeader(string value, bool asReportOnly) : base(reportOnly: asReportOnly)
            {
                _value = value;
            }

            /// <inheritdoc />
            protected override string GetValue(HttpContext context) => _value;
        }
    }
}