namespace NetEscapades.AspNetCore.SecurityHeaders.Infrastructure
{
    /// <summary>
    /// Extension methods for <see cref="CspDirectiveBuilder"/>s
    /// </summary>
    public static class CspDirectiveBuilderExtensions
    {
        /// <summary>
        /// Allow sources from the origin from which the protected document is being served, including the same URL scheme and port number.
        /// </summary>
        /// <typeparam name="T">A <see cref="CspDirectiveBuilder"/> to configure.</typeparam>
        /// <param name="builder">The <see cref="CspDirectiveBuilder"/> to apply the source to.</param>
        /// <returns>The CSP builder for method chaining</returns>
        public static T Self<T>(this T builder) where T : CspDirectiveBuilder
        {
            builder.Sources.Add("'self'");
            return builder;
        }

        /// <summary>
        /// Allows blob: URIs to be used as a content source
        /// </summary>
        /// <typeparam name="T">A <see cref="CspDirectiveBuilder"/> to configure.</typeparam>
        /// <param name="builder">The <see cref="CspDirectiveBuilder"/> to apply the source to.</param>
        /// <returns>The CSP builder for method chaining</returns>
        public static T Blob<T>(this T builder) where T : CspDirectiveBuilder
        {
            builder.Sources.Add("blob:");
            return builder;
        }

        /// <summary>
        /// data: Allows data: URIs to be used as a content source.
        /// WARNING: This is insecure; an attacker can also inject arbitrary data: URIs. Use this sparingly and definitely not for scripts.
        /// </summary>
        /// <typeparam name="T">A <see cref="CspDirectiveBuilder"/> to configure.</typeparam>
        /// <param name="builder">The <see cref="CspDirectiveBuilder"/> to apply the source to.</param>
        /// <returns>The CSP builder for method chaining</returns>
        public static T Data<T>(this T builder) where T : CspDirectiveBuilder
        {
            builder.Sources.Add("data:");
            return builder;
        }

        /// <summary>
        /// Block the resource (Refers to the empty set; that is, no URLs match)
        /// </summary>
        /// <typeparam name="T">A <see cref="CspDirectiveBuilder"/> to configure.</typeparam>
        /// <param name="builder">The <see cref="CspDirectiveBuilder"/> to apply the source to.</param>
        /// <returns>The CSP builder for method chaining</returns>
        public static T None<T>(this T builder) where T : CspDirectiveBuilder
        {
            builder.BlockResources = true;
            return builder;
        }

        /// <summary>
        /// Allow resources from the given <paramref name="uri"/>. May be any non-empty value
        /// </summary>
        /// <typeparam name="T">A <see cref="CspDirectiveBuilder"/> to configure.</typeparam>
        /// <param name="builder">The <see cref="CspDirectiveBuilder"/> to apply the source to.</param>
        /// <param name="uri">The URI to allow.</param>
        /// <returns>The CSP builder for method chaining</returns>
        public static T From<T>(this T builder, string uri) where T : CspDirectiveBuilder
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
        /// <typeparam name="T">A <see cref="CspDirectiveBuilder"/> to configure.</typeparam>
        /// <param name="builder">The <see cref="CspDirectiveBuilder"/> to apply the source to.</param>
        /// <returns>The CSP builder for method chaining</returns>
        public static T OverHttps<T>(this T builder) where T : CspDirectiveBuilder
        {
            builder.Sources.Add("https:");
            return builder;
        }

        /// <summary>
        /// A sha256, sha384 or sha512 hash of scripts or styles.
        /// Allow sources that match the provided hash.
        /// </summary>
        /// <typeparam name="T">A <see cref="CspDirectiveBuilder"/> to configure.</typeparam>
        /// <param name="builder">The <see cref="CspDirectiveBuilder"/> to apply the source to.</param>
        /// <param name="algorithm">The hash algorithm - one of sha256, sha384 or sha512 </param>
        /// <param name="hash">The hash for the source</param>
        /// <returns>The CSP builder for method chaining</returns>
        public static T WithHash<T>(this T builder, string algorithm, string hash) where T : CspDirectiveBuilder
        {
            // TODO: check hash algorithm is one of expected values
            builder.Sources.Add($"'{algorithm}-{hash}'");
            return builder;
        }

