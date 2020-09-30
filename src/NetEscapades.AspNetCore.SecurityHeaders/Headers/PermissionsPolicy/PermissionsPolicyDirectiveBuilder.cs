using System;
using System.Collections.Generic;
using System.Linq;

namespace NetEscapades.AspNetCore.SecurityHeaders.Headers.PermissionsPolicy
{
    /// <summary>
    /// Used to build a Permissions-Policy directive that uses a standard set of sources.
    /// </summary>
    public abstract class PermissionsPolicyDirectiveBuilder : PermissionsPolicyDirectiveBuilderBase
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="PermissionsPolicyDirectiveBuilder"/> class.
        /// </summary>
        /// <param name="directive">The name of the directive.</param>
        public PermissionsPolicyDirectiveBuilder(string directive) : base(directive)
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
                    $"Invalid directive values for Permissions-Policy '{Directive}' directive: you cannot both block all resources and allow all values");
            }

            if (BlockResources)
            {
                return $"{Directive}=()";
            }

            if (AllowAllResources)
            {
                return $"{Directive}=*";
            }

            if (Sources.Count == 1)
            {
                return $"{Directive}={Sources.FirstOrDefault()}";
            }

            return $"{Directive}=({string.Join(" ", Sources)})";
        }

        /// <summary>
        /// Enable the feature in top-level browsing contexts and all nested browsing contexts (iframes).
        /// </summary>
        /// <returns>The Permissions-Policy builder for method chaining</returns>
        public PermissionsPolicyDirectiveBuilder All()
        {
            AllowAllResources = true;
            return this;
        }

        /// <summary>
        /// Enable the feature in top-level browsing contexts and all nested browsing contexts (iframes) in the same origin. ]
        /// The feature is not allowed in cross-origin documents in nested browsing contexts.
        /// </summary>
        /// <returns>The Permissions-Policy builder for method chaining</returns>
        public PermissionsPolicyDirectiveBuilder Self()
        {
            Sources.Add("self");
            return this;
        }

        /// <summary>
        /// The feature is disabled in top-level and nested browsing contexts.
        /// </summary>
        /// <returns>The Permissions-Policy builder for method chaining</returns>
        public PermissionsPolicyDirectiveBuilder None()
        {
            BlockResources = true;
            return this;
        }

        /// <summary>
        /// Enable the feature for the specific origin specified in <paramref name="uri"/>. May be any non-empty value
        /// </summary>
        /// <param name="uri">The URI to allow.</param>
        /// <returns>The Permissions-Policy builder for method chaining</returns>
        public PermissionsPolicyDirectiveBuilder For(string uri)
        {
            if (string.IsNullOrWhiteSpace(uri))
            {
                throw new ArgumentException("Uri may not be null or empty", nameof(uri));
            }

            Sources.Add($"\"{uri}\"");
            return this;
        }
    }
}
