namespace NetEscapades.AspNetCore.SecurityHeaders.Infrastructure
{
    /// <summary>
    /// The header value to use for X-Content-Type-Options
    /// </summary>
    public class XContentTypeOptionsHeader : HeaderPolicyBase
    {
        /// <inheritdoc />
        public XContentTypeOptionsHeader(string value) : base(value)
        {
            Value = value;
        }

        /// <inheritdoc />
        public override string Header { get; } = "X-Content-Type-Options";

        /// <summary>
        /// Disables content sniffing
        /// </summary>
        public static XContentTypeOptionsHeader NoSniff()
        {
            return new XContentTypeOptionsHeader("nosniff");
        }
    }
}