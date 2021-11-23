using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;

namespace NetEscapades.AspNetCore.SecurityHeaders.Headers
{
    /// <summary>
    /// The header value to use for Cross-Origin-Resource-Policy
    /// </summary>
    public abstract class CrossOriginResourcePolicyHeader : HeaderPolicyBase
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CrossOriginResourcePolicyHeader"/> class.
        /// </summary>
        protected CrossOriginResourcePolicyHeader()
        {
        }

        /// <inheritdoc />
        public override string Header => "Cross-Origin-Resource-Policy";

        /// <summary>
        /// Configure a Cross Origin Resource Policy
        /// </summary>
        /// <param name="configure">Configure the Cross Origin Resource Policy</param>
        /// <returns>The configured <see cref="CrossOriginResourcePolicyHeader "/></returns>
        public static CrossOriginResourcePolicyHeader Build(Action<CrossOriginResourcePolicyBuilder> configure)
        {
            var builder = new CrossOriginResourcePolicyBuilder();

            configure(builder);

            var coopResult = builder.Build();

            return new StaticCrossOriginResourcePolicyHeader(coopResult.ConstantValue);
        }

        /// <summary>
        /// A <see cref="CrossOriginResourcePolicyHeader"/> which has a single static value
        /// </summary>
        public class StaticCrossOriginResourcePolicyHeader : CrossOriginResourcePolicyHeader
        {
            private readonly string _value;

            /// <summary>
            /// Initializes a new instance of the <see cref="CrossOriginResourcePolicyHeader.StaticCrossOriginResourcePolicyHeader"/> class.
            /// </summary>
            /// <param name="value">The value to apply for the header</param>
            public StaticCrossOriginResourcePolicyHeader(string value) : base()
            {
                _value = value;
            }

            /// <inheritdoc />
            protected override string GetValue(HttpContext context) => _value;
        }
    }
}