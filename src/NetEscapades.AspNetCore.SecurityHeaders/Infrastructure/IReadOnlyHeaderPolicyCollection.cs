using System.Collections.Generic;
using Microsoft.AspNetCore.Builder;
using NetEscapades.AspNetCore.SecurityHeaders.Headers;

namespace NetEscapades.AspNetCore.SecurityHeaders.Infrastructure;

/// <summary>
/// A readonly view of a <see cref="HeaderPolicyCollection"/>
/// </summary>
public interface IReadOnlyHeaderPolicyCollection : IReadOnlyDictionary<string, IHeaderPolicy>
{
}