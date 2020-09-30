using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;

namespace NetEscapades.AspNetCore.SecurityHeaders.Headers
{
    /// <summary>
    /// The header value to use for Permissions-Policy.
    /// </summary>
    public class PermissionsPolicyHeader : HtmlOnlyHeaderPolicyBase
    {
        private readonly string _value;

        /// <summary>
        /// Initializes a new instance of the <see cref="PermissionsPolicyHeader"/> class.
        /// </summary>
        /// <param name="value">The value to apply for the header</param>
        public PermissionsPolicyHeader(string value)
        {
            _value = value;
        }

        /// <inheritdoc />
        public override string Header => "Permissions-Policy";

        /// <inheritdoc />
        protected override string GetValue(HttpContext context) => _value;

        /// <summary>
        /// Configure a permission policy.
        /// </summary>
        /// <param name="configure">Configure the Permissions-Policy header</param>
        /// <returns>The complete Permissions-Policy header</returns>
        public static PermissionsPolicyHeader Build(Action<PermissionsPolicyBuilder> configure)
        {
            var builder = new PermissionsPolicyBuilder();

            configure(builder);

            return new PermissionsPolicyHeader(builder.Build());
        }
    }
}
