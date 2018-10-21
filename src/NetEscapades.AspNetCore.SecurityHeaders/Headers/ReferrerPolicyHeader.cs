using Microsoft.AspNetCore.Http;

namespace NetEscapades.AspNetCore.SecurityHeaders.Headers
{
    /// <summary>
    /// The header value to use for ReferrerPolicy
    /// </summary>
    public class ReferrerPolicyHeader : HtmlOnlyHeaderPolicyBase
    {
        private readonly string _value;

        /// <summary>
        /// Initializes a new instance of the <see cref="ReferrerPolicyHeader"/> class.
        /// </summary>
        /// <param name="value">The HTTP header value</param>
        public ReferrerPolicyHeader(string value)
        {
            _value = value;
        }

        /// <inheritdoc />
        public override string Header { get; } = "Referrer-Policy";

        /// <inheritdoc />
        protected override string GetValue(HttpContext context) => _value;
    }
}
