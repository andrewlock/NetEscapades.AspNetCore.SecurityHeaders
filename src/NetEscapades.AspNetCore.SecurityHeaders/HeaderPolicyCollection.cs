using System.Collections.Generic;
using NetEscapades.AspNetCore.SecurityHeaders.Infrastructure;

namespace NetEscapades.AspNetCore.SecurityHeaders
{
    /// <summary>
    /// Defines the policies to use for customising security headers for a request.
    /// </summary>
    public class HeaderPolicyCollection : Dictionary<string, IHeaderPolicy>
    {
    }
}