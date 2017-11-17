using System;

namespace NetEscapades.AspNetCore.SecurityHeaders.Infrastructure
{
    /// <summary>
    /// The header value to use for Content-SecurityHeader
    /// </summary>
    public class ContentSecurityPolicyHeader : HeaderPolicyBase
    {
        /// <inheritdoc />
        public ContentSecurityPolicyHeader(string value) : base(value)
        {
            Value = value;
        }

        /// <inheritdoc />
        public override string Header { get; } = "Content-Security-Policy";
        
        /// <summary>
        /// Configure a content security policy
        /// </summary>
        /// <param name="configure">Configure the CSP</param>
        /// <returns></returns>
        public static ContentSecurityPolicyHeader Build(Action<CspBuilder> configure)
        {
            var builder = new CspBuilder();

            configure(builder);

            return new ContentSecurityPolicyHeader(builder.Build());
        }
    }
}