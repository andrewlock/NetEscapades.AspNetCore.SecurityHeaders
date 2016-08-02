using System;
using System.Collections.Generic;

namespace NetEscapades.AspNetCore.SecurityHeaders.Infrastructure
{
    /// <summary>
    /// Provides programmatic configuration for Security Headers.
    /// </summary>
    public class CustomHeaderOptions
    {
        private string _defaultPolicyName = "__DefaultSecurityHeadersPolicy";

        /// <summary>
        /// The collections of policies to apply
        /// </summary>
        /// <returns></returns>
        public Dictionary<string, HeaderPolicyCollection> PolicyCollections { get; } =
            new Dictionary<string, HeaderPolicyCollection>();

        /// <summary>
        /// The name of the default policy
        /// </summary>
        /// <returns></returns>
        public string DefaultPolicyName
        {
            get
            {
                return _defaultPolicyName;
            }
            set
            {
                if (value == null)
                {
                    throw new ArgumentNullException(nameof(value));
                }

                _defaultPolicyName = value;
            }
        }

        /// <summary>
        /// Gets the policy based on the <paramref name="name"/>
        /// </summary>
        /// <param name="name">The name of the policy to lookup.</param>
        /// <returns>The <see cref="HeaderPolicyCollection"/> if the policy was added.<c>null</c> otherwise.</returns>
        public HeaderPolicyCollection GetPolicy(string name)
        {
            if (name == null)
            {
                throw new ArgumentNullException(nameof(name));
            }

            return PolicyCollections.ContainsKey(name) ? PolicyCollections[name] : null;
        }
    }
}
