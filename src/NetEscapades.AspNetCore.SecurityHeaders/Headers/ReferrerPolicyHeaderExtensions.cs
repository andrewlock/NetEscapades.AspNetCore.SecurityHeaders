using NetEscapades.AspNetCore.SecurityHeaders.Headers;

// ReSharper disable once CheckNamespace
namespace Microsoft.AspNetCore.Builder
{
    /// <summary>
    /// Extension methods for adding a <see cref="ReferrerPolicyHeader" /> to a <see cref="HeaderPolicyCollection" />
    /// </summary>
    public static class ReferrerPolicyHeaderExtensions
    {
        /// <summary>
        /// Indicates that the site doesn't want to set a Referrer Policy
        /// here and the browser should fallback to a Referrer Policy defined
        /// via other mechanisms elsewhere
        /// </summary>
        /// <param name="policies">The collection of policies</param>
        /// <returns>The <see cref="HeaderPolicyCollection"/> for method chaining</returns>
        public static HeaderPolicyCollection AddReferrerPolicyNone(this HeaderPolicyCollection policies)
        {
            return policies.ApplyPolicy(new ReferrerPolicyHeader(string.Empty));
        }

        /// <summary>
        /// Instructs the browser to never send the referrer header with requests
        /// that are made from your site. This also include links to pages on your own site.
        /// </summary>
        /// <param name="policies">The collection of policies</param>
        /// <returns>The <see cref="HeaderPolicyCollection"/> for method chaining</returns>
        public static HeaderPolicyCollection AddReferrerPolicyNoReferrer(this HeaderPolicyCollection policies)
        {
            return policies.ApplyPolicy(new ReferrerPolicyHeader("no-referrer"));
        }

        /// <summary>
        /// The browser will not send the referrer header when navigating from HTTPS to HTTP,
        /// but will always send the full URL in the referrer header when navigating
        /// from HTTP to any origin.
        /// </summary>
        /// <param name="policies">The collection of policies</param>
        /// <returns>The <see cref="HeaderPolicyCollection"/> for method chaining</returns>
        public static HeaderPolicyCollection AddReferrerPolicyNoReferrerWhenDowngrade(this HeaderPolicyCollection policies)
        {
            return policies.ApplyPolicy(new ReferrerPolicyHeader("no-referrer-when-downgrade"));
        }

        /// <summary>
        /// The browser will only set the referrer header on requests to the same origin.
        /// If the destination is another origin then no referrer information will be sent.
        /// </summary>
        /// <param name="policies">The collection of policies</param>
        /// <returns>The <see cref="HeaderPolicyCollection"/> for method chaining</returns>
        public static HeaderPolicyCollection AddReferrerPolicySameOrigin(this HeaderPolicyCollection policies)
        {
            return policies.ApplyPolicy(new ReferrerPolicyHeader("same-origin"));
        }

        /// <summary>
        /// The browser will always set the referrer header to the origin from which the request was made.
        /// This will strip any path information from the referrer information.
        /// </summary>
        /// <param name="policies">The collection of policies</param>
        /// <returns>The <see cref="HeaderPolicyCollection"/> for method chaining</returns>
        public static HeaderPolicyCollection AddReferrerPolicyOrigin(this HeaderPolicyCollection policies)
        {
            return policies.ApplyPolicy(new ReferrerPolicyHeader("origin"));
        }

        /// <summary>
        /// The browser will always set the referrer header to the origin from which the request was made, as
        /// long as the destination is HTTPS, otherwise no refer will not be sent.
        /// This will strip any path information from the referrer information.
        /// </summary>
        /// <param name="policies">The collection of policies</param>
        /// <returns>The <see cref="HeaderPolicyCollection"/> for method chaining</returns>
        public static HeaderPolicyCollection AddReferrerPolicyStrictOrigin(this HeaderPolicyCollection policies)
        {
            return policies.ApplyPolicy(new ReferrerPolicyHeader("strict-origin"));
        }

        /// <summary>
        /// The browser will send the full URL to requests to the same origin but
        /// only send the origin when requests are cross-origin.
        /// </summary>
        /// <param name="policies">The collection of policies</param>
        /// <returns>The <see cref="HeaderPolicyCollection"/> for method chaining</returns>
        public static HeaderPolicyCollection AddReferrerPolicyOriginWhenCrossOrigin(this HeaderPolicyCollection policies)
        {
            return policies.ApplyPolicy(new ReferrerPolicyHeader("origin-when-cross-origin"));
        }

        /// <summary>
        /// The browser will send the full URL to requests to the same origin but
        /// only send the origin when requests are cross-origin, as long as a scheme
        /// downgrade has not happened (i.e. you are not moving from HTTPS to HTTP)
        /// </summary>
        /// <param name="policies">The collection of policies</param>
        /// <returns>The <see cref="HeaderPolicyCollection"/> for method chaining</returns>
        public static HeaderPolicyCollection AddReferrerPolicyStrictOriginWhenCrossOrigin(this HeaderPolicyCollection policies)
        {
            return policies.ApplyPolicy(new ReferrerPolicyHeader("strict-origin-when-cross-origin"));
        }

        /// <summary>
        /// The browser will always send the full URL with any request to any origin.
        /// </summary>
        /// <param name="policies">The collection of policies</param>
        /// <returns>The <see cref="HeaderPolicyCollection"/> for method chaining</returns>
        public static HeaderPolicyCollection AddReferrerPolicyUnsafeUrl(this HeaderPolicyCollection policies)
        {
            return policies.ApplyPolicy(new ReferrerPolicyHeader("unsafe-url"));
        }
    }
}