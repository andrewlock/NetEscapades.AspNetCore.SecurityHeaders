using System;
using Microsoft.Extensions.DependencyInjection.Extensions;
using NetEscapades.AspNetCore.SecurityHeaders.Infrastructure;

namespace Microsoft.Extensions.DependencyInjection
{
    /// <summary>
    /// Extension methods for setting up security header services in an <see cref="IServiceCollection" />.
    /// </summary>
    public static class CustomHeadersServiceCollectionExtensions
    {
        /// <summary>
        /// Adds security header services to the specified <see cref="IServiceCollection" />.
        /// </summary>
        /// <param name="services">The <see cref="IServiceCollection" /> to add services to.</param>
        /// <returns>The <see cref="IServiceCollection"/> so that additional calls can be chained.</returns>
        [Obsolete("This method will be removed in a future version")]
        public static IServiceCollection AddCustomHeaders(this IServiceCollection services)
        {
            if (services == null)
            {
                throw new ArgumentNullException(nameof(services));
            }

            services.AddOptions();

            services.TryAdd(ServiceDescriptor.Singleton<ICustomHeaderService, CustomHeaderService>());
            services.TryAdd(ServiceDescriptor.Singleton<ICustomHeaderPolicyProvider, DefaultCustomHeaderPolicyProvider>());

            return services;
        }

        /// <summary>
        /// Adds security header services to the specified <see cref="IServiceCollection" />.
        /// </summary>
        /// <param name="services">The <see cref="IServiceCollection" /> to add services to.</param>
        /// <param name="setupAction">An <see cref="Action{SecurityHeadersOptions}"/> to configure the provided <see cref="CustomHeaderOptions"/>.</param>
        /// <returns>The <see cref="IServiceCollection"/> so that additional calls can be chained.</returns>
        [Obsolete("This method will be removed in a future version")]
        public static IServiceCollection AddCustomHeaders(this IServiceCollection services, Action<CustomHeaderOptions> setupAction)
        {
            if (services == null)
            {
                throw new ArgumentNullException(nameof(services));
            }

            if (setupAction == null)
            {
                throw new ArgumentNullException(nameof(setupAction));
            }

            services.AddCustomHeaders();
            services.Configure(setupAction);

            return services;
        }
    }
}
