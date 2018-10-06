using System.Collections.Generic;

namespace NetEscapades.AspNetCore.SecurityHeaders.Infrastructure
{
    /// <summary>
    /// Used to build a CSP directive that has a standard set of sources.
    /// </summary>
    public class CspDirectiveBuilder : CspDirectiveBuilderBase
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CspDirectiveBuilder"/> class.
        /// </summary>
        /// <param name="directive">The name of the directive</param>
        public CspDirectiveBuilder(string directive) : base(directive)
        {
        }

        /// <summary>
        /// The sources from which the directive is allowed.
        /// </summary>
        public List<string> Sources { get; } = new List<string>();

        /// <summary>
        /// If true, no sources are allowed.
        /// </summary>
        public bool BlockResources { get; set; } = false;

        /// <inheritdoc />
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
            if (string.IsNullOrEmpty(value))
            {
                return string.Empty;
            }

            return $"{Directive} {value}";
        }
    }
}