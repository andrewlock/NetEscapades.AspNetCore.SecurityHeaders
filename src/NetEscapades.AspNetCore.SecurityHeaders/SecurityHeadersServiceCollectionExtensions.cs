using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using NetEscapades.AspNetCore.SecurityHeaders.Infrastructure;

namespace NetEscapades.AspNetCore.SecurityHeaders
{
    /// <summary>
    /// Extension methods for setting up security header services in an <see cref="IServiceCollection" />.
    /// </summary>
    public static class SecurityHeadersServiceCollectionExtensions
    {
        /// <summary>
        /// Adds security header services to the specified <see cref="IServiceCollection" />.
        /// </summary>
        /// <param name="services">The <see cref="IServiceCollection" /> to add services to.</param>
        /// <returns>The <see cref="IServiceCollection"/> so that additional calls can be chained.</returns>
        public static IServiceCollection AddSecurityHeaders(this IServiceCollection services)
        {
            if (services == null)
            {
                throw new ArgumentNullException(nameof(services));
            }

            services.AddOptions();

            services.TryAdd(ServiceDescriptor.Transient<ISecurityHeadersService, SecurityHeadersService>());
            services.TryAdd(ServiceDescriptor.Transient<ISecurityHeadersPolicyProvider, DefaultSecurityHeadersPolicyProvider>());

            return services;
        }

        /// <summary>
        /// Adds security header services to the specified <see cref="IServiceCollection" />.
        /// </summary>
        /// <param name="services">The <see cref="IServiceCollection" /> to add services to.</param>
        /// <param name="setupAction">An <see cref="Action{SecurityHeadersOptions}"/> to configure the provided <see cref="SecurityHeadersOptions"/>.</param>
        /// <returns>The <see cref="IServiceCollection"/> so that additional calls can be chained.</returns>
        public static IServiceCollection AddSecurityHeaders(this IServiceCollection services, Action<SecurityHeadersOptions> setupAction)
        {
            if (services == null)
            {
                throw new ArgumentNullException(nameof(services));
            }

            if (setupAction == null)
            {
                throw new ArgumentNullException(nameof(setupAction));
            }

            services.AddSecurityHeaders();
            services.Configure(setupAction);

            return services;
        }
    }
}
