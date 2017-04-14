# NetEscapades.AspNetCore.SecurityHeaders

[![Build status](https://ci.appveyor.com/api/projects/status/q261l3sbokafmx1o/branch/develop?svg=true)](https://ci.appveyor.com/project/andrewlock/netescapades-aspnetcore-securityheaders/branch/master)
<!--[![Travis](https://img.shields.io/travis/andrewlock/NetEscapades.AspNetCore.SecurityHeaders.svg?maxAge=3600&label=travis)](https://travis-ci.org/andrewlock/NetEscapades.AspNetCore.SecurityHeaders)-->
[![NuGet](https://img.shields.io/nuget/v/NetEscapades.AspNetCore.SecurityHeaders.svg)](https://www.nuget.org/packages/NetEscapades.AspNetCore.SecurityHeaders/)
[![MyGet CI](https://img.shields.io/myget/andrewlock-ci/v/NetEscapades.AspNetCore.SecurityHeaders.svg)](http://myget.org/gallery/acndrewlock-ci)

A small package to allow adding security headers to ASP.NET Core websites

## Installing 

Install using the [NetEscapades.AspNetCore.SecurityHeaders NuGet package](https://www.nuget.org/packages/NetEscapades.AspNetCore.SecurityHeaders) (currently in beta):

```
PM> Install-Package NetEscapades.AspNetCore.SecurityHeaders -Pre
```

## Usage 

When you install the package, it should be added to your `package.json`. Alternatively, you can add it directly by adding:


```json
{
  "dependencies" : {
    "NetEscapades.AspNetCore.SecurityHeaders": "0.3.1"
  }
}
```

In order to use the CustomHeader middleware, you must configure the services in the `ConfigureServices` call of `Startup`: 

```csharp
public void ConfigureServices(IServiceCollection services)
{
    services.AddCustomHeaders();
}
```

You can then add the middleware to your ASP.NET Core application by configuring it as part of your normal `Startup` pipeline. Note that the order of middleware matters, so to apply the headers to all requests it should be configured first in your pipeline.

To configure the middleware, you should create an instance of a `HeaderPolicyCollection` and add the required policies to it. There are helper methods for adding a number of security-focused header values to the collection, or you can alternatively add any header by using the `CustomHeader` type. For example, the following would set a number of security headers, and a custom header `X-My-Test-Header`. 

```csharp
public void Configure(IApplicationBuilder app)
{
    var policyCollection = new HeaderPolicyCollection()
        .AddFrameOptionsDeny()
        .AddXssProtectionBlock()
        .AddContentTypeOptionsNoSniff()
        .AddStrictTransportSecurityMaxAge(maxAge = 60 * 60 * 24 * 365) // maxage = one year in seconds
        .AddReferrerPolicyOriginWhenCrossOrigin();
        .RemoveServerHeader()
        .AddCustomHeader("X-My-Test-Header", "Header value");
    
    app.UseCustomHeadersMiddleware(policyCollection)
    
    // other middleware e.g. logging, MVC etc  
}
```

The security headers above are also encapsulated in another extension method, so you could rewrite it more tersely using 

```csharp
public void Configure(IApplicationBuilder app)
{
    var policyCollection = new HeaderPolicyCollection()
        .AddDefaultSecurityHeaders()
        .AddCustomHeader("X-My-Test-Header", "Header value");
    
    app.UseCustomHeadersMiddleware(policyCollection)
    
    // other middleware e.g. logging, MVC etc  
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

## Additional Resources
* [ASP.NET Core Middleware Docs](https://docs.asp.net/en/latest/fundamentals/middleware.html)
* [How to add default security headers in ASP.NET Core using custom middleware](http://andrewlock.net/adding-default-security-headers-in-asp-net-core/)

> Note, Building on Travis is currently disabled, due to issues with the mono framework. For details, see
> * http://stackoverflow.com/questions/42747722/building-vs-2017-msbuild-csproj-projects-with-mono-on-linux/42861338
> * https://github.com/dotnet/sdk/issues/335