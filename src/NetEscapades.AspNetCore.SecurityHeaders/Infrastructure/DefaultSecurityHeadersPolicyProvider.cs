using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;

namespace NetEscapades.AspNetCore.SecurityHeaders.Infrastructure
{
    /// <inheritdoc />
    public class DefaultSecurityHeadersPolicyProvider : ISecurityHeadersPolicyProvider
    {
        private readonly SecurityHeadersOptions _options;

        /// <summary>
        /// Initializes a new instance of the <see cref="DefaultSecurityHeadersPolicyProvider"/> class.
        /// </summary>
        /// <param name="options">The options configured for the application.</param>
        public DefaultSecurityHeadersPolicyProvider(IOptions<SecurityHeadersOptions> options)
        {
            _options = options.Value;
        }

        /// <inheritdoc />
        public Task<HeaderPolicyCollection> GetPolicyAsync(HttpContext context, string policyName)
        {
            if (context == null)
            {
                throw new ArgumentNullException(nameof(context));
            }

            return Task.FromResult(_options.GetPolicy(policyName ?? _options.DefaultPolicyName));
        }
    }
}
