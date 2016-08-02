using Microsoft.AspNetCore.Http;

namespace NetEscapades.AspNetCore.SecurityHeaders.Infrastructure
{
    /// <summary>
    /// The header value to use for Strict-Transport-Security
    /// </summary>
    public class StrictTransportSecurityHeader : HeaderPolicyBase
    {        
        /// <inheritdoc />
        public StrictTransportSecurityHeader(string value) : base(value)
        {
        }

        /// <summary>
        /// The number of seconds in one year
        /// </summary>
        public const int OneYearInSeconds = 60 * 60 * 24 * 365;

        ///<inheritdoc />
        public override string Header { get; } = "Strict-Transport-Security";

        /// <summary>
        /// Tells the user-agent to cache the domain in the STS list for the provided number of seconds {0} 
        /// </summary>
        /// <param name="maxAgeInSeconds">The maimum number of seconds to cache the domain</param>
        /// <returns></returns>
        public static StrictTransportSecurityHeader MaxAge(int maxAgeInSeconds)
        {
            return new StrictTransportSecurityHeader($"max-age={maxAgeInSeconds}");
        }

        /// <summary>
        /// Tells the user-agent to cache the domain in the STS list for the provided number of seconds {0} and include any sub-domains. 
        /// </summary>
        /// <param name="maxAgeInSeconds">The maimum number of seconds to cache the domain</param>
        /// <returns></returns>
        public static StrictTransportSecurityHeader MaxAgeIncludeSubdomains(int maxAgeInSeconds)
        {
            return new StrictTransportSecurityHeader($"max-age={maxAgeInSeconds}; includeSubDomains");
        }

        /// <summary>
        /// Tells the user-agent to remove, or not cache the host in the STS cache. 
        /// </summary>
        /// <returns></returns>
        public static StrictTransportSecurityHeader NoCache()
        {
            return new StrictTransportSecurityHeader($"max-age=0");
        }

        /// <inheritdoc />
        protected override void EvaluateHttpRequest(HttpContext context, CustomHeadersResult result)
        {
            return;
        }
    }
}