# Changelog

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
