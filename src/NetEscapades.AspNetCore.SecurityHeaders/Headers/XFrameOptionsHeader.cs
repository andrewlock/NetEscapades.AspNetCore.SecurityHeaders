using System;

namespace NetEscapades.AspNetCore.SecurityHeaders.Infrastructure
{
    /// <summary>
    /// The header value to use for X-Frame-Options
    /// </summary>
    public class XFrameOptionsHeader : HtmlOnlyHeaderPolicyBase
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="XFrameOptionsHeader"/> class.
        /// </summary>
        /// <param name="value">The HTTP header value</param>
        public XFrameOptionsHeader(string value) : base(value)
        {
        }

        /// <inheritdoc />
        public override string Header { get; } = "X-Frame-Options";
    }
}