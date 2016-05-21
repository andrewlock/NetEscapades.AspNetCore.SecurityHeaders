using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace NetEscapades.AspNetCore.SecurityHeaders.Infrastructure
{
    /// <summary>
    /// A type which can provide a <see cref="SecurityHeadersPolicy"/> for a particular <see cref="HttpContext"/>.
    /// </summary>
    public interface ISecurityHeadersPolicyProvider
    {
        /// <summary>
        /// Gets a <see cref="SecurityHeadersPolicy"/> from the given <paramref name="context"/>
        /// </summary>
        /// <param name="context">The <see cref="HttpContext"/> associated with this call.</param>
        /// <param name="policyName">An optional policy name to look for.</param>
        /// <returns>A <see cref="SecurityHeadersPolicy"/></returns>
        Task<SecurityHeadersPolicy> GetPolicyAsync(HttpContext context, string policyName);
    }
}