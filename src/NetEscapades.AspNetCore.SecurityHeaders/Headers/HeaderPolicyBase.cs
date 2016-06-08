using System;
using Microsoft.AspNetCore.Http;

namespace NetEscapades.AspNetCore.SecurityHeaders.Infrastructure
{
    public abstract class HeaderPolicyBase : IHeaderPolicy
    {
        protected HeaderPolicyBase(string value)
        {
            Value = value;
        }

        ///<inheritdoc />
        public abstract string Header { get; }

        ///<inheritdoc />
        public string Value { get; set; }

        ///<inheritdoc />
        public void Apply(HttpContext context, CustomHeadersResult result)
        {
            var request = context?.Request;
            if (context == null)
            {
                throw new ArgumentNullException(nameof(context));
            }

            if (request.IsHttps)
            {
                EvaluateHttpsRequest(context, result);
            }
            else
            {
                EvaluateHttpRequest(context, result);
            }
        }

        protected virtual void EvaluateHttpRequest(HttpContext context, CustomHeadersResult result)
        {
            result.SetHeaders[Header] = Value;
        }

        protected virtual void EvaluateHttpsRequest(HttpContext context, CustomHeadersResult result)
        {
            result.SetHeaders[Header] = Value;
        }

    }
}