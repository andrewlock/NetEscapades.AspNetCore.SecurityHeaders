using System;
using Microsoft.AspNetCore.Http;

namespace NetEscapades.AspNetCore.SecurityHeaders.Infrastructure
{
    /// <summary>
    /// The header value to use for Feature-Policy.
    /// </summary>
    public class FeaturePolicyHeader : HtmlOnlyHeaderPolicyBase
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="FeaturePolicyHeader"/> class.
        /// </summary>
        /// <param name="value">The value to apply for the header</param>
        public FeaturePolicyHeader(string value) : base(value)
        {
        }

        /// <inheritdoc />
        public override string Header => "Feature-Policy";

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