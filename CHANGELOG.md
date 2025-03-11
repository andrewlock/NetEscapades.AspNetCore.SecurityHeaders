# Changelog

## [v1.0.0-preview.4]

Build updates:

* Fix recording test results #221
* Define version in the build project instead #223
* Generate SBOM #222
* Generate SBOM attestation #224
* Generate artifact provenance attestation #225
* Automatically create releases #229

## [v1.0.0-preview.3]

Features:

* Adds support for Trusted Types to Content-Security-Policy (#216, #218)

Breaking Changes:

* Removes obsolete APIs (#217)

## [v1.0.0-preview.2]

Features:

* Allow accessing an `IServiceProvider` when configuring a `SecurityHeaderPolicyBuilder` #200

Fixes:

* Fix incorrect dependency on obsolete Microsoft.AspNetCore.Mvc.Razor package #205 (Thanks [trejjam](https://github.com/trejjam))

Breaking Changes:

* Remove ambient-light-sensor=() from `DefaultSecureDirectives()` for permissions policy #203 (Thanks [damienbod](https://github.com/damienbod)!)
* Update COOP, COEP, and CORP for `AddDefaultSecurityHeaders()` and `AddDefaultApiSecurityHeaders()` #204 (Thanks [damienbod](https://github.com/damienbod)!)

## [v1.0.0-preview.01]

Features:

* Allow configuring "named" policies, and applying different policies to different endpoints #172, #173, #185
* Allow customizing the `HeaderPolicyCollection` just before it is applied, customizing per request #174, #185
* Make adding directives to `Content-Security-Policy` idempotent to avoid duplicates #169
* Add `AddDefaultApiSecurityHeaders()` for adding default headers to APIs #183, #184
* Add `AddPermissionsPolicyWithRecommendedDirectives()` and `PermissionsPolicyBuilder.AddDefaultSecureDirectives()` for adding secure `Permissions-Policy` directives in bulk #183, #184
* NetEscapades.AspNetCore.SecurityHeaders now has an icon, thanks @khalidabuhakmeh! #195

Breaking Changes:

* Drop support for .NET Standard 2.0, raises minimum framework to .NET Core 3.1 #167, #171
* Removed "document header" functionality, in favour of always adding all headers #186
* Remove `X-XSS-Protection` from default headers and mark obsolete #168
* Add `cross-origin-opener-policy: same-origin` to default headers #184
* Mark `Feature-Policy` as obsolete #187
* Mark `Expect-CT` as obsolete #197
* Make nonce generation lazy on call to `HttpContext.GetNonce()` #198

## [v0.24.0]

Features:

* Allow adding multiple uris to CSP builder `AddFrameAncestors()` #179
* Add support for additional directives on `Permissions-Policy` header #177 (Thanks [@Registeel](https://github.com/Registeel)!)

## [v0.23.0]

Features:

* Add support for `unsafe-hashes` on `style` attributes, and inline event handlers #162 (Thanks [@tiesmaster](https://github.com/tiesmaster)!)

## [v0.22.0]

Features:

* Add support for `Cross-Origin-Embedder-Policy: credentialless` #153 (Thanks [RaceProUK](https://github.com/RaceProUK)!)

Bugfix:

* Fix documentation errors in `StyleSourceAttr` and `StyleSourceElem` directives #152 (Thanks [ThomasBjallas](https://github.com/ThomasBjallas)!)

## [v0.21.0]

Features:

* Add support for using both `'none'` and `'report-sample'` in directives

## [v0.20.0]

Features:

* Add support for `script-src-attr`, `script-src-elem`, `style-src-attr`, `style-src-elem` #139 

## [v0.19.0]

Features:

* Apply "document" headers to `text/javascript` responses 


## [v0.18.0]

Features:

* Add support for applying document headers (such as CSP) to all responses (#130)
* Add support for `unsafe-hashes` and `wasm-unsafe-eval` (#125)
* Add support for Report-To directive in Content-Security-Policy (#126)
* Add support for sandbox directive in Content-Security-Policy (#127)
* Add support for Reporting-Endpoints header (part of the Reporting API) (#128)

Bugfix:

* Document headers (such as CSP) are now applied to `application/javascript` in addition to `text/html` (#130)

## [v0.17.0]

Bugfix:

* Fix `Cross-Origin-Embedder-Policy` (COEP) not being added to non-HTML requests

## [v0.16.1]

Bugfix:

* Fix `Cross-Origin-Resource-Policy` (CORP) not being added to non-HTML requests

## [v0.16.0]

Features:

* Added support for `Cross-Origin-Opener-Policy` (COOP), `Cross-Origin-Embedder-Policy` (COEP) and `Cross-Origin-Resource-Policy` (CORP) (Thanks [@jeremylindsayni](https://github.com/jeremylindsayni)!)

## [v0.15.0]

Features:

* Add support for creating custom CSP directives with `CspDirectiveBuilder`. Enables creating custom directives (for example unsupported, draft, directives) that require nonce or hash values

BugFix:

* Add missing `EncryptedMedia` directive to permissions policy (Thanks [@jotoledo](https://github.com/jotatoledo))


## [v0.14.0]

Features:

* Add support for `interest-cohort=()` in `Permissions-Policy` directive (Thanks [@jeremylindsayni](https://github.com/jeremylindsayni)!)

BugFix: 

* Rename `AddFrameSource()` -> `AddFrameSrc()` for consistency, and deprecate `AddFrameSource()`

## [v0.13.0]

Features:

* Add support for `report-sample` in `style-src` directive for CSP (Thanks [@jeremylindsayni](https://github.com/jeremylindsayni)!)

## [v0.12.1]

BugFix:

* Fix API inconsistencies between Permissions-Policy and Feature-Policy (Thanks [@Rtalos](https://github.com/Rtalos)!)
 
## [v0.12.0]

Features:

* Add support for `manifest-src` directive in CSP (Thanks [@jotatoledo](https://github.com/jotatoledo)!)
* Add support for `Permissions-Policy` (supersedes `Feature-Policy`) (Thanks [@Rtalos](https://github.com/Rtalos)!)

## [v0.11.2]

Minor:

* Switch to standard MIT SPDX license

## [v0.11.0]

Features:

* Add support for `Expect-CT` header. Allows excluding domains that will not have the `Expect-CT` header applied. By default, the `Expect-CT` header will not be applied to localhost. It is also only applied to HTTPS requests  
* Add support for `worker-src` directive for `Content-Security-Policy` header

## [v0.10.0]

Breaking Changes:

* Drop support for ASP.NET Core 1.x
* Add support for ASP.NET Core 3.0

## [v0.9.0]

Features:

* Add support for Nonce generation for `Content-Security-Policy` headers. See [README.md](https://github.com/andrewlock/NetEscapades.AspNetCore.SecurityHeaders/blob/master/README.md#using-nonces-and-generated-hashes-with-content-security-policy) for details
* Add [TagHelpers](https://www.nuget.org/packages/NetEscapades.AspNetCore.SecurityHeaders.TagHelpers/) library for adding nonces and generating hashes for Razor elements. 
* Allow using HSTS preload with `Strict-Transport-Security`
* Allow excluding domains from `Strict-Transport-Security`. Similar to the [Microsoft `HstsMiddleware`](https://github.com/aspnet/BasicMiddleware/blob/master/src/Microsoft.AspNetCore.HttpsPolicy/HstsMiddleware.cs), you can skip applying `Strict-Transport-Security` to specific hosts

Breaking Changes:

* All obsolete classes have been removed.
* Many classes have changed namespace to better reflect their location in the project, and also to aid discovery. If you're using the recommended builders and extension methods, you should not have any build-time breaking changes, but the package is not runtime-compatible with previous versions
* The `Strict-Transport-Security` header is no longer applied to `localhost` by default. Generally speaking, this isn't something you should do anyway.
* The CSP classes have undergone significant refactoring to allow dynamic values per-request (i.e. nonces). This doesn't affect the main public API, but will impact you if you're working with the low-level infrastructure classes.

[v0.9.0]: https://github.com/andrewlock/NetEscapades.AspNetCore.SecurityHeaders/compare/v0.8.0...0.9.0
[v0.10.0]: https://github.com/andrewlock/NetEscapades.AspNetCore.SecurityHeaders/compare/v0.9.0...0.10.0
[v0.11.0]: https://github.com/andrewlock/NetEscapades.AspNetCore.SecurityHeaders/compare/v0.10.0...0.11.0
[v0.11.2]: https://github.com/andrewlock/NetEscapades.AspNetCore.SecurityHeaders/compare/v0.11.0...0.11.2
[v0.12.0]: https://github.com/andrewlock/NetEscapades.AspNetCore.SecurityHeaders/compare/v0.11.2...0.12.0
[v0.12.1]: https://github.com/andrewlock/NetEscapades.AspNetCore.SecurityHeaders/compare/v0.12.0...0.12.1
[v0.13.0]: https://github.com/andrewlock/NetEscapades.AspNetCore.SecurityHeaders/compare/v0.12.1...0.13.0
[v0.14.0]: https://github.com/andrewlock/NetEscapades.AspNetCore.SecurityHeaders/compare/v0.13.0...0.14.0
[v0.15.0]: https://github.com/andrewlock/NetEscapades.AspNetCore.SecurityHeaders/compare/v0.14.0...0.15.0
[v0.16.0]: https://github.com/andrewlock/NetEscapades.AspNetCore.SecurityHeaders/compare/v0.15.0...0.16.0
[v0.16.1]: https://github.com/andrewlock/NetEscapades.AspNetCore.SecurityHeaders/compare/v0.16.0...0.16.1
[v0.17.0]: https://github.com/andrewlock/NetEscapades.AspNetCore.SecurityHeaders/compare/v0.16.1...0.17.0
[v0.18.0]: https://github.com/andrewlock/NetEscapades.AspNetCore.SecurityHeaders/compare/v0.17.0...0.18.0
