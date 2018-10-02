using System;

namespace NetEscapades.AspNetCore.SecurityHeaders.Infrastructure
{
    /// <summary>
    /// The header value to use for ReferrerPolicy
    /// </summary>
    public class ReferrerPolicyHeader : HtmlOnlyHeaderPolicyBase
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ReferrerPolicyHeader"/> class.
        /// </summary>
        /// <param name="value">The HTTP header value</param>
        public ReferrerPolicyHeader(string value) : base(value)
        {
        }

        /// <inheritdoc />
        public override string Header { get; } = "Referrer-Policy";
    }
}
