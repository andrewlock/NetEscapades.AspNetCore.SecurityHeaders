using System;

namespace NetEscapades.AspNetCore.SecurityHeaders.Infrastructure
{
    public abstract class FeaturePolicyDirectiveBuilderBase
    {
        public FeaturePolicyDirectiveBuilderBase(string directive)
        {
            Directive = directive;
        }

        internal string Directive { get; }
        internal abstract string Build();
    }
}