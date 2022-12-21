using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using NetEscapades.AspNetCore.SecurityHeaders.Infrastructure;

namespace NetEscapades.AspNetCore.SecurityHeaders.Headers;

/// <summary>
/// A base implementation of <see cref="IHeaderPolicy" /> that only
/// adds headers for specific content types.
/// </summary>
public abstract class DocumentHeaderPolicyBase : HeaderPolicyBase, IDocumentHeaderPolicy
{
    /// <inheritdoc />
    public override void Apply(HttpContext context, CustomHeadersResult result)
    {
        // This method won't be called, as IDocumentHeaderPolicy is preferred
        throw new NotImplementedException();
    }

    /// <inheritdoc />
    public void Apply(HttpContext context, CustomHeadersResult result, HeaderPolicyCollection policies)
    {
        if (context == null)
        {
            throw new ArgumentNullException(nameof(context));
        }

        var request = context.Request;
        var contentTypePrefixes = policies.DocumentHeaderContentTypePrefixes;
        if (contentTypePrefixes is null || IsMatch(context, contentTypePrefixes))
        {
            if (request.IsHttps)
            {
                EvaluateHttpsRequest(context, result);
            }
            else
            {
                EvaluateHttpRequest(context, result);
            }
        }
    }

    private static bool IsMatch(HttpContext context, string[] contentTypes)
    {
        foreach (var contentTypePrefix in contentTypes)
        {
            if (context.Response.ContentType?.StartsWith(contentTypePrefix) ?? true)
            {
                return true;
            }
        }

        return false;
    }
}