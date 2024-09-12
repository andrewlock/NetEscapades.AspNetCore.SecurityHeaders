namespace NetEscapades.AspNetCore.SecurityHeaders.Headers.PermissionsPolicy;

/// <summary>
/// The HTTP <c>Permissions-Policy</c> header <c>attribution-reporting</c> directive controls whether the current document is allowed to use the <c>Attribution Reporting API</c>.
/// Specifically, where a defined policy blocks the use of this feature:
/// <list type="bullet">
/// <item><description>Background <c>attributionsrc</c> requests won't be made.</description></item>
/// <item><description>The <c>XMLHttpRequest.setAttributionReporting()</c> method will throw an exception when called.</description></item>
/// <item><description>The <c>attributionReporting</c> option, when included on a <c>fetch()</c> call, will cause it to throw an exception.</description></item>
/// <item><description>Registration headers (<c>Attribution-Reporting-Register-Source</c> and <c>Attribution-Reporting-Register-Trigger</c>) in HTTP responses on associated documents will be ignored.</description></item>
/// </list>
/// </summary>
public class AttributionReportingPermissionsPolicyDirectiveBuilder : PermissionsPolicyDirectiveBuilder
{
    /// <summary>
    /// Initializes a new instance of the <see cref="AttributionReportingPermissionsPolicyDirectiveBuilder"/> class.
    /// </summary>
    public AttributionReportingPermissionsPolicyDirectiveBuilder() : base("attribution-reporting")
    {
    }
}