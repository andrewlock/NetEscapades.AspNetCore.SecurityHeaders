using Microsoft.AspNetCore.Http;

namespace NetEscapades.AspNetCore.SecurityHeaders.Headers
{
    /// <summary>
    /// An arbitrary custom header class for applying header values
    /// </summary>
    public class CustomHeader : HeaderPolicyBase
    {
        private readonly string _value;

        /// <summary>
        /// Initializes a new instance of the <see cref="CustomHeader"/> class.
        /// </summary>
        /// <param name="header">The string value of the header</param>
        /// <param name="value">The value to apply for the header</param>
        public CustomHeader(string header, string value)
        {
            Header = header;
            _value = value;
        }

        /// <inheritdoc />
        public override string Header { get; }

        /// <inheritdoc />
        protected override string GetValue(HttpContext context) => _value;
    }
}