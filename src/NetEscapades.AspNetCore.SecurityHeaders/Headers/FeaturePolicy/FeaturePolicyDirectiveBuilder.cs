using System;
using System.Collections.Generic;

namespace NetEscapades.AspNetCore.SecurityHeaders.Infrastructure
{
    public class FeaturePolicyDirectiveBuilder : FeaturePolicyDirectiveBuilderBase
    {
        public FeaturePolicyDirectiveBuilder(string directive) : base(directive) { }

        public List<string> Sources { get; } = new List<string>();
        public bool BlockResources { get; set; } = false;
        public bool AllowAllResources { get; set; } = false;

        internal override string Build()
        {
            if(BlockResources && AllowAllResources)
            {
                throw new InvalidOperationException(
                    $"Invalid directive values for Feature-Policy '{Directive}' directive: you cannot both block all resources and allow all values");
            }
            if (BlockResources)
            {
                return GetPolicy("'none'");
            }
            if (AllowAllResources)
            {
                return GetPolicy("*");
            }
            return GetPolicy(string.Join(" ", Sources));
        }

        private string GetPolicy(string value)
        {
            if (string.IsNullOrEmpty(value)) { return string.Empty; }
            return $"{Directive} {value}";
        }
    }
}