using Microsoft.AspNetCore.Http;

namespace NetEscapades.AspNetCore.SecurityHeaders.Headers
{
    /// <summary>
    /// The header value to use for X-Frame-Options
    /// </summary>
    public class XFrameOptionsHeader : DocumentHeaderPolicyBase
    {
        private readonly string _value;

        /// <summary>
        /// Initializes a new instance of the <see cref="XFrameOptionsHeader"/> class.
        /// </summary>
        /// <param name="value">The HTTP header value</param>
        public XFrameOptionsHeader(string value)
        {
            _value = value;
        }

        /// <inheritdoc />
        public override string Header { get; } = "X-Frame-Options";

        /// <inheritdoc />
        protected override string GetValue(HttpContext context) => _value;
    }
}