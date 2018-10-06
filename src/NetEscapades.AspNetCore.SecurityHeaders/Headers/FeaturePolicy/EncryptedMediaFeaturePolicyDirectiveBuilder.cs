namespace NetEscapades.AspNetCore.SecurityHeaders.Infrastructure
{
    /// <summary>
    /// Controls whether encrypted media extensions are available. If disabled
    /// The promise returned by <code>requestMediaKeySystemAccess()</code> must 
    /// return a promise which rejects with a <code>SecurityError</code> DOMException 
    /// object as its parameter.
    /// </summary>
    public class EncryptedMediaFeaturePolicyDirectiveBuilder : FeaturePolicyDirectiveBuilder
    {
        public EncryptedMediaFeaturePolicyDirectiveBuilder() : base("encrypted-media")
        {
        }
    }
}