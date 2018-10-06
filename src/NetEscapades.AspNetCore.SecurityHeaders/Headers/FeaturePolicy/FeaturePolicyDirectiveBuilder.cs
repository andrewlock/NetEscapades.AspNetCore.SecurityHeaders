using System;
using System.Collections.Generic;

namespace NetEscapades.AspNetCore.SecurityHeaders.Infrastructure
{
    public class FeaturePolicyDirectiveBuilder : FeaturePolicyDirectiveBuilderBase
    {
        public FeaturePolicyDirectiveBuilder(string directive) : base(directive) { }

        public List<string> Sources { get; } = new List<string>();
        internal bool BlockResources { get; set; } = false;
        internal bool AllowAllResources { get; set; } = false;

        internal override string Build()
        {
            if(BlockResources && AllowAllResources)
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
            if (string.IsNullOrEmpty(value)) { return string.Empty; }
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