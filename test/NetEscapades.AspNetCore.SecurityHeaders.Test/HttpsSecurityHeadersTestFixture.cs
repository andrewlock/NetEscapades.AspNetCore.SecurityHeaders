namespace NetEscapades.AspNetCore.SecurityHeaders.Infrastructure
{
    public class HttpsSecurityHeadersTestFixture<TStartup> : SecurityHeadersTestFixtureBase<TStartup>
        where TStartup : class
    {
        public HttpsSecurityHeadersTestFixture() : base("https://example.com:5001")
        {
        }
    }
}