using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using NetEscapades.AspNetCore.SecurityHeaders.Infrastructure;

namespace NetEscapades.AspNetCore.SecurityHeaders.Test.Mocks
{
    /// <summary>
    /// If policy name is null, automatically select policy depending on request path.
    /// </summary>
    public class CustomSecurityHeadersPolicyProvider : ISecurityHeadersPolicyProvider
    {
        private readonly SecurityHeadersOptions _options;

        /// <summary>
        /// Initializes a new instance of the <see cref="CustomSecurityHeadersPolicyProvider"/> class.
        /// </summary>
        /// <param name="options">The options configured for the application.</param>
        public CustomSecurityHeadersPolicyProvider(IOptions<SecurityHeadersOptions> options)
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

            if (policyName == null && context.Request.Path == "/signout-oidc")
            {
                policyName = "AllowFrame";
            }

            return Task.FromResult(_options.GetPolicy(policyName ?? _options.DefaultPolicyName));
        }
    }
}
