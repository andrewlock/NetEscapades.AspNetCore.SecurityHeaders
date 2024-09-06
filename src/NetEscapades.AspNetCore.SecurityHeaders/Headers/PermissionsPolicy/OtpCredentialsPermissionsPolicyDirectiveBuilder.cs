namespace NetEscapades.AspNetCore.SecurityHeaders.Headers.PermissionsPolicy;

/// <summary>
/// The HTTP Permissions-Policy header otp-credentials directive controls whether
/// the current document is allowed to use the <code>WebOTP API</code> to request a one-time
/// password (OTP) from a specially-formatted SMS message sent by the app's server,
/// i.e., via <code>navigator.credentials.get({otp: ..., ...})</code> Specifically, where a defined
/// policy blocks the use of this feature, the Promise returned by <code>navigator.credentials.get({otp})</code>
/// will reject with a <code>SecurityError DOMException</code>
/// </summary>
public class OtpCredentialsPermissionsPolicyDirectiveBuilder : PermissionsPolicyDirectiveBuilder
{
    /// <summary>
    /// Initializes a new instance of the <see cref="OtpCredentialsPermissionsPolicyDirectiveBuilder"/> class.
    /// </summary>
    public OtpCredentialsPermissionsPolicyDirectiveBuilder() : base("otp-credentials")
    {
    }
}