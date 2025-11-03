using System;
using System.Diagnostics;

namespace NetEscapades.AspNetCore.SecurityHeaders.Headers.ContentSecurityPolicy;

/// <summary>
/// Marker attribute used by the Content Security Policy source generator.
/// Applying this attribute to a directive builder class instructs the generator
/// to include the specified mixin helpers (e.g. support for <c>'self'</c>, host/scheme
/// sources, <c>nonce-</c>, hashes, <c>'unsafe-*'</c> tokens, etc.).
/// </summary>
[AttributeUsage(AttributeTargets.Class)]
[Conditional("KEEP_ATTRIBUTES")]
internal class CspMixinAttribute : Attribute
{
    /// <summary>
    /// Initializes a new instance of the <see cref="CspMixinAttribute"/> class.
    /// </summary>
    /// <param name="mixinTypes">
    /// Flags indicating which mixin helpers should be generated for the decorated class.
    /// Combine values from <see cref="MixinTypes"/> to enable multiple helpers.
    /// </param>
    public CspMixinAttribute(MixinTypes mixinTypes)
    {
        _ = mixinTypes;
    }
}