using System;

namespace NetEscapades.AspNetCore.SecurityHeaders.Infrastructure
{
    /// <summary>
    /// The header value to use for X-Frame-Options
    /// </summary>
    public class XFrameOptionsHeader : HtmlOnlyHeaderPolicyBase
    {
        /// <inheritdoc />
        public XFrameOptionsHeader(string value) : base(value)
        {
            Value = value;
        }

        /// <inheritdoc />
        public override string Header { get; } = "X-Frame-Options";
    }
}