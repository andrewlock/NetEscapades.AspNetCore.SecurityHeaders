Guidance for AI Agents when working in this repository.

## Project overview

`NetEscapades.AspNetCore.SecurityHeaders` is a NuGet package providing ASP.NET Core middleware for adding security-related HTTP response headers (CSP, HSTS, Permissions-Policy, X-Frame-Options, Cross-Origin-*, etc.). Published to NuGet as two packages:

- `NetEscapades.AspNetCore.SecurityHeaders` — the main middleware
- `NetEscapades.AspNetCore.SecurityHeaders.TagHelpers` — Razor tag helpers for CSP nonces and inline script/style hashes

## Repo layout

```
src/
  NetEscapades.AspNetCore.SecurityHeaders/            Main library (middleware, policies, header builders)
    Headers/                                           Header types (CSP, HSTS, Permissions-Policy, ...)
    Infrastructure/                                    Middleware plumbing, CustomHeaderService
  NetEscapades.AspNetCore.SecurityHeaders.TagHelpers/ Razor tag helpers (Nonce, Hash, AttributeHash)
  NetEscapades.AspNetCore.SecurityHeaders.Analyzers/  Roslyn analyzers shipped with the main package
  SourceGenerator/                                    Incremental source generator for CSP builders
test/
  *.Test/                                              xUnit test projects
  RazorWebSite/, SecurityHeadersMiddlewareWebSite/     Test web apps used via TestServer
build/                                                 Nuke build definition (_build.csproj, Build.cs)
```

The `Analyzers` and `SourceGenerator` projects are referenced as `OutputItemType="Analyzer"` / `PrivateAssets="All"` — they ship inside the main package, not as separate NuGet dependencies.

Generated source files are emitted to `src/NetEscapades.AspNetCore.SecurityHeaders/Generated/<tfm>/` (`EmitCompilerGeneratedFiles=true`). They are excluded from compilation but visible in the IDE — do not edit them; change the generator instead.

## Build and test

- SDK pinned in `global.json`: **.NET 10.0.101** (no prerelease). Install matching SDK before building.
- Use the Nuke wrappers, not raw `dotnet`:
  - `./build.sh` (Linux/macOS) or `build.cmd` (Windows) — default target runs `Test` + `Pack`
  - Targets: `Clean`, `Restore`, `Compile`, `Test`, `Pack`, `GenerateSbom`, `PushToNuGet`
  - Example: `./build.sh Test` or `./build.sh Compile --configuration Release`
- Ad-hoc `dotnet test NetEscapades.AspNetCore.SecurityHeaders.sln` also works for quick iteration.

### Target frameworks

- Main library: `netcoreapp3.1` only (single-target — do not add more TFMs without discussion; it affects analyzer/source-generator packaging).
- Test projects: `net6.0;net8.0;net9.0;net10.0` (plus `netcoreapp3.1` on Windows only — Linux CI skips it due to missing libssl).

## Code conventions enforced by the build

- `TreatWarningsAsErrors=true` on the main library — any new warning fails the build.
- `Nullable` and `ImplicitUsings` are **enabled** on the main library.
- `GenerateDocumentationFile=true` — public APIs must have XML doc comments or StyleCop/CS warnings will fail the build.
- StyleCop.Analyzers is active with a custom ruleset (`NetEscapades.AspNetCore.SecurityHeaders.ruleset`) and `stylecop.json`.
- `LangVersion=latest`.

## Where to add things

- **New header type** → `src/NetEscapades.AspNetCore.SecurityHeaders/Headers/` with a `*Header.cs` + `*HeaderExtensions.cs` pair; register via extensions on `HeaderPolicyCollection`. Include an MDN reference on the header class, builder, and extension class — use `/// <seealso href="https://developer.mozilla.org/en-US/docs/Web/HTTP/Reference/Headers/<header-name>"/>` (matches the existing convention in `Cross-Origin-Opener-Policy` and `Clear-Site-Data` builders).
- **New CSP directive** → usually needs changes in `src/SourceGenerator/` (the directive builders are generated). Check `ContentSecurityPolicyGenerator.cs` and `SourceGenerationHelper.cs`.
- **Deprecation / insecure API warnings** → `src/NetEscapades.AspNetCore.SecurityHeaders.Analyzers/` and the `[Deprecated]` / `[InsecureApi]` attributes in `Helpers/`.
- **Tests** → prefer adding to `NetEscapades.AspNetCore.SecurityHeaders.Test` using `TestServer` against the sample web apps rather than unit-testing middleware internals directly.

## Release / versioning

- Version is currently hardcoded in `build/Build.cs` (`readonly string Version = "1.3.1"`) — bump there, not in csproj files.
- `ReleaseNotes.md` is embedded into `PackageReleaseNotes` via `Directory.Build.props`. Update `CHANGELOG.md` and `ReleaseNotes.md` on version bumps.
- Publishing is gated on a git tag + CI + Windows (`PushToNuGet` target). Releases include provenance attestations and CycloneDX SBOMs — don't bypass the CI pipeline for a release.
