namespace NetEscapades.AspNetCore.SecurityHeaders.Infrastructure
{
    /// <summary>
    /// The block-all-mixed-content directive prevents loading any assets using 
    /// HTTP when the page is loaded using HTTPS.
    /// 
    /// All mixed content resource requests are blocked, including both active 
    /// and passive mixed content. This also applies to &lt;iframe&gt; documents, 
    /// ensuring the entire page is mixed content free.
    /// 
    /// The upgrade-insecure-requests directive is evaluated before block-all-mixed-content 
    /// and If the former is set, the latter is effectively a no-op. 
    /// It is recommended to set one directive or the other – not both.
    /// </summary>
    public class BlockAllMixedContentDirectiveBuilder : CspDirectiveBuilderBase
    {
        public BlockAllMixedContentDirectiveBuilder() : base("block-all-mixed-content") { }

        internal override string Build()
        {
            return Directive;
        }
    }
}