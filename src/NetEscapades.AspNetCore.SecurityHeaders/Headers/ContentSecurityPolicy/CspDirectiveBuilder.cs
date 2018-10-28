using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Http;

namespace NetEscapades.AspNetCore.SecurityHeaders.Headers.ContentSecurityPolicy
{
    /// <summary>
    /// Used to build a CSP directive that has a standard set of sources.
    /// </summary>
    public class CspDirectiveBuilder : CspDirectiveBuilderBase
    {
        private const string Separator = " ";

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
        /// A collection of functions which is used to generate the sources for which a directive is
        /// allowed for a given request
        /// </summary>
        internal List<Func<HttpContext, string>> SourceBuilders { get; } = new List<Func<HttpContext, string>>();

        /// <summary>
        /// If true, the header directives are unique per request, and require
        /// runtime formatting (e.g. for use with Nonce).
        /// </summary>
        internal override bool HasPerRequestValues => SourceBuilders.Any();

        /// <summary>
        /// If true, no sources are allowed.
        /// </summary>
        public bool BlockResources { get; set; } = false;

        /// <inheritdoc />
        internal override Func<HttpContext, string> CreateBuilder()
        {
            if (BlockResources)
            {
                return ctx => GetPolicy("'none'");
            }

            var sources = string.Join(Separator, Sources);

            if (!HasPerRequestValues)
            {
                var directive = GetPolicy(sources);
                return ctx => directive;
            }

            var builders = SourceBuilders;

            if (!string.IsNullOrEmpty(sources))
            {
                // Copy, so calls to CreateBuilder are idempotent
                builders = SourceBuilders.ToList();

                // insert the constant sources first, just for aesthetics
                builders.Insert(0, ctx => sources);
            }

            return ctx =>
            {
                var dynamicSources = builders
                    .Select(builder => builder.Invoke(ctx))
                    .Where(str => !string.IsNullOrEmpty(str));
                return GetPolicy(string.Join(Separator, dynamicSources));
            };
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