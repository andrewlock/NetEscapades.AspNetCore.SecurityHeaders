﻿namespace NetEscapades.AspNetCore.SecurityHeaders.Headers.ContentSecurityPolicy;

/// <summary>
/// The <c>frame-src</c> directive specifies valid sources for nested browsing contexts loading
/// using elements such as  &lt;frame&gt; and  &lt;iframe&gt;
/// </summary>
public class FrameSourceDirectiveBuilder : CspDirectiveBuilder
{
    /// <summary>
    /// Initializes a new instance of the <see cref="FrameSourceDirectiveBuilder"/> class.
    /// </summary>
    public FrameSourceDirectiveBuilder() : base("frame-src")
    {
    }
}