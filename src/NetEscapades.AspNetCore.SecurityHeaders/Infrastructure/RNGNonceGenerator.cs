#if !NETSTANDARD1_3
using System;
using System.Security.Cryptography;

namespace NetEscapades.AspNetCore.SecurityHeaders.Infrastructure
{
    /// <summary>
    /// Generates nonce values using <see cref="RNGCryptoServiceProvider"/>
    /// </summary>
    internal class RNGNonceGenerator : INonceGenerator, IDisposable
    {
        private readonly RandomNumberGenerator _random = new RNGCryptoServiceProvider();

        /// <inheritdoc />
        public string GetNonce(int nonceBytes)
        {
            // Probably no point in using ArrayPool as these are such small arrays.
            // Adds a slight GC pressure but https://adamsitnik.com/Array-Pool/ suggets
            // it's probably OK
            var bytes = new byte[nonceBytes];
            _random.GetBytes(bytes);

            return Convert.ToBase64String(bytes);
        }

        /// <inheritdoc />
        public void Dispose()
        {
            _random.Dispose();
        }
    }
}
#endif
