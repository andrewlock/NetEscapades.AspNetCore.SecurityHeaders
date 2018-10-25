using Microsoft.AspNetCore.Http;

namespace NetEscapades.AspNetCore.SecurityHeaders.Infrastructure
{
    /// <summary>
    /// The header value to use for X-Content-Type-Options
    /// </summary>
    public class XContentTypeOptionsHeader : HeaderPolicyBase
    {
        private readonly string _value;

        /// <summary>
        /// Initializes a new instance of the <see cref="XContentTypeOptionsHeader"/> class.
        /// </summary>
        /// <param name="value">The value to apply for the header</param>
        public XContentTypeOptionsHeader(string value)
        {
            _value = value;
        }

        /// <inheritdoc />
        public override string Header { get; } = "X-Content-Type-Options";

        /// <inheritdoc />
        protected override string GetValue(HttpContext context) => _value;
    }
}