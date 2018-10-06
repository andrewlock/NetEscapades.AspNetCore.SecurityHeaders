namespace NetEscapades.AspNetCore.SecurityHeaders.Infrastructure
{
    /// <summary>
    /// The header value to use for X-Content-Type-Options
    /// </summary>
    public class XContentTypeOptionsHeader : HeaderPolicyBase
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="XContentTypeOptionsHeader"/> class.
        /// </summary>
        /// <param name="value">The value to apply for the header</param>
        public XContentTypeOptionsHeader(string value) : base(value)
        {
        }

        /// <inheritdoc />
        public override string Header { get; } = "X-Content-Type-Options";
    }
}