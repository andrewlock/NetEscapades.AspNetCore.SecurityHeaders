using System;
using NetEscapades.AspNetCore.SecurityHeaders;
using NetEscapades.AspNetCore.SecurityHeaders.Infrastructure;

namespace Microsoft.AspNetCore.Builder
{
    /// <summary>
    /// The <see cref="IApplicationBuilder"/> extensions for adding Security headers middleware support.
    /// </summary>
    public static class SecurityHeadersMiddlewareExtensions
    {
        /// <summary>
        /// Adds middleware to your web application pipeline to automatically add security headers to requests
        /// </summary>
        /// <param name="app">The IApplicationBuilder passed to your Configure method.</param>
        /// <param name="policies">A configured policy collection.</param>
        /// <returns>The original app parameter</returns>
        public static IApplicationBuilder UseSecurityHeaders(this IApplicationBuilder app, HeaderPolicyCollection policies)
        {
            if (app == null)
            {
                throw new ArgumentNullException(nameof(app));
            }

            if (policies == null)
            {
                throw new ArgumentNullException(nameof(policies));
            }

            var service = app.ApplicationServices.GetService(typeof(ICustomHeaderService)) ?? new CustomHeaderService();

            return app.UseMiddleware<SecurityHeadersMiddleware>(service, policies);
        }

        /// <summary>
        /// Adds middleware to your web application pipeline to automatically add security headers to requests
        /// </summary>
        /// <param name="app">The IApplicationBuilder passed to your Configure method.</param>
        /// <param name="configure">An <see cref="Action{T}"/>to configure the security headers for the application</param>
        /// <returns>The original app parameter</returns>
        public static IApplicationBuilder UseSecurityHeaders(this IApplicationBuilder app, Action<HeaderPolicyCollection> configure)
        {
            if (configure == null)
            {
                throw new ArgumentNullException(nameof(configure));
            }

            var policies = new HeaderPolicyCollection();
            configure(policies);

            return app.UseSecurityHeaders(policies);
        }

        /// <summary>
        /// Adds middleware to your web application pipeline to automatically add security headers to requests
        ///
        /// Adds a policy collection configured using the default security headers, as in <see cref="HeaderPolicyCollectionExtensions.AddDefaultSecurityHeaders"/>
        /// </summary>
        /// <param name="app">The IApplicationBuilder passed to your Configure method.</param>
        /// <returns>The original app parameter</returns>
        public static IApplicationBuilder UseSecurityHeaders(this IApplicationBuilder app)
        {
            return app.UseSecurityHeaders(policies => policies.AddDefaultSecurityHeaders());
        }
    }
}