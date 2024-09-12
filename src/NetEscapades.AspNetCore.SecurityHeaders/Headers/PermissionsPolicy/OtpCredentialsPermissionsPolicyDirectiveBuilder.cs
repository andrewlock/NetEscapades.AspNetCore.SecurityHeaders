namespace NetEscapades.AspNetCore.SecurityHeaders.Headers.PermissionsPolicy;

/// <summary>
/// The HTTP <c>Permissions-Policy</c> header <c>otp-credentials</c> directive controls whether
/// the current document is allowed to use the <c>WebOTP API</c> to request a one-time password (OTP)
/// from a specially-formatted SMS message sent by the app's server, i.e., via
/// <c>navigator.credentials.get({otp: ..., ...})</c>. Specifically, where a defined policy blocks
/// the use of this feature, the <c>Promise</c> returned by <c>navigator.credentials.get({otp})</c> will
/// reject with a <c>SecurityError DOMException</c>.
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