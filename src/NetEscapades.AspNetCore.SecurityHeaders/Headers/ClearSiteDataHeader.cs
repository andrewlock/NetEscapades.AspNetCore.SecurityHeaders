using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using NetEscapades.AspNetCore.SecurityHeaders.Infrastructure;

namespace NetEscapades.AspNetCore.SecurityHeaders.Headers;

/// <summary>
/// The header value to use for <c>Clear-Site-Data</c>. Browsers only honour
/// this header in secure (HTTPS) contexts, so it is suppressed on plain
/// HTTP requests.
/// </summary>
/// <seealso href="https://developer.mozilla.org/en-US/docs/Web/HTTP/Reference/Headers/Clear-Site-Data"/>
public class ClearSiteDataHeader : HeaderPolicyBase
{
    private readonly string _value;

    /// <summary>
    /// Initializes a new instance of the <see cref="ClearSiteDataHeader"/> class.
    /// </summary>
    /// <param name="value">The pre-built header value, e.g. <c>"cache", "cookies"</c>.</param>
    public ClearSiteDataHeader(string value)
    {
        _value = value;
    }

    /// <inheritdoc />
    public override string Header => "Clear-Site-Data";

    /// <inheritdoc />
    protected override string GetValue(HttpContext context) => _value;

    /// <inheritdoc />
    protected override void EvaluateHttpRequest(HttpContext context, CustomHeadersResult result)
    {
        // Clear-Site-Data is only honoured by browsers in secure contexts,
        // so don't emit it on plain HTTP responses.
    }

    /// <summary>
    /// Configure a <see cref="ClearSiteDataHeader"/> using the supplied builder action.
    /// </summary>
    /// <param name="configure">Configure the <c>Clear-Site-Data</c> directives.</param>
    /// <returns>The configured <see cref="ClearSiteDataHeader"/>.</returns>
    public static ClearSiteDataHeader Build(Action<ClearSiteDataBuilder> configure)
    {
        var builder = new ClearSiteDataBuilder();
        configure(builder);
        return new ClearSiteDataHeader(builder.Build());
    }
}
