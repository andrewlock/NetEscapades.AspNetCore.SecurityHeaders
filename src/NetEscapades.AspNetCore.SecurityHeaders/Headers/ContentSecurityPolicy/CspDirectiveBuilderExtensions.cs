namespace NetEscapades.AspNetCore.SecurityHeaders.Infrastructure
{
    public static class CspDirectiveBuilderExtensions
    { 
        /// <summary>
        /// Allow sources from the origin from which the protected document is being served, including the same URL scheme and port number
        /// </summary>
        /// <returns>The CSP builder for method chaining</returns>
        public static T Self<T>(this T builder) where T: CspDirectiveBuilder
        {
            builder.Sources.Add("'self'");
            return builder;
        }

        /// <summary>
        /// Allows blob: URIs to be used as a content source
        /// </summary>
        /// <returns>The CSP builder for method chaining</returns>
        public static T Blob<T>(this T builder) where T: CspDirectiveBuilder
        {
            builder.Sources.Add("blob:");
            return builder;
        }

        /// <summary>
        /// data: Allows data: URIs to be used as a content source. 
        /// WARNING: This is insecure; an attacker can also inject arbitrary data: URIs. Use this sparingly and definitely not for scripts.
        /// </summary>
        /// <returns>The CSP builder for method chaining</returns>
        public static T Data<T>(this T builder) where T: CspDirectiveBuilder
        {
            builder.Sources.Add("data:");
            return builder;
        }

        /// <summary>
        /// Block the resource (Refers to the empty set; that is, no URLs match)
        /// </summary>
        /// <returns>The CSP builder for method chaining</returns>
        public static T None<T>(this T builder) where T: CspDirectiveBuilder
        {
            builder.BlockResources = true;
            return builder;
        }

        /// <summary>
        /// Allow resources from the given <paramref name="uri"/>. May be any non-empty value
        /// </summary>
        /// <param name="builder">The builder instance</param>
        /// <param name="uri">The URI to allow.</param>
        /// <returns>The CSP builder for method chaining</returns>
        public static T From<T>(this T builder, string uri) where T: CspDirectiveBuilder
        {
            if (string.IsNullOrWhiteSpace(uri))
            {
                throw new System.ArgumentException("Uri may not be null or empty", nameof(uri));
            }

            builder.Sources.Add(uri);
            return builder;
        }

        /// <summary>
        /// Allow resources served over https
        /// </summary>
        /// <returns>The CSP builder for method chaining</returns>
        public static T OverHttps<T>(this T builder) where T: CspDirectiveBuilder
        {
            builder.Sources.Add("https:");
            return builder;
        }

        /// <summary>
        /// A sha256, sha384 or sha512 hash of scripts or styles. 
        /// Allow sources that match the provided hash.
        /// </summary>
        /// <param name="builder">The builder instance</param>
        /// <param name="algorithm">The hash algorithm - one of sha256, sha384 or sha512 </param>
        /// <param name="hash">The hash for the source</param>
        /// <returns>The CSP builder for method chaining</returns>
        public static T WithHash<T>(this T builder, string algorithm, string hash) where T: CspDirectiveBuilder
        {
            //TODO: check hash algorithm is one of expected values
            builder.Sources.Add($"'{algorithm}-{hash}'");
            return builder;
        }

        /// <summary>
        /// A sha256 hash of scripts or styles. 
        /// Allow sources that match the provided hash.
        /// </summary>
        /// <param name="builder">The builder instance</param>
        /// <param name="hash">The hash for the source</param>
        /// <returns>The CSP builder for method chaining</returns>
        public static T WithHash256<T>(this T builder, string hash) where T: CspDirectiveBuilder
        {
            return builder.WithHash("sha256", hash);
        }

        /// <summary>
        /// A sha384 hash of scripts or styles. 
        /// Allow sources that match the provided hash.
        /// </summary>
        /// <param name="builder">The builder instance</param>
        /// <param name="hash">The hash for the source</param>
        /// <returns>The CSP builder for method chaining</returns>
        public static T WithHashSha384<T>(this T builder, string hash) where T: CspDirectiveBuilder
        {
            return builder.WithHash("sha384", hash);
        }

        /// <summary>
        /// A sha512 hash of scripts or styles. 
        /// Allow sources that match the provided hash.
        /// </summary>
        /// <param name="builder">The builder instance</param>
        /// <param name="hash">The hash for the source</param>
        /// <returns>The CSP builder for method chaining</returns>
        public static T WithHashSha512<T>(this T builder, string hash) where T: CspDirectiveBuilder
        {
            return builder.WithHash("sha512", hash);
        }
    }
}