using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;

namespace NetEscapades.AspNetCore.SecurityHeaders.Infrastructure
{
    /// <summary>
    /// A type which can provide a <see cref="SecurityHeadersPolicy"/> for a particular <see cref="HttpContext"/>.
    /// </summary>
    public class DefaultSecurityHeadersPolicyProvider : ISecurityHeadersPolicyProvider
    {
        private readonly SecurityHeadersOptions _options;

        /// <summary>
        /// Creates a new instance of the <see cref="SecurityHeadersOptions"/>
        /// </summary>
        /// <param name="options">The option model representing <see cref="SecurityHeadersOptions"/></param>
        public DefaultSecurityHeadersPolicyProvider(IOptions<SecurityHeadersOptions> options)
        {
            _options = options.Value;
        }

        /// <summary>
        /// Gets a <see cref="SecurityHeadersPolicy"/> from the given <paramref name="context"/>
        /// </summary>
        /// <param name="context">The <see cref="HttpContext"/> associated with this call.</param>
        /// <param name="policyName">An optional policy name to look for.</param>
        /// <returns>A <see cref="SecurityHeadersPolicy"/></returns>
        public Task<SecurityHeadersPolicy> GetPolicyAsync(HttpContext context, string policyName)
        {
            if (context == null)
            {
                throw new ArgumentNullException(nameof(context));
            }

            return Task.FromResult(_options.GetPolicy(policyName ?? _options.DefaultPolicyName));
        }
    }
}
