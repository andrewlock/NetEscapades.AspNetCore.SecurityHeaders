namespace NetEscapades.AspNetCore.SecurityHeaders.Headers.PermissionsPolicy
{
    /// <summary>
    /// Base class for building Permissions-Policy directives.
    /// </summary>
    public abstract class PermissionsPolicyDirectiveBuilderBase
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="PermissionsPolicyDirectiveBuilderBase"/> class.
        /// </summary>
        /// <param name="directive">The name of the directive.</param>
        public PermissionsPolicyDirectiveBuilderBase(string directive)
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
