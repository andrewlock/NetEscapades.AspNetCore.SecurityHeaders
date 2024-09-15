using NetEscapades.AspNetCore.SecurityHeaders;
using NetEscapades.AspNetCore.SecurityHeaders.Headers;
using NetEscapades.AspNetCore.SecurityHeaders.Infrastructure;

// ReSharper disable once CheckNamespace
namespace Microsoft.AspNetCore.Builder;

/// <summary>
/// Utility class exposing common extension methods
/// </summary>
public static class HeaderPolicyCollectionExtensions
{
    /// <summary>
    /// Add the policy to the policy collection
    /// </summary>
    /// <param name="policies">The <see cref="HeaderPolicyCollection" /> to add the deafult security header policies too</param>
    /// <param name="policy">The policy to add to the collection</param>
    /// <returns>The <see cref="HeaderPolicyCollection" /> for method chaining</returns>
    internal static HeaderPolicyCollection ApplyPolicy(
        this HeaderPolicyCollection policies,
        IHeaderPolicy policy)
    {
        policies[policy.Header] = policy;
        return policies;
    }

    /// <summary>
    /// Creates a copy of the header collection
    /// </summary>
    /// <param name="policies">The <see cref="IReadOnlyHeaderPolicyCollection" /> to copy</param>
    /// <returns>A new <see cref="HeaderPolicyCollection"/> that is a shallow copy of the header policy</returns>
    public static HeaderPolicyCollection Copy(this IReadOnlyHeaderPolicyCollection policies)
        => new(policies);

    /// <summary>
    /// Add default headers in accordance with the most secure approach
    /// </summary>
    /// <param name="policies">The <see cref="HeaderPolicyCollection" /> to add the deafult security header policies too</param>
    /// <returns>The <see cref="HeaderPolicyCollection" /> for method chaining</returns>
    public static HeaderPolicyCollection AddDefaultSecurityHeaders(this HeaderPolicyCollection policies)
    {
        policies.AddFrameOptionsDeny();
        policies.AddContentTypeOptionsNoSniff();
        policies.AddStrictTransportSecurityMaxAge();
        policies.AddReferrerPolicyStrictOriginWhenCrossOrigin();
        policies.RemoveServerHeader();
        policies.AddContentSecurityPolicy(builder =>
        {
            builder.AddObjectSrc().None();
            builder.AddFormAction().Self();
            builder.AddFrameAncestors().None();
        });
        policies.AddCrossOriginOpenerPolicy(x => x.SameOrigin());
        return policies;
    }

    /// <summary>
    /// Add default headers in accordance with the most secure approach.
    /// A reduced set of headers is applied compared to <see cref="AddDefaultSecurityHeaders"/>,
    /// adding only those headers that make sense for APIs returning JSON rather than HTML.
    /// </summary>
    /// <remarks>Note that there are non-security headers you should also apply to your responses,
    /// such as <c>Content-Type</c>, in accordance with
    /// <see href="https://cheatsheetseries.owasp.org/cheatsheets/REST_Security_Cheat_Sheet.html#security-headers">OWASP recommendations</see>
    /// </remarks>
    /// <param name="policies">The <see cref="HeaderPolicyCollection" /> to add the default security header policies too</param>
    /// <returns>The <see cref="HeaderPolicyCollection" /> for method chaining</returns>
    public static HeaderPolicyCollection AddDefaultApiSecurityHeaders(this HeaderPolicyCollection policies)
    {
        policies.AddFrameOptionsDeny();
        policies.AddContentTypeOptionsNoSniff();
        policies.AddStrictTransportSecurityMaxAge();
        policies.RemoveServerHeader();
        policies.AddContentSecurityPolicy(builder =>
        {
            builder.AddDefaultSrc().None();
            builder.AddFrameAncestors().None();
        });

        // The following are generally not applicable, but still worth applying for safety
        policies.AddReferrerPolicyNoReferrer();
        policies.AddPermissionsPolicyWithDefaultSecureDirectives();
        return policies;
    }
}