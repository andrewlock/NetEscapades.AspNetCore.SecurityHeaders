## Changes in 1.3.0:

Features:
* Add API for registering an async policy selector #259 (Thanks [@jchannon](https://github.com/jchannon))
* Update Content-Security-Policy builders to encourage correct directives and to flag incorrect directives #272
* Add `OverInsecureHttp()` and `OverInsecureWs()` scheme sources to CSP builders #273

## Changes in 1.2.0:

Features:
* Add support for `child-src` to `Content-Security-Policy` #259
* Allow multiple calls to `AddSecurityHeadersPolicies()` for registering named polices #250

Fixes:
* Fix typos in ReadMe #256 (Thanks [@jt-pt-dev](https://github.com/jt-pt-dev))

## Changes in 1.1.0

Features:
* Add `AddRange()` to `SourceCollection` #240, #242 (Thanks [@rankobp](https://github.com/rankobp))
* Improve CSP documentation #241 (Thanks [@Meir017](https://github.com/Meir017))
* Add `X-Frame-Options ALLOW-FROM` with correct method name `AddFrameOptionsAllowFrom()` #244

## Changes in 1.0.0

This marks the first major release of the _NetEscapades.AspNetCore.SecurityHeaders_. For simplicity, all the changes since 0.24.0 are included below.  

Breaking Changes:

* Drop support for .NET Standard 2.0, raises minimum framework to .NET Core 3.1 #167, #171
* Removed "document header" functionality, in favour of always adding all headers #186
* Remove `X-XSS-Protection` from default headers and mark obsolete #168
* Add `cross-origin-opener-policy: same-origin` to default headers #184
* Mark `Feature-Policy` as obsolete #187
* Mark `Expect-CT` as obsolete #197
* Make nonce generation lazy on call to `HttpContext.GetNonce()` #198
* Remove ambient-light-sensor=() from `DefaultSecureDirectives()` for permissions policy #203 (Thanks [damienbod](https://github.com/damienbod)!)
* Update COOP, COEP, and CORP for `AddDefaultSecurityHeaders()` and `AddDefaultApiSecurityHeaders()` #204 (Thanks [damienbod](https://github.com/damienbod)!)
* Removes obsolete APIs (#217)

Features:

* Allow configuring "named" policies, and applying different policies to different endpoints #172, #173, #185
* Allow customizing the `HeaderPolicyCollection` just before it is applied, customizing per request #174, #185
* Make adding directives to `Content-Security-Policy` idempotent to avoid duplicates #169
* Add `AddDefaultApiSecurityHeaders()` for adding default headers to APIs #183, #184
* Add `AddPermissionsPolicyWithRecommendedDirectives()` and `PermissionsPolicyBuilder.AddDefaultSecureDirectives()` for adding secure `Permissions-Policy` directives in bulk #183, #184
* NetEscapades.AspNetCore.SecurityHeaders now has an icon, thanks @khalidabuhakmeh! #195
* Allow accessing an `IServiceProvider` when configuring a `SecurityHeaderPolicyBuilder` #200
* Adds support for Trusted Types to Content-Security-Policy (#216, #218)

Build updates:

* Allow building from forks #232
* Fix release generation #231, #235, #236
* Fix recording test results #221
* Define version in the build project instead #223
* Generate SBOM #222
* Generate SBOM attestation #224
* Generate artifact provenance attestation #225
* Automatically create releases #229
