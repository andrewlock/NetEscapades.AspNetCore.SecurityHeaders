// ReSharper disable once CheckNamespace
using NetEscapades.AspNetCore.SecurityHeaders.Headers.CrossOriginPolicies.ResourcePolicy;

namespace Microsoft.AspNetCore.Builder
{
    /// <summary>
    /// Used to build a Cross Origin Resource Policy header from multiple directives.
    /// </summary>
    public class CrossOriginResourcePolicyBuilder : CrossOriginPolicyBuilder
    {
        /// <summary>
        /// More relaxed setting - only allow resources to be loaded from the same domain and sub-domains.
        /// </summary>
        /// <returns>A configured <see cref="SameSiteDirectiveBuilder"/></returns>
        public SameSiteDirectiveBuilder SameSite() => AddDirective(new SameSiteDirectiveBuilder());

        /// <summary>
        /// Most strict setting - only allow resources to be loaded from the same origin.
        /// </summary>
        /// <returns>A configured <see cref="SameOriginDirectiveBuilder"/></returns>
        public SameOriginDirectiveBuilder SameOrigin() => AddDirective(new SameOriginDirectiveBuilder());

        /// <summary>
        /// Allows resources to be loaded from other domains.
        /// </summary>
        /// <returns>A configured <see cref="CrossOriginDirectiveBuilder"/></returns>
        public CrossOriginDirectiveBuilder CrossOrigin() => AddDirective(new CrossOriginDirectiveBuilder());
    }
}