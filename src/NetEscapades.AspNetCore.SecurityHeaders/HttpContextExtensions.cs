using System;
using Microsoft.AspNetCore.Http;

namespace NetEscapades.AspNetCore.SecurityHeaders
{
    /// <summary>
    /// Helper methods for working with nonces
    /// </summary>
    internal static class HttpContextExtensions
    {
        /// <summary>
        /// Fetch the nonce (number used once) for the request
        /// </summary>
        /// <param name="context">The <see cref="HttpContext"/> for the request</param>
        /// <returns>The nonce for the request, as a string</returns>
        internal static string GetNonce(this HttpContext context)
        {
            return context.Items[Constants.DefaultNonceKey] as string ?? string.Empty;
        }

        /// <summary>
        /// Fetch the nonce (number used once) for the request
        /// </summary>
        /// <param name="context">The <see cref="HttpContext"/> for the request</param>
        /// <param name="nonce">The nonce to set for the request</param>
        internal static void SetNonce(this HttpContext context, string nonce)
        {
            if (string.IsNullOrEmpty(nonce))
            {
                throw new ArgumentException("Value cannot be null or empty.", nameof(nonce));
            }

            context.Items[Constants.DefaultNonceKey] = nonce;
        }
    }
}