using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Builder;

namespace NetEscapades.AspNetCore.SecurityHeaders.Infrastructure;

/// <summary>
/// Provides programmatic configuration for Security Headers.
/// </summary>
internal class CustomHeaderOptions
{
    /// <summary>
    /// The collections of policies to apply
    /// </summary>
    /// <returns>The collection of policies, indexed by header name</returns>
    public Dictionary<string, HeaderPolicyCollection> NamedPolicyCollections { get; } = new();

    /// <summary>
    /// The default policy to apply
    /// </summary>
    public HeaderPolicyCollection? DefaultPolicy { get; set; }

    /// <summary>
    /// The policy selector function
    /// </summary>
    public Func<PolicySelectorContext, IReadOnlyHeaderPolicyCollection>? PolicySelector { get; set; }

    /// <summary>
    /// Gets the policy based on the <paramref name="name"/>
    /// </summary>
    /// <param name="name">The name of the policy to lookup.</param>
    /// <returns>The <see cref="HeaderPolicyCollection"/> if the policy was added.<c>null</c> otherwise.</returns>
    public HeaderPolicyCollection? GetPolicy(string name)
    {
        if (name == null)
        {
            throw new ArgumentNullException(nameof(name));
        }

        return NamedPolicyCollections.GetValueOrDefault(name);
    }
}