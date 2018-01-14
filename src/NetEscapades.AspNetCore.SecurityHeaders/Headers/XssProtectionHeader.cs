using System;

namespace NetEscapades.AspNetCore.SecurityHeaders.Infrastructure
{
    /// <summary>
    /// The header value to use for XSS-Protection
    /// </summary>
    public class XssProtectionHeader : HtmlOnlyHeaderPolicyBase
    {
        /// <inheritdoc />
        public XssProtectionHeader(string value) : base(value)
        {
            Value = value;
        }

        /// <inheritdoc />
        public override string Header { get; } = "X-XSS-Protection";
    }
}