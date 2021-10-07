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
#pragma warning disable 618 // Version_2_1 is obsolete
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
#pragma warning restore 618
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
#pragma warning disable 618 // IHostingEnvironment is obsolete
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
#pragma warning restore 618
        {
            var policyCollection = new HeaderPolicyCollection()
                .AddXssProtectionBlock()
                .AddContentTypeOptionsNoSniff()
                .AddExpectCTNoEnforceOrReport(0)
                .AddStrictTransportSecurityMaxAgeIncludeSubDomains(maxAgeInSeconds: 60 * 60 * 24 * 365) // maxage = one year in seconds
                .AddReferrerPolicyStrictOriginWhenCrossOrigin()
                .AddContentSecurityPolicy(builder =>
                {
                    builder.AddUpgradeInsecureRequests();
                    builder.AddDefaultSrc().Self(); 
                    builder.AddConnectSrc().From("*");
                    builder.AddFontSrc().From("*");
                    builder.AddFrameAncestors().From("*");
                    builder.AddFrameSrc().From("*");
                    builder.AddWorkerSrc().From("*");
                    builder.AddMediaSrc().From("*");
                    builder.AddImgSrc().From("*").Data();
                    builder.AddObjectSrc().From("*");
                    builder.AddScriptSrc().From("*").UnsafeInline().UnsafeEval();
                    builder.AddStyleSrc().From("*").UnsafeEval().UnsafeInline();
                })
                .RemoveServerHeader();


            app.UseSecurityHeaders(policyCollection);

            app.UseStaticFiles();
#if NETCOREAPP3_0
            app.UseRouting();
            app.UseEndpoints(endpoints => { endpoints.MapRazorPages(); });
#else
            app.UseMvc();
#endif
        }
    }
}
