using Microsoft.AspNetCore.Http;

namespace NetEscapades.AspNetCore.SecurityHeaders.Headers
{
    /// <summary>
    /// The header value to use for X-Permitted-Cross-Domain-Policies
    /// </summary>
    public class PermittedCrossDomainPoliciesHeader : HtmlOnlyHeaderPolicyBase
    {
        private readonly string _value;

        /// <summary>
        /// Initializes a new instance of the <see cref="PermittedCrossDomainPoliciesHeader"/> class.
        /// </summary>
        /// <param name="value">The value to apply for the header</param>
        public PermittedCrossDomainPoliciesHeader(string value)
        {
            _value = value;
        }

        /// <inheritdoc />
        public override string Header { get; } = "X-Permitted-Cross-Domain-Policies";

        /// <inheritdoc />
        protected override string GetValue(HttpContext context) => _value;
    }
}