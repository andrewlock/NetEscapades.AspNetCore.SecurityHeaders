namespace NetEscapades.AspNetCore.SecurityHeaders;

/// <summary>
/// Metadata about which policy to apply to and endpoint
/// </summary>
internal interface ISecurityHeadersPolicyMetadata
{
    /// <summary>
    /// The name of the policy to apply
    /// </summary>
    public string PolicyName { get; }
}