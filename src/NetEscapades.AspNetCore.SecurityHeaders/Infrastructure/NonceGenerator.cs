using System;
using System.Security.Cryptography;

namespace NetEscapades.AspNetCore.SecurityHeaders.Infrastructure
{
    /// <summary>
    /// Generates nonce values using <see cref="RandomNumberGenerator"/>
    /// </summary>
    internal class NonceGenerator : IDisposable
    {
        // RandomNumberGenerator.Create is preferred over calling the constructor of the derived class RNGCryptoServiceProvider.
        // https://docs.microsoft.com/en-us/dotnet/api/system.security.cryptography.randomnumbergenerator?view=netframework-4.7.2#remarks
        private readonly RandomNumberGenerator _random = RandomNumberGenerator.Create();

        /// <summary>
        /// Generate a nonce
        /// </summary>
        /// <param name="nonceBytes">The number of bytes used to generate a nonce</param>
        /// <returns>The nonce as a string</returns>
        public string GetNonce(int nonceBytes)
        {
            // Probably no point in using ArrayPool as these are such small arrays.
            // Adds a slight GC pressure but https://adamsitnik.com/Array-Pool/ suggests
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
