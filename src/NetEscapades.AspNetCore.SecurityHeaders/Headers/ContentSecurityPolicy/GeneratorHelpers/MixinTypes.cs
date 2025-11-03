using System;

namespace NetEscapades.AspNetCore.SecurityHeaders.Headers.ContentSecurityPolicy;

/// <summary>
/// Used by source generator to generate mixins for CSP headers
/// </summary>
[Flags]
internal enum MixinTypes
{
    /// <summary>
    /// Adds the <c>'self'</c> source, allowing resources from the same origin.
    /// </summary>
    Self = 1 << 0,

    /// <summary>
    /// Adds one or more host sources (e.g. <c>https://cdn.example.com</c>).
    /// </summary>
    HostSource = 1 << 1,

    /// <summary>
    /// Adds scheme sources (e.g. <c>https:</c>, <c>data:</c>, <c>blob:</c>).
    /// </summary>
    SchemeSource = 1 << 2,

    /// <summary>
    /// Adds the <c>'unsafe-eval'</c> source to permit eval-like constructs.
    /// </summary>
    UnsafeEval = 1 << 3,

    /// <summary>
    /// Adds the <c>'unsafe-inline'</c> source to permit inline scripts/styles.
    /// </summary>
    UnsafeInline = 1 << 4,

    /// <summary>
    /// Adds the <c>'unsafe-hashes'</c> source to allow inline event handlers when accompanied by valid hashes.
    /// </summary>
    UnsafeHashes = 1 << 5,

    /// <summary>
    /// Adds a <c>nonce-...</c> source to allow nonced scripts/styles.
    /// </summary>
    Nonce = 1 << 6,

    /// <summary>
    /// Adds hash sources (e.g. <c>sha256-...</c>, <c>sha384-...</c>, <c>sha512-...</c>).
    /// </summary>
    Hash = 1 << 7,

    /// <summary>
    /// Adds the <c>'report-sample'</c> token to include code samples in violation reports.
    /// </summary>
    ReportSample = 1 << 8,

    /// <summary>
    /// Adds the <c>'wasm-unsafe-eval'</c> token to permit WebAssembly eval-like behavior.
    /// </summary>
    WasmUnsafeEval = 1 << 9,

    /// <summary>
    /// Adds the <c>'inline-speculation-rules'</c> token to allow inline speculation rules.
    /// </summary>
    InlineSpeculationRules = 1 << 10,

    /// <summary>
    /// Adds the <c>'strict-dynamic'</c> token to trust dynamically added scripts when a nonce or hash is present.
    /// </summary>
    StrictDynamic = 1 << 11,

    /// <summary>
    /// Adds the <c>'none'</c> source, blocking all sources for the directive.
    /// </summary>
    None = 1 << 12,

    /// <summary>
    /// Convenience combination of all mixin flags.
    /// </summary>
    All = Self |
          HostSource |
          SchemeSource |
          UnsafeEval |
          UnsafeInline |
          UnsafeHashes |
          Nonce |
          Hash |
          ReportSample |
          WasmUnsafeEval |
          InlineSpeculationRules |
          StrictDynamic |
          None,
}