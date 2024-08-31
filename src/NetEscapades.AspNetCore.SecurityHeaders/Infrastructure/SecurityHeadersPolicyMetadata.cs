namespace NetEscapades.AspNetCore.SecurityHeaders;

/// <summary>
/// Creates a new instance of <see cref="SecurityHeadersPolicyMetadata"/> with the specified <paramref name="policyName"/>
/// </summary>
/// <param name="policyName">The name of the policy to apply</param>
internal class SecurityHeadersPolicyMetadata(string policyName) : ISecurityHeadersPolicyMetadata
{
    /// <inheritdoc/>
    public string PolicyName { get; } = policyName;
}