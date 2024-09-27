using System;
using System.Security.Cryptography;

namespace NetEscapades.AspNetCore.SecurityHeaders.Infrastructure;

/// <summary>
/// Generates nonce values using <see cref="RandomNumberGenerator"/>
/// </summary>
internal static class NonceGenerator
{
    /// <summary>
    /// Generate a nonce
    /// </summary>
    /// <returns>The nonce as a string</returns>
    public static string CreateNonce()
    {
        Span<byte> bytes = stackalloc byte[Constants.DefaultBytesInNonce];
        RandomNumberGenerator.Fill(bytes);
        return Convert.ToBase64String(bytes);
    }
}