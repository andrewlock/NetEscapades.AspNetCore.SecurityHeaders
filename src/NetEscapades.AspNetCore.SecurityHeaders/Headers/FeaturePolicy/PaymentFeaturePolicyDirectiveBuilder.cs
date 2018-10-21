namespace NetEscapades.AspNetCore.SecurityHeaders.Headers.FeaturePolicy
{
    /// <summary>
    /// Controls  whether the current document is allowed to use the
    /// PaymentRequest interface. If disabled then calls to the
    /// <code>PaymentRequest</code> constuctor will throw a <code>SecurityError</code>.
    /// </summary>
    public class PaymentFeaturePolicyDirectiveBuilder : FeaturePolicyDirectiveBuilder
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="PaymentFeaturePolicyDirectiveBuilder"/> class.
        /// </summary>
        public PaymentFeaturePolicyDirectiveBuilder() : base("payment")
        {
        }
    }
}