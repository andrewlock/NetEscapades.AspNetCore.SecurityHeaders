using System;
using System.Collections.Generic;
using System.Linq;

namespace NetEscapades.AspNetCore.SecurityHeaders.Infrastructure
{
    /// <summary>
    /// Provides programmatic configuration for Security Headers.
    /// </summary>
    public class SecurityHeadersOptions
    {
        private string _defaultPolicyName = "__DefaultSecurityHeadersPolicy";

        public Dictionary<string, SecurityHeadersPolicy> Policies { get; } =
            new Dictionary<string, SecurityHeadersPolicy>();

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
        /// <returns>The <see cref="SecurityHeadersPolicy"/> if the policy was added.<c>null</c> otherwise.</returns>
        public SecurityHeadersPolicy GetPolicy(string name)
        {
            if (name == null)
            {
                throw new ArgumentNullException(nameof(name));
            }

            return Policies.ContainsKey(name) ? Policies[name] : null;
        }
    }
}
