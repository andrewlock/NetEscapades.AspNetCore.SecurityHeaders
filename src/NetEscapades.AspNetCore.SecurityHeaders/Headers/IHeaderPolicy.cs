using Microsoft.AspNetCore.Http;

namespace NetEscapades.AspNetCore.SecurityHeaders.Infrastructure
{
    public interface IHeaderPolicy
    {
        /// <summary>
        /// The header value of the policy
        /// </summary>
        /// <returns></returns>
        string Header { get; }

        /// <summary>
        /// The value to set for the header
        /// </summary>
        /// <returns></returns>
        string Value { get; set; }

        /// <summary>
        /// Apply the header to the result given the provided context  
        /// </summary>
        /// <param name="context">The <see cref="HttpContext"/> associated with the current call.</param>
        /// <param name="result">The <see cref="CustomHeadersResult"/> to update.</param>
        void Apply(HttpContext context, CustomHeadersResult result);
    }
}