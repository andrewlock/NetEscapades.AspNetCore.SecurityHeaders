namespace NetEscapades.AspNetCore.SecurityHeaders.Infrastructure
{
    public static class FeaturePolicyDirectiveBuilderExtensions
    {
        /// <summary>
        /// Enable the feature in top-level browsing contexts and all nested browsing contexts (iframes).
        /// </summary>
        /// <returns>The Feature-Policy builder for method chaining</returns>
        public static T EnableForAll<T>(this T builder) where T: FeaturePolicyDirectiveBuilder
        {
            builder.AllowAllResources = true;
            return builder;
        }

        /// <summary>
        /// Enable the feature in top-level browsing contexts and all nested browsing contexts (iframes) in the same origin. ]
        /// The feature is not allowed in cross-origin documents in nested browsing contexts.
        /// </summary>
        /// <returns>The Feature-Policy builder for method chaining</returns>
        public static T EnableForSelf<T>(this T builder) where T : FeaturePolicyDirectiveBuilder
        {
            builder.Sources.Add("'self'");
            return builder;
        }

        /// <summary>
        /// The feature is disabled in top-level and nested browsing contexts.
        /// </summary>
        /// <returns>The Feature-Policy builder for method chaining</returns>
        public static T EnableForNone<T>(this T builder) where T: FeaturePolicyDirectiveBuilder
        {
            builder.BlockResources = true;
            return builder;
        }

        /// <summary>
        /// Enable the feature for the specific origin specified in <paramref name="uri"/>. May be any non-empty value
        /// </summary>
        /// <param name="builder">The builder instance</param>
        /// <param name="uri">The URI to allow.</param>
        /// <returns>The Feature-Policy builder for method chaining</returns>
        public static T EnableFor<T>(this T builder, string uri) where T: FeaturePolicyDirectiveBuilder
        {
            if (string.IsNullOrWhiteSpace(uri))
            {
                throw new System.ArgumentException("Uri may not be null or empty", nameof(uri));
            }

            builder.Sources.Add(uri);
            return builder;
        }
    }
}