using System;
using Microsoft.AspNetCore.Http;

namespace NetEscapades.AspNetCore.SecurityHeaders.Headers.CrossOriginPolicies.EmbedderPolicy
{
    /// <summary>
    /// The Cross Origin Embedder Policy directive builder base class
    /// </summary>
    public abstract class CrossOriginEmbedderPolicyDirectiveBuilderBase : CrossOriginPolicyDirectiveBuilderBase
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CrossOriginEmbedderPolicyDirectiveBuilderBase"/> class.
        /// </summary>
        /// <param name="directive">The name of the directive</param>
        protected CrossOriginEmbedderPolicyDirectiveBuilderBase(string directive) : base(directive)
        {
        }

        /// <inheritdoc />
        internal override abstract Func<HttpContext, string> CreateBuilder();
    }
}
