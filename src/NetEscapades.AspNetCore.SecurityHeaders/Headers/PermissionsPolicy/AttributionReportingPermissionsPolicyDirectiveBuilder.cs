namespace NetEscapades.AspNetCore.SecurityHeaders.Headers.PermissionsPolicy;

/// <summary>
/// Controls whether the current document is allowed to use the attribution reporting API.
/// If disabled then attributionsrc requests won't be made. Calling XMLHttpRequest.setAttributionReporting() will
/// throw an <code>Exception</code>. Including the attributionReporting option on a fetch() call will throw an <code>Exception</code>.
/// Registration headers "Attribution-Reporting-Register-Source" and "Attribution-Reporting-Register-Trigger" in 
/// HTTP responses on associated documents will be ignored.
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