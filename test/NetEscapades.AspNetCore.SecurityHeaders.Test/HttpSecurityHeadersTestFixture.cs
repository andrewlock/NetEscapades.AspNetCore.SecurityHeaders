using System;

namespace NetEscapades.AspNetCore.SecurityHeaders.Infrastructure
{
    public class HttpSecurityHeadersTestFixture<TStartup> : SecurityHeadersTestFixtureBase<TStartup>
        where TStartup : class
    {
        public HttpSecurityHeadersTestFixture() : base("http://localhost:5000")
        {
        }
    }
}