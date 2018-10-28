using System.Collections.Generic;
using NetEscapades.AspNetCore.SecurityHeaders.Headers;

// ReSharper disable once CheckNamespace
namespace Microsoft.AspNetCore.Builder
{
    /// <summary>
    /// Defines the policies to use for customising security headers for a request.
    /// </summary>
    public class HeaderPolicyCollection : Dictionary<string, IHeaderPolicy>
    {
    }
}