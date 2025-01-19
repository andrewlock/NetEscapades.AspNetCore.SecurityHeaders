using System;
using System.Collections.Generic;
using NetEscapades.AspNetCore.SecurityHeaders.Headers;
using NetEscapades.AspNetCore.SecurityHeaders.Infrastructure;

// ReSharper disable once CheckNamespace
namespace Microsoft.AspNetCore.Builder;

/// <summary>
/// Defines the policies to use for customising security headers for a request.
/// </summary>
public class HeaderPolicyCollection : Dictionary<string, IHeaderPolicy>, IReadOnlyHeaderPolicyCollection
{
    /// <summary>
    /// Initializes a new instance of the <see cref="HeaderPolicyCollection"/> class.
    /// </summary>
    public HeaderPolicyCollection()
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="HeaderPolicyCollection"/> class.
    /// </summary>
    /// <param name="other">The <see cref="IReadOnlyHeaderPolicyCollection"/> policies to copy</param>
    internal HeaderPolicyCollection(IReadOnlyHeaderPolicyCollection other)
        : base(other)
    {
    }
}