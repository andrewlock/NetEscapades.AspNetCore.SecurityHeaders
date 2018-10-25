using System;
using System.Security.Cryptography;

namespace NetEscapades.AspNetCore.SecurityHeaders.TagHelpers
{
    /// <summary>
    /// Helpers for creating hashing algorithms
    /// </summary>
    internal class CryptographyAlgorithms
    {
        /// <summary>
        /// Create an instance of the required hashing algorithm
        /// </summary>
        /// <param name="algorithm">The CSP algorithm to create</param>
        /// <returns>The hashing algorithm instance</returns>
        public static HashAlgorithm Create(CSPHashType algorithm)
        {
            switch (algorithm)
            {
                case CSPHashType.SHA256:
                    return CreateSHA256();
                case CSPHashType.SHA384:
                    return CreateSHA384();
                case CSPHashType.SHA512:
                    return CreateSHA512();
                default:
                    throw new InvalidOperationException($"Unknown CSP Hash Type: {algorithm}");
            }
        }

        /// <summary>
        /// Creates an instance of <see cref="SHA256"/> or <see cref="SHA256CryptoServiceProvider"/> on
        /// FIPS compliant machines.
        /// </summary>
        /// <returns>An instance of <see cref="SHA256"/></returns>
        private static SHA256 CreateSHA256()
        {
            try
            {
                return SHA256.Create();
            }
            catch (System.Reflection.TargetInvocationException)
            {
                // SHA256.Create is documented to throw this exception on FIPS compliant machines.
                // See: https://msdn.microsoft.com/en-us/library/z08hz7ad%28v=vs.110%29.aspx?f=255&MSPPError=-2147217396
                // Fallback to a FIPS compliant SHA256 algorithm.
                return new SHA256CryptoServiceProvider();
            }
        }

        /// <summary>
        /// Creates an instance of <see cref="SHA384" /> or <see cref="SHA384CryptoServiceProvider"/> on
        /// FIPS compliant machines.
        /// </summary>
        /// <returns>An instance of <see cref="SHA384"/></returns>
        private static SHA384 CreateSHA384()
        {
            try
            {
                return SHA384.Create();
            }
            catch (System.Reflection.TargetInvocationException)
            {
                // SHA384.Create is documented to throw this exception on FIPS compliant machines.
                // See: https://msdn.microsoft.com/en-us/library/z08hz7ad%28v=vs.110%29.aspx?f=255&MSPPError=-2147217396
                // Fallback to a FIPS compliant SHA384 algorithm.
                return new SHA384CryptoServiceProvider();
            }
        }

        /// <summary>
        /// Creates an instance of <see cref="SHA512"/> or <see cref="SHA512CryptoServiceProvider"/> on
        /// FIPS compliant machines.
        /// </summary>
        /// <returns>An instance of <see cref="SHA512"/></returns>
        private static SHA512 CreateSHA512()
        {
            try
            {
                return SHA512.Create();
            }
            catch (System.Reflection.TargetInvocationException)
            {
                // SHA512.Create is documented to throw this exception on FIPS compliant machines.
                // See: https://msdn.microsoft.com/en-us/library/z08hz7ad%28v=vs.110%29.aspx?f=255&MSPPError=-2147217396
                // Fallback to a FIPS compliant SHA512 algorithm.
                return new SHA512CryptoServiceProvider();
            }
        }
    }
}