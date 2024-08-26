using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;

namespace RazorWebSite;

public class Startup
{
    // This method gets called by the runtime. Use this method to add services to the container.
    public void ConfigureServices(IServiceCollection services)
    {
        services.AddMvc();
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
                builder.AddScriptSrc().From("*")
                    .WithHashTagHelper()
                    .WithNonce()
                    .UnsafeHashes()
                    .WithHash256("j/kWZ/y+DZQOTLHQxWwAAu/hg+GGWVXxmPixUq26wnc=") // Allow the jquery asp-fallback-test script element
                    .WithHash256("WuuOVwpUdf7Fb0r2WZxkqiv5V457zV2zpgSjN0Jy63Q="); // Allow the bootstrap asp-fallback-test script element
                builder.AddStyleSrc().From("*").WithHashTagHelper().UnsafeHashes();
            })
            .RemoveServerHeader();


        app.UseSecurityHeaders(policyCollection);

        app.UseStaticFiles();
        app.UseRouting();
        app.UseEndpoints(endpoints => { endpoints.MapRazorPages(); });
    }
}