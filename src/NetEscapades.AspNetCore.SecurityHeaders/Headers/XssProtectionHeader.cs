using System;
using Microsoft.AspNetCore.Http;

namespace NetEscapades.AspNetCore.SecurityHeaders.Headers;

/// <summary>
/// The header value to use for XSS-Protection
/// </summary>
[Obsolete(ObsoleteHeader)]
public class XssProtectionHeader : HeaderPolicyBase
{
    /// <summary>
    /// Obsolete header description
    /// </summary>
    internal const string ObsoleteHeader =
        "The `X-XSS-Protection` header can create XSS vulnerabilities in otherwise safe websites." +
        " For more details, see https://developer.mozilla.org/en-US/docs/Web/HTTP/Headers/X-XSS-Protection";

    private readonly string _value;

    /// <summary>
    /// Initializes a new instance of the <see cref="XssProtectionHeader"/> class.
    /// </summary>
    /// <param name="value">The value to apply for the header</param>
    public XssProtectionHeader(string value)
    {
        _value = value;
    }

    /// <inheritdoc />
    public override string Header { get; } = "X-XSS-Protection";

    /// <inheritdoc />
    protected override string GetValue(HttpContext context) => _value;
}