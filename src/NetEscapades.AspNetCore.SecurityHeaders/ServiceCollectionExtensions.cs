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
    /// Note that this method overrides any previous calls to the method. Favor calling <see cref="AddSecurityHeaderPolicies(Microsoft.Extensions.DependencyInjection.IServiceCollection)"/>
    /// or <see cref="AddSecurityHeaderPolicies(Microsoft.Extensions.DependencyInjection.IServiceCollection)"/> to chain multiple calls.
    /// </summary>
    /// <param name="services">The <see cref="IServiceCollection" /> to add services to.</param>
    /// <returns>The <see cref="SecurityHeaderPolicyBuilder"/> so that header policies can be configured.</returns>
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

    /// <summary>
    /// Creates a builder for configuring security header policies.
    /// </summary>
    /// <param name="services">The <see cref="IServiceCollection" /> to add services to.</param>
    /// <param name="configure">A configuration method to configure your header policies</param>
    /// <returns>The <see cref="IServiceCollection"/> so that additional calls can be chained.</returns>
    public static IServiceCollection AddSecurityHeaderPolicies(
        this IServiceCollection services,
        Action<SecurityHeaderPolicyBuilder> configure)
    {
        if (services == null)
        {
            throw new ArgumentNullException(nameof(services));
        }

        if (configure == null)
        {
            throw new ArgumentNullException(nameof(configure));
        }

        services.AddSingleton(new ConfigureHeaderOptions((options, provider) =>
        {
            configure(new SecurityHeaderPolicyBuilder(options));
        }));

        return services;
    }

    /// <summary>
    /// Creates a builder for configuring security header policies.
    /// </summary>
    /// <param name="services">The <see cref="IServiceCollection" /> to add services to.</param>
    /// <param name="configure">A configuration method that provides access to an <see cref="IServiceProvider"/>
    /// to configure your header policies</param>
    /// <returns>The <see cref="IServiceCollection"/> so that additional calls can be chained.</returns>
    public static IServiceCollection AddSecurityHeaderPolicies(
        this IServiceCollection services,
        Action<SecurityHeaderPolicyBuilder, IServiceProvider> configure)
    {
        if (services == null)
        {
            throw new ArgumentNullException(nameof(services));
        }

        if (configure == null)
        {
            throw new ArgumentNullException(nameof(configure));
        }

        services.AddSingleton(new ConfigureHeaderOptions((options, provider) =>
        {
            configure(new SecurityHeaderPolicyBuilder(options), provider);
        }));

        return services;
    }
}