        /// <summary>
        /// A sha256 hash of scripts or styles.
        /// Allow sources that match the provided hash.
        /// </summary>
        /// <typeparam name="T">A <see cref="CspDirectiveBuilder"/> to configure.</typeparam>
        /// <param name="builder">The <see cref="CspDirectiveBuilder"/> to apply the source to.</param>
        /// <param name="hash">The hash for the source</param>
        /// <returns>The CSP builder for method chaining</returns>
        public static T WithHash256<T>(this T builder, string hash) where T : CspDirectiveBuilder
        {
            return builder.WithHash("sha256", hash);
        }

        /// <summary>
        /// A sha384 hash of scripts or styles.
        /// Allow sources that match the provided hash.
        /// </summary>
        /// <typeparam name="T">A <see cref="CspDirectiveBuilder"/> to configure.</typeparam>
        /// <param name="builder">The <see cref="CspDirectiveBuilder"/> to apply the source to.</param>
        /// <param name="hash">The hash for the source</param>
        /// <returns>The CSP builder for method chaining</returns>
        public static T WithHashSha384<T>(this T builder, string hash) where T : CspDirectiveBuilder
        {
            return builder.WithHash("sha384", hash);
        }

        /// <summary>
        /// A sha512 hash of scripts or styles.
        /// Allow sources that match the provided hash.
        /// </summary>
        /// <typeparam name="T">A <see cref="CspDirectiveBuilder"/> to configure.</typeparam>
        /// <param name="builder">The <see cref="CspDirectiveBuilder"/> to apply the source to.</param>
        /// <param name="hash">The hash for the source</param>
        /// <returns>The CSP builder for method chaining</returns>
        public static T WithHashSha512<T>(this T builder, string hash) where T : CspDirectiveBuilder
        {
            return builder.WithHash("sha512", hash);
        }

        /// <summary>
        /// Allows the use of inline resources, such as inline &lt;script&gt; elements, javascripT : URLs,
        /// inline event handlers, and inline &lt;style&gt; elements
        /// WARNING: This source is insecure - you should not use this directive if at all possible
        /// </summary>
        /// <typeparam name="T">A <see cref="CspDirectiveBuilder"/> to configure.</typeparam>
        /// <param name="builder">The <see cref="CspDirectiveBuilder"/> to apply the source to.</param>
        /// <returns>The CSP builder for method chaining</returns>
        public static T UnsafeInline<T>(this T builder) where T : CspDirectiveBuilder
        {
            builder.Sources.Add("'unsafe-inline'");
            return builder;
        }

        /// <summary>
        /// Allows the use of eval() and similar methods for creating code from strings.
        /// WARNING: This source is insecure - you should not use this directive if at all possible
        /// </summary>
        /// <typeparam name="T">A <see cref="CspDirectiveBuilder"/> to configure.</typeparam>
        /// <param name="builder">The <see cref="CspDirectiveBuilder"/> to apply the source to.</param>
        /// <returns>The CSP builder for method chaining</returns>
        public static T UnsafeEval<T>(this T builder) where T : CspDirectiveBuilder
        {
            builder.Sources.Add("'unsafe-eval'");
            return builder;
        }

        /// <summary>
        /// The strict-dynamic source expression specifies that the trust explicitly given to a
        /// script present in the markup, by accompanying it with a nonce or a hash, shall be
        /// propagated to all the scripts loaded by that root script. At the same time, any
        /// whitelist or source expressions such as 'self' or 'unsafe-inline' will be ignored.
        /// </summary>
        /// <typeparam name="T">A <see cref="CspDirectiveBuilder"/> to configure.</typeparam>
        /// <param name="builder">The <see cref="CspDirectiveBuilder"/> to apply the source to.</param>
        /// <returns>The CSP builder for method chaining</returns>
        public static T StrictDynamic<T>(this T builder) where T : CspDirectiveBuilder
        {
            builder.Sources.Add("'strict-dynamic'");
            return builder;
        }

#if !NETSTANDARD1_3
        /// <summary>
        /// A whitelist for specific inline scripts using a cryptographic nonce (number used once).
        /// The server generates a unique nonce value for each request. Specifying a nonce makes a
        /// modern browser ignore 'unsafe-inline' which could still be set for older browsers
        /// without nonce support.
        /// </summary>
        /// <typeparam name="T">A <see cref="CspDirectiveBuilder"/> to configure.</typeparam>
        /// <param name="builder">The <see cref="CspDirectiveBuilder"/> to apply the source to.</param>
        /// <returns>The CSP builder for method chaining</returns>
        public static T WithNonce<T>(this T builder) where T : CspDirectiveBuilder
        {
            // The format parameter will be replaced with the nonce for each request
            builder.IsUniquePerRequest = true;
            builder.Sources.Add("'nonce-{0}'");
            return builder;
        }
#endif
    }
}