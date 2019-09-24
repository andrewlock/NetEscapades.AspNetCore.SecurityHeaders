using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Builder;

namespace NetEscapades.AspNetCore.SecurityHeaders.Infrastructure
{
    /// <summary>
    /// Provides programmatic configuration for Security Headers.
    /// </summary>
    [Obsolete("This class is unused since v0.5.0, and will be removed in a future version of the package")]
    public class CustomHeaderOptions
    {
        private string _defaultPolicyName = "__DefaultSecurityHeadersPolicy";

        /// <summary>
        /// The collections of policies to apply
        /// </summary>
        /// <returns>The collection of policies, indexed by header name</returns>
        public Dictionary<string, HeaderPolicyCollection> PolicyCollections { get; } =
            new Dictionary<string, HeaderPolicyCollection>();

        /// <summary>
        /// Gets or sets the name of the default policy
        /// </summary>
        /// <returns>The name of the default policy</returns>
        public string DefaultPolicyName
        {
            get => _defaultPolicyName;
            set => _defaultPolicyName = value ?? throw new ArgumentNullException(nameof(value));
        }

        /// <summary>
        /// Gets the policy based on the <paramref name="name"/>
        /// </summary>
        /// <param name="name">The name of the policy to lookup.</param>
        /// <returns>The <see cref="HeaderPolicyCollection"/> if the policy was added.<c>null</c> otherwise.</returns>
        public HeaderPolicyCollection? GetPolicy(string name)
        {
            if (name == null)
            {
                throw new ArgumentNullException(nameof(name));
            }

            return PolicyCollections.ContainsKey(name) ? PolicyCollections[name] : null;
        }
    }
}
