using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using NetEscapades.AspNetCore.SecurityHeaders.Infrastructure;

namespace NetEscapades.AspNetCore.SecurityHeaders.Headers;

/// <summary>
/// A header policy that represents a document-based header, which only needs
/// to be applied to a sub set of requests
/// </summary>
internal interface IDocumentHeaderPolicy : IHeaderPolicy
{
    /// <summary>
    /// Apply the header to the result given the provided context
    /// </summary>
    /// <param name="context">The <see cref="HttpContext"/> associated with the current call.</param>
    /// <param name="result">The <see cref="CustomHeadersResult"/> to update.</param>
    /// <param name="policies">The <see cref="HeaderPolicyCollection"/> applied to the request</param>
    void Apply(HttpContext context, CustomHeadersResult result, HeaderPolicyCollection policies);
}