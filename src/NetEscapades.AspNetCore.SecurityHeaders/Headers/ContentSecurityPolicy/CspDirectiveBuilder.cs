using System.Collections.Generic;

namespace NetEscapades.AspNetCore.SecurityHeaders.Infrastructure
{
    public class CspDirectiveBuilder : CspDirectiveBuilderBase
    {
        public CspDirectiveBuilder(string directive) : base(directive) { }

        public List<string> Sources { get; } = new List<string>();
        public bool BlockResources { get; set; } = false;

        internal override string Build()
        {
            if (BlockResources)
            {
                return GetPolicy("'none'");
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