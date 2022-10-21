using System;
using Microsoft.AspNetCore.Http;

namespace NetEscapades.AspNetCore.SecurityHeaders.Headers;

/// <summary>
/// The header value to use for Reporting-Endpoints
/// </summary>
public abstract class ReportingEndpointsHeader : HeaderPolicyBase
{
    /// <summary>
    /// Initializes a new instance of the <see cref="ReportingEndpointsHeader"/> class.
    /// </summary>
    protected ReportingEndpointsHeader()
    {
    }

    /// <inheritdoc />
    public override string Header => "Reporting-Endpoints";

    /// <summary>
    /// Configure a Reporting-Endpoints policy
    /// </summary>
    /// <param name="configure">Configure the Cross Origin Embedder Policy</param>
    /// <returns>The configured <see cref="CrossOriginEmbedderPolicyHeader "/></returns>
    public static ReportingEndpointsHeader Build(Action<ReportingEndpointsHeaderBuilder> configure)
    {
        var builder = new ReportingEndpointsHeaderBuilder();

        configure(builder);

        var constantValue = builder.Build();
        return new StaticReportingEndpointsHeader(constantValue);
    }

    /// <summary>
    /// A <see cref="CrossOriginEmbedderPolicyHeader"/> which has a single static value
    /// </summary>
    public class StaticReportingEndpointsHeader : ReportingEndpointsHeader
    {
        private readonly string _value;

        /// <summary>
        /// Initializes a new instance of the <see cref="ReportingEndpointsHeader.StaticReportingEndpointsHeader"/> class.
        /// </summary>
        /// <param name="value">The value to apply for the header</param>
        public StaticReportingEndpointsHeader(string value)
        {
            _value = value;
        }

        /// <inheritdoc />
        protected override string GetValue(HttpContext context) => _value;
    }
}