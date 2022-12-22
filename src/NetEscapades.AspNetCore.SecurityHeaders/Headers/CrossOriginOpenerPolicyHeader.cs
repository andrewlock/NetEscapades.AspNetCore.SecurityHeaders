using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;

namespace NetEscapades.AspNetCore.SecurityHeaders.Headers
{
    /// <summary>
    /// The header value to use for Cross-Origin-Opener-Policy
    /// </summary>
    public abstract class CrossOriginOpenerPolicyHeader : DocumentHeaderPolicyBase
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CrossOriginOpenerPolicyHeader"/> class.
        /// </summary>
        /// <param name="reportOnly">If true, the Cross-Origin-Opener-Policy header is added as "Cross-Origin-Opener-Policy-Report-Only".</param>
        protected CrossOriginOpenerPolicyHeader(bool reportOnly)
        {
            ReportOnly = reportOnly;
        }

        /// <inheritdoc />
        public override string Header => ReportOnly ? "Cross-Origin-Opener-Policy-Report-Only" : "Cross-Origin-Opener-Policy";

        /// <summary>
        /// If true, the COOP header is added as "Cross-Origin-Opener-Policy-Report-Only".
        /// If false, it's set to "Cross-Origin-Opener-Policy";
        /// </summary>
        internal bool ReportOnly { get; }

        /// <summary>
        /// Configure a Cross Origin Opener Policy
        /// </summary>
        /// <param name="configure">Configure the Cross Origin Opener Policy</param>
        /// <param name="asReportOnly">If true, the header is added as report only</param>
        /// <returns>The configured <see cref="CrossOriginOpenerPolicyHeader "/></returns>
        public static CrossOriginOpenerPolicyHeader Build(Action<CrossOriginOpenerPolicyBuilder> configure, bool asReportOnly)
        {
            var builder = new CrossOriginOpenerPolicyBuilder();

            configure(builder);

            var coopResult = builder.Build();

            return new StaticCrossOriginOpenerPolicyHeader(coopResult.ConstantValue, asReportOnly);
        }

        /// <summary>
        /// A <see cref="CrossOriginOpenerPolicyHeader"/> which has a single static value
        /// </summary>
        public class StaticCrossOriginOpenerPolicyHeader : CrossOriginOpenerPolicyHeader
        {
            private readonly string _value;

            /// <summary>
            /// Initializes a new instance of the <see cref="CrossOriginOpenerPolicyHeader.StaticCrossOriginOpenerPolicyHeader"/> class.
            /// </summary>
            /// <param name="asReportOnly">If true, the header is added as report only</param>
            /// <param name="value">The value to apply for the header</param>
            public StaticCrossOriginOpenerPolicyHeader(string value, bool asReportOnly) : base(reportOnly: asReportOnly)
            {
                _value = value;
            }

            /// <inheritdoc />
            protected override string GetValue(HttpContext context) => _value;
        }
    }
}