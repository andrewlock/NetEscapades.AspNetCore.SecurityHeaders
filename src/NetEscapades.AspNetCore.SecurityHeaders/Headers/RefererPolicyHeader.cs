using System;

namespace NetEscapades.AspNetCore.SecurityHeaders.Infrastructure
{

    /// <summary>
    /// The header value to use for ReferrerPolicy
    /// </summary>
    public class ReferrerPolicyHeader : HtmlOnlyHeaderPolicyBase
    {
        /// <inheritdoc />
        public ReferrerPolicyHeader(string value) : base(value)
        {
            Value = value;
        }

        /// <inheritdoc />
        public override string Header { get; } = "Referrer-Policy";
    }
}
