using System;
using NetEscapades.AspNetCore.SecurityHeaders;

// ReSharper disable once CheckNamespace
namespace Microsoft.AspNetCore.Builder;

/// <summary>
/// Header extension methods for <see cref="IEndpointConventionBuilder"/>.
/// </summary>
public static class EndpointConventionBuilderExtensions
{
    /// <summary>
    /// Adds a security headers policy with the provided policy name to the endpoint(s).
    /// </summary>
    /// <param name="builder">The endpoint convention builder.</param>
    /// <param name="policyName">The security headers policy to use.</param>
    /// <returns>The original convention builder parameter.</returns>
    /// <typeparam name="TBuilder">The type of the endpoint convention builder</typeparam>
    public static TBuilder WithSecurityHeadersPolicy<TBuilder>(this TBuilder builder, string policyName)
        where TBuilder : IEndpointConventionBuilder
    {
        if (builder is null)
        {
            throw new ArgumentNullException(nameof(builder));
        }

        builder.Add(endpointBuilder => { endpointBuilder.Metadata.Add(new SecurityHeadersPolicyMetadata(policyName)); });
        return builder;
    }
}