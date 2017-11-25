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

        /// <inheritdoc />
        public static XFrameOptionsHeader Deny()
        {
            return new XFrameOptionsHeader("DENY");
        }

        /// <inheritdoc />
        public static XFrameOptionsHeader SameOrigin()
        {
            return new XFrameOptionsHeader("SAMEORIGIN");
        }

        /// <inheritdoc />
        public static XFrameOptionsHeader AllowFromUri(string uri)
        {
            return new XFrameOptionsHeader($"ALLOW-FROM {uri}");
        }
    }
}