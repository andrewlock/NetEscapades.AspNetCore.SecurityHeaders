using System;
using System.Collections.Generic;

namespace NetEscapades.AspNetCore.SecurityHeaders.Infrastructure
{
    /// <summary>
    /// Used to build a Feature-Policy directive that uses a standard set of sources.
    /// </summary>
    public class FeaturePolicyDirectiveBuilder : FeaturePolicyDirectiveBuilderBase
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="FeaturePolicyDirectiveBuilder"/> class.
        /// </summary>
        /// <param name="directive">The name of the directive.</param>
        public FeaturePolicyDirectiveBuilder(string directive) : base(directive)
        {
        }

        /// <summary>
        /// The sources from which the directive is allowed.
        /// </summary>
        public List<string> Sources { get; } = new List<string>();

        /// <summary>
        /// If true, no sources are allowed.
        /// </summary>
        internal bool BlockResources { get; set; } = false;

        /// <summary>
        /// If true, all sources are allowed.
        /// </summary>
        internal bool AllowAllResources { get; set; } = false;

        /// <inheritdoc />
        internal override string Build()
        {
            if (BlockResources && AllowAllResources)
            {
                throw new InvalidOperationException(
                    $"Invalid directive values for Feature-Policy '{Directive}' directive: you cannot both block all resources and allow all values");
            }

            if (BlockResources)
            {
                return GetPolicy("'none'");
            }

            if (AllowAllResources)
            {
                return GetPolicy("*");
            }

            return GetPolicy(string.Join(" ", Sources));
        }

        private string GetPolicy(string value)
        {
            if (string.IsNullOrEmpty(value))
            {
                return string.Empty;
            }

            return $"{Directive} {value}";
        }

        /// <summary>
        /// Enable the feature in top-level browsing contexts and all nested browsing contexts (iframes).
        /// </summary>
        /// <returns>The Feature-Policy builder for method chaining</returns>
        public FeaturePolicyDirectiveBuilder All()
        {
            AllowAllResources = true;
            return this;
        }

        /// <summary>
        /// Enable the feature in top-level browsing contexts and all nested browsing contexts (iframes) in the same origin. ]
        /// The feature is not allowed in cross-origin documents in nested browsing contexts.
        /// </summary>
        /// <returns>The Feature-Policy builder for method chaining</returns>
        public FeaturePolicyDirectiveBuilder Self()
        {
            Sources.Add("'self'");
            return this;
        }

        /// <summary>
        /// The feature is disabled in top-level and nested browsing contexts.
        /// </summary>
        /// <returns>The Feature-Policy builder for method chaining</returns>
        public FeaturePolicyDirectiveBuilder None()
        {
            BlockResources = true;
            return this;
        }

        /// <summary>
        /// Enable the feature for the specific origin specified in <paramref name="uri"/>. May be any non-empty value
        /// </summary>
        /// <param name="uri">The URI to allow.</param>
        /// <returns>The Feature-Policy builder for method chaining</returns>
        public FeaturePolicyDirectiveBuilder For(string uri)
        {
            if (string.IsNullOrWhiteSpace(uri))
            {
                throw new System.ArgumentException("Uri may not be null or empty", nameof(uri));
            }

            Sources.Add(uri);
            return this;
        }
    }
}