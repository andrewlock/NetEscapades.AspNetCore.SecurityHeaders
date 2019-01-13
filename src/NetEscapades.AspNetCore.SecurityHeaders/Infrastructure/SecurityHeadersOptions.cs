using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Builder;

namespace NetEscapades.AspNetCore.SecurityHeaders.Infrastructure
{
    /// <summary>
    /// Provides programmatic configuration for Security Headers.
    /// </summary>
    public class SecurityHeadersOptions
    {
        private string _defaultPolicyName = "__DefaultSecurityHeadersPolicy";

        /// <summary>
        /// The collections of policies to apply
        /// </summary>
        /// <returns>The collection of policies, indexed by header name</returns>
        private IDictionary<string, HeaderPolicyCollection> PolicyMap { get; } = new Dictionary<string, HeaderPolicyCollection>();

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
        /// Adds a new policy and sets it as the default.
        /// </summary>
        /// <param name="policy">The <see cref="HeaderPolicyCollection"/> policy to be added.</param>
        public void AddDefaultPolicy(HeaderPolicyCollection policy)
        {
            if (policy == null)
            {
                throw new ArgumentNullException(nameof(policy));
            }

            AddPolicy(DefaultPolicyName, policy);
        }

        /// <summary>
        /// Adds a new policy and sets it as the default.
        /// </summary>
        /// <param name="configurePolicy">A delegate which can use a policy builder to build a policy.</param>
        public void AddDefaultPolicy(Action<HeaderPolicyCollection> configurePolicy)
        {
            if (configurePolicy == null)
            {
                throw new ArgumentNullException(nameof(configurePolicy));
            }

            AddPolicy(DefaultPolicyName, configurePolicy);
        }

        /// <summary>
        /// Adds a new policy.
        /// </summary>
        /// <param name="name">The name of the policy.</param>
        /// <param name="policy">The <see cref="HeaderPolicyCollection"/> policy to be added.</param>
        public void AddPolicy(string name, HeaderPolicyCollection policy)
        {
            if (name == null)
            {
                throw new ArgumentNullException(nameof(name));
            }

            PolicyMap[name] = policy ?? throw new ArgumentNullException(nameof(policy));
        }

        /// <summary>
        /// Adds a new policy.
        /// </summary>
        /// <param name="name">The name of the policy.</param>
        /// <param name="configurePolicy">A delegate which can use a policy builder to build a policy.</param>
        public void AddPolicy(string name, Action<HeaderPolicyCollection> configurePolicy)
        {
            if (name == null)
            {
                throw new ArgumentNullException(nameof(name));
            }

            if (configurePolicy == null)
            {
                throw new ArgumentNullException(nameof(configurePolicy));
            }

            var policyBuilder = new HeaderPolicyCollection();
            configurePolicy(policyBuilder);
            PolicyMap[name] = policyBuilder;
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

            return PolicyMap.TryGetValue(name, out var policies) ? policies : null;
        }
    }
}
