﻿using System;
using Microsoft.AspNetCore.Http;

namespace NetEscapades.AspNetCore.SecurityHeaders.Headers.ContentSecurityPolicy;

/// <summary>
/// The <c>block-all-mixed-content</c> directive prevents loading any assets using
/// HTTP when the page is loaded using HTTPS.
///
/// All mixed content resource requests are blocked, including both active
/// and passive mixed content. This also applies to &lt;iframe&gt; documents,
/// ensuring the entire page is mixed content free.
///
/// The <c>upgrade-insecure-requests</c> directive is evaluated before <c>block-all-mixed-content</c>
/// and If the former is set, the latter is effectively a no-op.
/// It is recommended to set one directive or the other – not both.
/// </summary>
public class BlockAllMixedContentDirectiveBuilder : CspDirectiveBuilderBase
{
    /// <summary>
    /// Initializes a new instance of the <see cref="BlockAllMixedContentDirectiveBuilder"/> class.
    /// </summary>
    public BlockAllMixedContentDirectiveBuilder()
        : base("block-all-mixed-content")
    {
    }

    /// <inheritdoc />
    internal override Func<HttpContext, string> CreateBuilder()
    {
        return ctx => Directive;
    }
}