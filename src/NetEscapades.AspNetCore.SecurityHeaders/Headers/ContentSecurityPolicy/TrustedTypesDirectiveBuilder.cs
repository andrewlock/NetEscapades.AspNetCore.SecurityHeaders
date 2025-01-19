using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Http;
using NetEscapades.AspNetCore.SecurityHeaders.Helpers;

namespace NetEscapades.AspNetCore.SecurityHeaders.Headers.ContentSecurityPolicy;

/// <summary>
/// The <c>trusted-types</c> directive instructs user agents
/// to restrict the creation of Trusted Types policies - functions that build non-spoofable,
/// typed values intended to be passed to DOM XSS sinks in place of strings.
///
/// Together with <see cref="RequireTrustedTypesForDirectiveBuilder"/> this allows authors
/// to define rules guarding writing values to the DOM and thus reducing the DOM XSS attack
/// surface to small, isolated parts of the web application codebase, facilitating their
/// monitoring and code review. This directive declares an allowlist of trusted type policy names
/// created with <c>trustedTypes.createPolicy</c> from Trusted Types API.
/// </summary>
public class TrustedTypesDirectiveBuilder : CspDirectiveBuilderBase
{
    private const string Separator = " ";

    /// <summary>
    /// Initializes a new instance of the <see cref="TrustedTypesDirectiveBuilder"/> class.
    /// </summary>
    public TrustedTypesDirectiveBuilder() : base("trusted-types")
    {
    }

    /// <summary>
    /// The policies to apply.
    /// </summary>
    private List<string> Policies { get; } = new();

    /// <summary>
    /// Allows for creating policies with a name that was already used.
    /// </summary>
    /// <returns>The CSP builder for method chaining</returns>
    public TrustedTypesDirectiveBuilder AllowDuplicates()
    {
        Policies.Add("'allow-duplicates'");
        return this;
    }

    /// <summary>
    /// Disallows creating any Trusted Type policy (same as not specifying any policyName).
    /// </summary>
    /// <returns>The CSP builder for method chaining</returns>
    public TrustedTypesDirectiveBuilder None()
    {
        Policies.Add("'none'");
        return this;
    }

    /// <summary>
    /// Allows creating any Trusted Type policy (same as not specifying any policyName).
    /// </summary>
    /// <returns>The CSP builder for method chaining</returns>
    public TrustedTypesDirectiveBuilder Any()
    {
        Policies.Add("*");
        return this;
    }

    /// <summary>
    /// Adds a policy name to the allow-list for trusted type policies. A valid policy
    /// name consists only of alphanumeric characters, or one of <c>-#=_/@.%</c>
    /// </summary>
    /// <returns>The CSP builder for method chaining</returns>
    /// <param name="policyName">The policy name to add</param>
    public TrustedTypesDirectiveBuilder AllowPolicy(string policyName)
    {
        foreach (var c in policyName)
        {
            if (!char.IsLetterOrDigit(c)
                && c is not ('-' or '#' or '=' or '_' or '/' or '@' or '.' or '%'))
            {
                ThrowHelpers.ThrowArgumentException(
                    nameof(policyName),
                    $"Trusted type policy name '{policyName}' is invalid. Policy names must contain only alphanumeric characters or one of -#=_/@.%");
            }
        }

        Policies.Add(policyName);
        return this;
    }

    /// <inheritdoc />
    internal override Func<HttpContext, string> CreateBuilder()
    {
        var policies = string.Join(Separator, Policies);
        var result = string.IsNullOrEmpty(policies) ? Directive : $"{Directive} {policies}";

        return _ => result;
    }
}