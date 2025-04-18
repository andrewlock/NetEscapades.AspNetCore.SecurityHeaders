﻿namespace NetEscapades.AspNetCore.SecurityHeaders.Headers.ContentSecurityPolicy;

/// <summary>
/// The <c>img-src</c> directive specifies valid sources of images and favicons.
/// </summary>
public class ImageSourceDirectiveBuilder : CspDirectiveBuilder
{
    /// <summary>
    /// Initializes a new instance of the <see cref="ImageSourceDirectiveBuilder"/> class.
    /// </summary>
    public ImageSourceDirectiveBuilder() : base("img-src")
    {
    }
}