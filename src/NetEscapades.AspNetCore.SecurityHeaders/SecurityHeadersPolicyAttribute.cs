using System;

namespace NetEscapades.AspNetCore.SecurityHeaders;

/// <summary>
/// Adds a security headers policy with the provided policy name to the endpoint(s).
/// </summary>
/// <param name="policyName">The security headers policy to use.</param>
public class SecurityHeadersPolicyAttribute(string policyName) : Attribute, ISecurityHeadersPolicyMetadata
{
    /// <inheritdoc/>
    public string PolicyName { get; } = policyName;
}