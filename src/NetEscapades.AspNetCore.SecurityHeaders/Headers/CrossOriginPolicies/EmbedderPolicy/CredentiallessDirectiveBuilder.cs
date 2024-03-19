using System;
using Microsoft.AspNetCore.Http;

namespace NetEscapades.AspNetCore.SecurityHeaders.Headers.CrossOriginPolicies.EmbedderPolicy;

/// <summary>
/// no-cors cross-origin requests are sent without credentials.
/// In particular, it means Cookies are omitted from the request, and ignored from the response.
/// The responses are allowed without an explicit permission via the Cross-Origin-Resource-Policy header.
/// Navigate responses behave similarly as the require-corp mode: They require Cross-Origin-Resource-Policy response header.
/// From: https://developer.mozilla.org/en-US/docs/Web/HTTP/Headers/Cross-Origin-Embedder-Policy#directives
/// </summary>
public class CredentiallessDirectiveBuilder : CrossOriginEmbedderPolicyDirectiveBuilderBase
{
    /// <summary>
    /// Initializes a new instance of the <see cref="CredentiallessDirectiveBuilder"/> class.
    /// </summary>
    public CredentiallessDirectiveBuilder() : base("credentialless")
    {
    }

    /// <inheritdoc />
    internal override Func<HttpContext, string> CreateBuilder()
    {
        return ctx => Directive;
    }
}