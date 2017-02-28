namespace NetEscapades.AspNetCore.SecurityHeaders.Infrastructure
{

    /// <summary>
    /// The header value to use for ReferrerPolicy
    /// </summary>
    public class ReferrerPolicyHeader : HeaderPolicyBase
    {
        /// <inheritdoc />
        public ReferrerPolicyHeader(string value) : base(value)
        {
            Value = value;
        }

        /// <inheritdoc />
        public override string Header { get; } = "Referrer-Policy";

        /// <summary>
        /// Indicates that the site doesn't want to set a Referrer Policy 
        /// here and the browser should fallback to a Referrer Policy defined 
        /// via other mechanisms elsewhere
        /// </summary>
        public static ReferrerPolicyHeader None()
        {
            return new ReferrerPolicyHeader("");
        }

        /// <summary>
        /// Instructs the browser to never send the referrer header with requests 
        /// that are made from your site. This also include links to pages on your own site.
        /// </summary>
        public static ReferrerPolicyHeader NoReferrer()
        {
            return new ReferrerPolicyHeader("no-referrer");
        }

        /// <summary>
        /// The browser will not send the referrer header when navigating from HTTPS to HTTP, 
        /// but will always send the full URL in the referrer header when navigating 
        /// from HTTP to any origin.
        /// </summary>
        public static ReferrerPolicyHeader NoReferrerWhenDowngrade()
        {
            return new ReferrerPolicyHeader("no-referrer-when-downgrade");
        }

        /// <summary>
        /// The browser will only set the referrer header on requests to the same origin. 
        /// If the destination is another origin then no referrer information will be sent.
        /// </summary>
        public static ReferrerPolicyHeader SameOrigin()
        {
            return new ReferrerPolicyHeader("same-origin");
        }

        /// <summary>
        /// The browser will always set the referrer header to the origin from which the request was made. 
        /// This will strip any path information from the referrer information.
        /// </summary>
        public static ReferrerPolicyHeader Origin()
        {
            return new ReferrerPolicyHeader("origin");
        }

        /// <summary>
        /// The browser will always set the referrer header to the origin from which the request was made, as
        /// long as the destination is HTTPS, otherwise no refer will not be sent. 
        /// This will strip any path information from the referrer information.
        /// </summary>
        public static ReferrerPolicyHeader StrictOrigin()
        {
            return new ReferrerPolicyHeader("strict-origin");
        }

        /// <summary>
        /// The browser will send the full URL to requests to the same origin but 
        /// only send the origin when requests are cross-origin.
        /// </summary>
        public static ReferrerPolicyHeader OriginWhenCrossOrigin()
        {
            return new ReferrerPolicyHeader("origin-when-cross-origin");
        }

        /// <summary>
        /// The browser will send the full URL to requests to the same origin but 
        /// only send the origin when requests are cross-origin, as long as a scheme
        /// downgrade has not happened (i.e. you are not moving from HTTPS to HTTP)
        /// </summary>
        public static ReferrerPolicyHeader StrictOriginWhenCrossOrigin()
        {
            return new ReferrerPolicyHeader("strict-origin-when-cross-origin");
        }

        /// <summary>
        /// The browser will always send the full URL with any request to any origin.
        /// </summary>
        public static ReferrerPolicyHeader UnsafeUrl()
        {
            return new ReferrerPolicyHeader("unsafe-url");
        }
    }
}
