using Microsoft.AspNetCore.Http;

namespace NetEscapades.AspNetCore.SecurityHeaders.Infrastructure
{
    /// <summary>
    /// The header value to use for XSS-Protection
    /// </summary>
    public class XssProtectionHeader : HtmlOnlyHeaderPolicyBase
    {
        private readonly string _value;

        /// <summary>
        /// Initializes a new instance of the <see cref="XssProtectionHeader"/> class.
        /// </summary>
        /// <param name="value">The value to apply for the header</param>
        public XssProtectionHeader(string value)
        {
            _value = value;
        }

        /// <inheritdoc />
        public override string Header { get; } = "X-XSS-Protection";

        /// <inheritdoc />
        protected override string GetValue(HttpContext context) => _value;
    }
}