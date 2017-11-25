using System;

namespace NetEscapades.AspNetCore.SecurityHeaders.Infrastructure
{
    public abstract class CspDirectiveBuilderBase
    {
        public CspDirectiveBuilderBase(string directive)
        {
            Directive = directive;
        }

        internal string Directive { get; }
        internal abstract string Build();
    }
}