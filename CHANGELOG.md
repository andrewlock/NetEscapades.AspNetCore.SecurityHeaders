# Changelog

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
