using Microsoft.AspNetCore.Http;

namespace NetEscapades.AspNetCore.SecurityHeaders.Infrastructure
{
    /// <summary>
    /// A type which can evaluate a policy for a particular <see cref="HttpContext"/>.
    /// </summary>
    public interface ICustomHeaderService
    {
        /// <summary>
        /// Evaluates the given <paramref name="policies"/> using the passed in <paramref name="context"/>.
        /// </summary>
        /// <param name="context">The <see cref="HttpContext"/> associated with the call.</param>
        /// <param name="policies">The <see cref="HeaderPolicyCollection"/> containing policies to be evaluated.</param>
        /// <returns>A <see cref="CustomHeadersResult"/> which contains the result of policy evaluation and can be
        /// used by the caller to set appropriate response headers.</returns>
        CustomHeadersResult EvaluatePolicy(HttpContext context, HeaderPolicyCollection policies);

        /// <summary>
        /// Adds and removes the required headers to the given <paramref name="response"/>.
        /// </summary>
        /// <param name="response">The <see cref="HttpResponse"/> associated with the current call.</param>
        /// <param name="result">The <see cref="CustomHeadersResult"/> used to read the allowed values.</param>
        void ApplyResult(HttpResponse response, CustomHeadersResult result);
    }
}