namespace NetEscapades.AspNetCore.SecurityHeaders.Headers.PermissionsPolicy
{
    /// <summary>
    /// Controls whether the current document is allowed to make a FLoC calculation
    /// If disabled in a document, the promise returned by <code>interestCohort()</code>
    /// must reject with a DOMException parameter.
    /// </summary>
    public class FederatedLearningOfCohortsCalculationPermissionsPolicyDirectiveBuilder : PermissionsPolicyDirectiveBuilder
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="FederatedLearningOfCohortsCalculationPermissionsPolicyDirectiveBuilder"/> class.
        /// </summary>
        public FederatedLearningOfCohortsCalculationPermissionsPolicyDirectiveBuilder() : base("interest-cohort")
        {
        }
    }
}
