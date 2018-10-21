namespace NetEscapades.AspNetCore.SecurityHeaders.Headers.FeaturePolicy
{
    /// <summary>
    /// Base class for building Feature-Policy directives.
    /// </summary>
    public abstract class FeaturePolicyDirectiveBuilderBase
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="FeaturePolicyDirectiveBuilderBase"/> class.
        /// </summary>
        /// <param name="directive">The name of the directive.</param>
        public FeaturePolicyDirectiveBuilderBase(string directive)
        {
            Directive = directive;
        }

        /// <summary>
        /// The name of the directive.
        /// </summary>
        internal string Directive { get; }

        /// <summary>
        /// Builds the complete directive policy string.
        /// </summary>
        /// <returns>The complete directive string.</returns>
        internal abstract string Build();
    }
}