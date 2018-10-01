namespace NetEscapades.AspNetCore.SecurityHeaders.Infrastructure
{
    /// <summary>
    /// Controls  whether the current document is allowed to use the
    /// PaymentRequest interface. If disabled then calls to the 
    /// <code>PaymentRequest</code> constuctor will throw a <code>SecurityError</code>.
    /// </summary>
    public class PaymentFeaturePolicyDirectiveBuilder : FeaturePolicyDirectiveBuilder
    {
        public PaymentFeaturePolicyDirectiveBuilder() : base("payment")
        {
        }
    }
}