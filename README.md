# NetEscapades.AspNetCore.SecurityHeaders

[![Build status](https://ci.appveyor.com/api/projects/status/q261l3sbokafmx1o/branch/develop?svg=true)](https://ci.appveyor.com/project/andrewlock/netescapades-aspnetcore-securityheaders/branch/master)
<!--[![Travis](https://img.shields.io/travis/andrewlock/NetEscapades.AspNetCore.SecurityHeaders.svg?maxAge=3600&label=travis)](https://travis-ci.org/andrewlock/NetEscapades.AspNetCore.SecurityHeaders)-->
[![NuGet](https://img.shields.io/nuget/v/NetEscapades.AspNetCore.SecurityHeaders.svg)](https://www.nuget.org/packages/NetEscapades.AspNetCore.SecurityHeaders/)
[![MyGet CI](https://img.shields.io/myget/andrewlock-ci/v/NetEscapades.AspNetCore.SecurityHeaders.svg)](http://myget.org/gallery/acndrewlock-ci)

A small package to allow adding security headers to ASP.NET Core websites

## Installing 

Install using the [NetEscapades.AspNetCore.SecurityHeaders NuGet package](https://www.nuget.org/packages/NetEscapades.AspNetCore.SecurityHeaders) from the Visual Studio Package Manager Console:

```
PM> Install-Package NetEscapades.AspNetCore.SecurityHeaders
```

Or using the `dotnet` CLI

```
dotnet package add Install-Package NetEscapades.AspNetCore.SecurityHeaders
```

## Usage 

When you install the package, it should be added to your `.csproj`. Alternatively, you can add it directly by adding:


```xml
<Project Sdk="Microsoft.NET.Sdk.Web">

  <PropertyGroup>
    <TargetFramework>netcoreapp2.0</TargetFramework>
  </PropertyGroup>

  <ItemGroup>
    <PackageReference Include="NetEscapades.AspNetCore.SecurityHeaders" Version="0.5.0" />
  </ItemGroup>
  
</Project>
```

In order to use the security headers middleware, you must configure the services in the `ConfigureServices` call of `Startup`: 

```csharp
public void ConfigureServices(IServiceCollection services)
{
    services.AddSecurityHeaders();
}
```

You can then add the middleware to your ASP.NET Core application by configuring it as part of your normal `Startup` pipeline. Note that the order of middleware matters, so to apply the headers to all requests it should be configured first in your pipeline.

To use the default security headers for your application, add the middleware using:

```csharp
public void Configure(IApplicationBuilder app)
{
    app.UseSecurityHeaders()
    
    // other middleware e.g. static files, MVC etc  
}
```

This adds the following headers to all responses that pass through the middleware:

* `X-Content-Type-Options: nosniff`
* `Strict-Transport-Security: max-age=31536000; includeSubDomains` - _only applied to HTTPS responses_
* `X-Frame-Options: Deny` - _only applied to `text/html` responses_
* `X-XSS-Protection: 1; mode=block` - _only applied to `text/html` responses_
* `Referrer-Policy: origin-when-cross-origin` - _only applied to `text/html` responses_
* `Content-Security-Policy: object-src 'none'; form-action 'self'; frame-ancestors 'none'` - _only applied to `text/html` responses_

## Customising the security headers added to reponses

To customise the headers returned, you should create an instance of a `HeaderPolicyCollection` and add the required policies to it. There are helper methods for adding a number of security-focused header values to the collection, or you can alternatively add any header by using the `CustomHeader` type. For example, the following would set a number of security headers, and a custom header `X-My-Test-Header`. 

```csharp
public void Configure(IApplicationBuilder app)
{
    var policyCollection = new HeaderPolicyCollection()
        .AddFrameOptionsDeny()
        .AddXssProtectionBlock()
        .AddContentTypeOptionsNoSniff()
        .AddStrictTransportSecurityMaxAgeIncludeSubDomains(maxAge: 60 * 60 * 24 * 365) // maxage = one year in seconds
        .AddReferrerPolicyOriginWhenCrossOrigin()
        .RemoveServerHeader()
        .AddContentSecurityPolicy(builder =>
        {
            builder.AddObjectSrc().None();
            builder.AddFormAction().Self();
            builder.AddFrameAncestors().None();
        });
        .AddCustomHeader("X-My-Test-Header", "Header value");
    
    app.UseSecurityHeaders(policyCollection)
    
    // other middleware e.g. static files, MVC etc  
}
```

The security headers above are also encapsulated in another extension method, so you could rewrite it more tersely using 

```csharp
public void Configure(IApplicationBuilder app)
{
    var policyCollection = new HeaderPolicyCollection()
        .AddDefaultSecurityHeaders()
        .AddCustomHeader("X-My-Test-Header", "Header value");
    
    app.UseSecurityHeaders(policyCollection)
    
    // other middleware e.g. static files, MVC etc  
}
```

If you want to use the default security headers, but change one specific header, you can simply add another header to the default collection. For example, the following uses the default headers, but changes the max-age on the `Strict-Transport-Security` header:

```csharp
public void Configure(IApplicationBuilder app)
{
    var policyCollection = new HeaderPolicyCollection()
        .AddDefaultSecurityHeaders()
        .AddStrictTransportSecurityMaxAgeIncludeSubDomains(maxAge: 63072000);
    
    app.UseSecurityHeaders(policyCollection)
    
    // other middleware e.g. static files, MVC etc  
}
```

## RemoveServerHeader

One point to be aware of is that the `RemoveServerHeader` method will rarely (ever?) be sufficient to remove the `Server` header from your output. If any subsequent middleware in your application pipeline add the header, then this will be able to remove it. However Kestrel will generally add the `Server` header too late in the pipeline to be able to modify it. 

Luckily, Kestrel exposes it's own mechanism to allow you to prevent it being added:

```csharp
var host = new WebHostBuilder()
    .UseKestrel(options => options.AddServerHeader = false)
    //...
```

In `Program.cs`, when constructing your app's `WebHostBuilder`, configure the `KestrelServerOptions` to prevent the `Server` tag being added.

## AddContentSecurityPolicy

The `Content-Security-Policy` (CSP) headder is a very powerful header that can protect your website from a wide range of attacks. However, it's also totally possible to create a CSP header that completely breaks your app. 

The CSP has a dizzying array of options, only some of which are implemented in this project. Consequently, I highly recommend reading [this post by Scott Helme](https://scotthelme.co.uk/content-security-policy-an-introduction/), in which he discusses the impact of each "directive". I also highly recommend using the "report only" version of the header when you start. This won't break your site, but will report instances that it would be broken, by providing reports to a service such as report-uri.com.

Set the header to report-only by using the `AddContentSecurityPolicyReportOnly()` extension. For example:


```csharp
public void Configure(IApplicationBuilder app)
{
    var policyCollection = new HeaderPolicyCollection()
        .AddContentSecurityPolicyReportOnly(builder => // report-only 
        {
            // configure policies
        }); 
}
```

or by by passing `true` to the `AddContentSecurityPolicy` command

```csharp
public void Configure(IApplicationBuilder app)
{
    var policyCollection = new HeaderPolicyCollection()
        .AddContentSecurityPolicy(builder =>
        {
            // configure policies
        },
        asReportOnly: true); // report-only 
}
```

You configure your CSP policy when you configure your `HeaderPolicyCollection` in `Startup.Configure`. For example:

```csharp
public void Configure(IApplicationBuilder app)
{
    var policyCollection = new HeaderPolicyCollection()
        .AddContentSecurityPolicy(builder =>
        {
            builder.AddUpgradeInsecureRequests() // upgrade-insecure-requests

            builder.AddReportUri() // report-uri: https://report-uri.com
                .To("https://report-uri.com");

            builder.AddDefaultSrc() // default-src 'self' http://testUrl.com
                .Self()
                .From("http://testUrl.com");

            builder.AddConnectSrc() // connect-src 'self' http://testUrl.com
                .Self()
                .From("http://testUrl.com");

            builder.AddFontSrc() // font-src 'self'
                .Self();

            builder.AddObjectSrc() // object-src 'none'
                .None();
                
            builder.AddFormAction() // form-action 'self'
                .Self();
                
            builder.AddImgSrc() // img-src https:
                .OverHttps();
            
            builder.AddScriptSrc() // script-src 'self'
                .Self();
            
            builder.AddStyleSrc() // style-src 'self'
                .Self();
            
            builder.AddMediaSrc() // media-src https:
                .OverHttps();

            builder.AddFrameAncestors() // frame-ancestors 'none'
                .None();
                
            builder.AddFrameSource() // frame-src http://testUrl.com
                .From("http://testUrl.com");
            
            // You can also add arbitrary extra directives: plugin-types application/x-shockwave-flash"
            builder.AddCustomDirective("plugin-types", "application/x-shockwave-flash");
            
        });
        .AddCustomHeader("X-My-Test-Header", "Header value");
    
    app.UseSecurityHeaders(policyCollection)
    
    // other middleware e.g. static files, MVC etc  
}
```


## Additional Resources
* [ASP.NET Core Middleware Docs](https://docs.asp.net/en/latest/fundamentals/middleware.html)
* [How to add default security headers in ASP.NET Core using custom middleware](http://andrewlock.net/adding-default-security-headers-in-asp-net-core/)
* [Content Security Policy - An Introduction](https://scotthelme.co.uk/content-security-policy-an-introduction/) by Scott Helme
* [Content Security Policy Reference](https://content-security-policy.com/)
* [Content Security Policy (CSP)](https://developer.mozilla.org/en-US/docs/Web/HTTP/CSP) by Mozilla Developer Network

> Note, Building on Travis is currently disabled, due to issues with the mono framework. For details, see
> * http://stackoverflow.com/questions/42747722/building-vs-2017-msbuild-csproj-projects-with-mono-on-linux/42861338
> * https://github.com/dotnet/sdk/issues/335
