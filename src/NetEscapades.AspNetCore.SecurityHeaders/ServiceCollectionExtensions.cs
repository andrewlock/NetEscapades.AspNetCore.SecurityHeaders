using System;
using NetEscapades.AspNetCore.SecurityHeaders.Infrastructure;

// ReSharper disable once CheckNamespace
namespace Microsoft.Extensions.DependencyInjection;

/// <summary>
/// Extension methods for setting up security header services in an <see cref="IServiceCollection" />.
/// </summary>
public static class ServiceCollectionExtensions
{
    /// <summary>
    /// Creates a builder for configuring security header policies.
    /// </summary>
    /// <param name="services">The <see cref="IServiceCollection" /> to add services to.</param>
    /// <returns>The <see cref="IServiceCollection"/> so that additional calls can be chained.</returns>
    public static SecurityHeaderPolicyBuilder AddSecurityHeaderPolicies(this IServiceCollection services)
    {
        if (services == null)
        {
            throw new ArgumentNullException(nameof(services));
        }

        var options = new CustomHeaderOptions();
        services.AddSingleton(options);
        return new SecurityHeaderPolicyBuilder(options);
    }
}