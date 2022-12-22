using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;

namespace NetEscapades.AspNetCore.SecurityHeaders.Headers
{
    /// <summary>
    /// The header value to use for Feature-Policy.
    /// </summary>
    public class FeaturePolicyHeader : DocumentHeaderPolicyBase
    {
        private readonly string _value;

        /// <summary>
        /// Initializes a new instance of the <see cref="FeaturePolicyHeader"/> class.
        /// </summary>
        /// <param name="value">The value to apply for the header</param>
        public FeaturePolicyHeader(string value)
        {
            _value = value;
        }

        /// <inheritdoc />
        public override string Header => "Feature-Policy";

        /// <inheritdoc />
        protected override string GetValue(HttpContext context) => _value;

        /// <summary>
        /// Configure a feature policy.
        /// </summary>
        /// <param name="configure">Configure the Feature-Policy header</param>
        /// <returns>The complete Feature-Policy header</returns>
        public static FeaturePolicyHeader Build(Action<FeaturePolicyBuilder> configure)
        {
            var builder = new FeaturePolicyBuilder();

            configure(builder);

            return new FeaturePolicyHeader(builder.Build());
        }
    }
}