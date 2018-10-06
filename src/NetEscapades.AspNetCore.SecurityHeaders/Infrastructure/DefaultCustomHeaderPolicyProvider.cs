using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;

namespace NetEscapades.AspNetCore.SecurityHeaders.Infrastructure
{
    /// <summary>
    /// A type which can provide a <see cref="HeaderPolicyCollection"/> for a particular <see cref="HttpContext"/>.
    /// </summary>
    [Obsolete("This class is obsolete, and will be removed in a future version of the package")]
    public class DefaultCustomHeaderPolicyProvider : ICustomHeaderPolicyProvider
    {
        private readonly CustomHeaderOptions _options;

        /// <summary>
        /// Initializes a new instance of the <see cref="DefaultCustomHeaderPolicyProvider"/> class.
        /// </summary>
        /// <param name="options">The option model representing <see cref="CustomHeaderOptions"/></param>
        public DefaultCustomHeaderPolicyProvider(IOptions<CustomHeaderOptions> options)
        {
            _options = options.Value;
        }

        /// <summary>
        /// Gets a <see cref="HeaderPolicyCollection"/> from the given <paramref name="context"/>
        /// </summary>
        /// <param name="context">The <see cref="HttpContext"/> associated with this call.</param>
        /// <param name="policyName">An optional policy name to look for.</param>
        /// <returns>A <see cref="HeaderPolicyCollection"/></returns>
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
