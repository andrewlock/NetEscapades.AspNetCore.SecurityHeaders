using System;
using NetEscapades.AspNetCore.SecurityHeaders.Headers;

// ReSharper disable once CheckNamespace
namespace Microsoft.AspNetCore.Builder;

/// <summary>
/// Extension methods for adding a <see cref="PermissionsPolicyHeader" /> to a <see cref="HeaderPolicyCollection" />
/// </summary>
public static class PermissionsPolicyHeaderExtensions
{
    /// <summary>
    /// The policy applied by <see cref="AddPermissionsPolicyWithDefaultSecureDirectives"/>
    /// </summary>
    internal const string DefaultSecurePolicy =
        "accelerometer=(), ambient-light-sensor=(), autoplay=(), camera=(), display-capture=(), " +
        "encrypted-media=(), fullscreen=(), geolocation=(), gyroscope=(), magnetometer=(), " +
        "microphone=(), midi=(), payment=(), picture-in-picture=(), publickey-credentials-get=(), " +
        "screen-wake-lock=(), sync-xhr=(), usb=(), web-share=(), xr-spatial-tracking=()";

    /// <summary>
    /// Add a Permissions-Policy header to all requests
    /// </summary>
    /// <param name="policies">The collection of policies</param>
    /// <param name="configure">Configure the Permissions-Policy</param>
    /// <returns>The <see cref="HeaderPolicyCollection"/> for method chaining</returns>
    public static HeaderPolicyCollection AddPermissionsPolicy(this HeaderPolicyCollection policies, Action<PermissionsPolicyBuilder> configure)
    {
        return policies.ApplyPolicy(PermissionsPolicyHeader.Build(configure));
    }

    /// <summary>
    /// Add a Permissions-Policy with recommended "secure" directives based on
    /// <see href="https://cheatsheetseries.owasp.org/cheatsheets/REST_Security_Cheat_Sheet.html#security-headers">OWASP recommendations</see>.
    /// Note that this disables many features. If you need to enable some features, consider calling <see cref="AddPermissionsPolicy"/>
    /// and <see cref="PermissionsPolicyBuilder.AddDefaultSecureDirectives"/>, and overriding one or more policies.
    /// </summary>
    /// <param name="policies">The collection of policies</param>
    /// <returns>The <see cref="HeaderPolicyCollection"/> for method chaining</returns>
    /// <remarks>The OWASP recommended policy includes directives that are either experimental,
    /// not available by default, or not implemented. For consistency with <see cref="PermissionsPolicyBuilder"/>,
    /// those directives are not included in the policy.
    ///
    /// The policy added is equivalent to <c>accelerometer=(), ambient-light-sensor=(),
    /// autoplay=(), camera=(), display-capture=(), encrypted-media=(), fullscreen=(),
    /// geolocation=(), gyroscope=(), magnetometer=(), microphone=(), midi=(), payment=(),
    /// picture-in-picture=(), publickey-credentials-get=(), screen-wake-lock=(), sync-xhr=(),
    /// usb=(), web-share=(), xr-spatial-tracking=()</c></remarks>
    public static HeaderPolicyCollection AddPermissionsPolicyWithDefaultSecureDirectives(this HeaderPolicyCollection policies)
    {
        return policies.ApplyPolicy(new PermissionsPolicyHeader(DefaultSecurePolicy));
    }
}