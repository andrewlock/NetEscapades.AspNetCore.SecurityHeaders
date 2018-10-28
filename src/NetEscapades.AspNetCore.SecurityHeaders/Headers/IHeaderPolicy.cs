using Microsoft.AspNetCore.Http;
using NetEscapades.AspNetCore.SecurityHeaders.Infrastructure;

namespace NetEscapades.AspNetCore.SecurityHeaders.Headers
{
    /// <summary>
    /// An interface that defines a custom header
    /// </summary>
    public interface IHeaderPolicy
    {
        /// <summary>
        /// The header value of the policy
        /// </summary>
        /// <returns>The HTTP header name</returns>
        string Header { get; }

        /// <summary>
        /// Apply the header to the result given the provided context
        /// </summary>
        /// <param name="context">The <see cref="HttpContext"/> associated with the current call.</param>
        /// <param name="result">The <see cref="CustomHeadersResult"/> to update.</param>
        void Apply(HttpContext context, CustomHeadersResult result);
    }
}