using System;
using System.Diagnostics.CodeAnalysis;

namespace NetEscapades.AspNetCore.SecurityHeaders.Helpers;

/// <summary>
/// Throw helpers to shim runtime behaviour
/// </summary>
internal static class ThrowHelpers
{
    /// <summary>
    /// Throw an argument exception with the provided method
    /// </summary>
    /// <param name="paramName">The parameter name</param>
    /// <param name="message">The message name</param>
    [DoesNotReturn]
    public static void ThrowArgumentException(string paramName, string message)
    {
        throw new ArgumentException(message, paramName);
    }
}