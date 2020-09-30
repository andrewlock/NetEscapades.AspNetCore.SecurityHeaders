namespace NetEscapades.AspNetCore.SecurityHeaders.Headers.PermissionsPolicy
{
    /// <summary>
    /// Controls  whether the current document is allowed to use the
    /// PaymentRequest interface. If disabled then calls to the
    /// <code>PaymentRequest</code> constuctor will throw a <code>SecurityError</code>.
    /// </summary>
    public class PaymentPermissionsPolicyDirectiveBuilder : PermissionsPolicyDirectiveBuilder
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="PaymentPermissionsPolicyDirectiveBuilder"/> class.
        /// </summary>
        public PaymentPermissionsPolicyDirectiveBuilder() : base("payment")
        {
        }
    }
}
