using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;

namespace RazorWebSite
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            app.UseSecurityHeaders(policies => policies
                .AddDefaultSecurityHeaders()
                .AddContentSecurityPolicy(csp =>
                {
                    csp.AddDefaultSrc().Self();
                    csp.AddScriptSrc().Self().WithHashTagHelper().WithNonce();
                    csp.AddStyleSrc().Self().WithHashTagHelper().WithNonce();
                })
                .AddFeaturePolicy(fp =>
                {
                    fp.AddAccelerometer().Self();
                    fp.AddCamera().None();
                })
            );

            app.UseStaticFiles();
            app.UseMvc();
        }
    }
}
