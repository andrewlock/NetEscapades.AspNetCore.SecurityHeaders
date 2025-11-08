using System.Text;
using NetEscapades.AspNetCore.SecurityHeaders.Headers.ContentSecurityPolicy;

namespace SourceGenerator;

internal static class SourceGenerationHelper
{
    public static (string Content, string HintName) GeneratePartials(ContentSecurityPolicyGenerator.MixinToGenerate toGenerate)
    {
        var sb = new StringBuilder(Constants.Header);
        var mixins = toGenerate.Mixins;

        sb.Append(
            //lang=csharp
            $$"""
            
            namespace {{toGenerate.NameSpace}};
            
            public partial class {{toGenerate.ClassName}}
            {
            """);

        if (Contains(mixins, MixinTypes.None))
        {
            //lang=csharp
            sb.AppendLine(
                $$"""

                      /// <summary>
                      /// Block the resource (Refers to the empty set; that is, no URLs match)
                      /// </summary>
                      /// <returns>The builder for method chaining</returns>
                      public {{toGenerate.ClassName}} None()
                      {
                          BlockResources = true;
                          return this;
                      }
                  """);
        }

        if (Contains(mixins, MixinTypes.Self))
        {
            sb.AppendLine(
                //lang=csharp
                $$"""

                      /// <summary>
                      /// Allow sources from the origin from which the protected document is being served, including the same URL scheme and port number.
                      /// </summary>
                      /// <returns>The CSP builder for method chaining</returns>
                      public {{toGenerate.ClassName}} Self()
                      {
                          Sources.Add("'self'");
                          return this;
                      }
                  """);
        }

        if (Contains(mixins, MixinTypes.HostSource))
        {
            sb.AppendLine(
                //lang=csharp
                $$"""

                      /// <summary>
                      /// Allow resources from the given <paramref name="uri"/>. May be any non-empty value
                      /// </summary>
                      /// <param name="uri">The URI to allow.</param>
                      /// <returns>The CSP builder for method chaining</returns>
                      public {{toGenerate.ClassName}} From(string uri)
                      {
                          if (string.IsNullOrWhiteSpace(uri))
                          {
                              throw new System.ArgumentException("Uri may not be null or empty", nameof(uri));
                          }

                          Sources.Add(uri);
                          return this;
                      }

                      /// <summary>
                      /// Allow resources from the given IEnumerable <paramref name="uris"/>. Elements may be any non-empty value
                      /// </summary>
                      /// <param name="uris">An IEnumerable of URIs to allow.</param>
                      /// <returns>The CSP builder for method chaining</returns>
                      public {{toGenerate.ClassName}} From(System.Collections.Generic.IEnumerable<string> uris)
                      {
                          if (uris == null)
                          {
                              throw new System.ArgumentException("Uris may not be null or empty", "uris");
                          }

                          foreach (var uri in uris)
                          {
                              From(uri);
                          }

                          return this;
                      }
                  """);
        }

        if (Contains(mixins, MixinTypes.SchemeSource))
        {
            sb.AppendLine(
                //lang=csharp
                $$"""
                      /// <summary>
                      /// Allows blob: URIs to be used as a content source
                      /// </summary>
                      /// <returns>The CSP builder for method chaining</returns>
                      public {{toGenerate.ClassName}} Blob()
                      {
                          Sources.Add("blob:");
                          return this;
                      }

                      /// <summary>
                      /// data: Allows data: URIs to be used as a content source.
                      /// WARNING: This is insecure; an attacker can also inject arbitrary data: URIs. Use this sparingly and definitely not for scripts.
                      /// </summary>
                      /// <returns>The CSP builder for method chaining</returns>
                      public {{toGenerate.ClassName}} Data()
                      {
                          Sources.Add("data:");
                          return this;
                      }
                  
                      /// <summary>
                      /// Allow resources served over https:
                      /// </summary>
                      /// <returns>The CSP builder for method chaining</returns>
                      public {{toGenerate.ClassName}} OverHttps()
                      {
                          Sources.Add("https:");
                          return this;
                      }

                      /// <summary>
                      /// Allow resources served over wss:
                      /// </summary>
                      /// <returns>The CSP builder for method chaining</returns>
                      public {{toGenerate.ClassName}} OverWss()
                      {
                          Sources.Add("wss:");
                          return this;
                      }

                      /// <summary>
                      /// Allow resources served over http: This also allows resources served over https://  
                      /// </summary>
                      /// <returns>The CSP builder for method chaining</returns>
                      [global::NetEscapades.AspNetCore.SecurityHeaders.Helpers.InsecureApi("This API allows the use of the insecure HTTP scheme. Consider calling OverHttps() instead")]
                      public {{toGenerate.ClassName}} OverInsecureHttp()
                      {
                          Sources.Add("http:");
                          return this;
                      }
                  
                      /// <summary>
                      /// Allow resources served over ws:This also allows resources served over wss://
                      /// </summary>
                      /// <returns>The CSP builder for method chaining</returns>
                      [global::NetEscapades.AspNetCore.SecurityHeaders.Helpers.InsecureApi("This API allows the use of the insecure websocket scheme. Consider calling OverWss() instead")]
                      public {{toGenerate.ClassName}} OverInsecureWs()
                      {
                          Sources.Add("ws:");
                          return this;
                      }
                  """);
        }

        if (Contains(mixins, MixinTypes.Hash))
        {
            sb.AppendLine(
                $$"""

                      /// <summary>
                      /// A sha256, sha384 or sha512 hash of scripts or styles.
                      /// Allow sources that match the provided hash.
                      /// </summary>
                      /// <param name="algorithm">The hash algorithm - one of sha256, sha384 or sha512 </param>
                      /// <param name="hash">The hash for the source</param>
                      /// <returns>The CSP builder for method chaining</returns>
                      public {{toGenerate.ClassName}} WithHash(string algorithm, string hash)
                      {
                          // TODO: check hash algorithm is one of expected values
                          Sources.Add($"'{algorithm}-{hash}'");
                          return this;
                      }
    
                      /// <summary>
                      /// A sha256 hash of scripts or styles.
                      /// Allow sources that match the provided hash.
                      /// </summary>
                      /// <param name="hash">The hash for the source</param>
                      /// <returns>The CSP builder for method chaining</returns>
                      public {{toGenerate.ClassName}} WithHash256(string hash)
                      {
                          return WithHash("sha256", hash);
                      }

                      /// <summary>
                      /// A sha384 hash of scripts or styles.
                      /// Allow sources that match the provided hash.
                      /// </summary>
                      /// <param name="hash">The hash for the source</param>
                      /// <returns>The CSP builder for method chaining</returns>
                      public {{toGenerate.ClassName}} WithHashSha384(string hash)
                      {
                          return WithHash("sha384", hash);
                      }

                      /// <summary>
                      /// A sha512 hash of scripts or styles.
                      /// Allow sources that match the provided hash.
                      /// </summary>
                      /// <param name="hash">The hash for the source</param>
                      /// <returns>The CSP builder for method chaining</returns>
                      public {{toGenerate.ClassName}} WithHashSha512(string hash)
                      {
                          return WithHash("sha512", hash);
                      }
                  
                      /// <summary>
                      /// Allow sources for content generated using the HashTagHelper.
                      /// </summary>
                      /// <returns>The CSP builder for method chaining</returns>
                      public {{toGenerate.ClassName}} WithHashTagHelper()
                      {
                          SourceBuilders.Add(
                              ctx => string.Join(" ", ctx.GetScriptCSPHashes()),
                              $"{{toGenerate.NameSpace}}.{{toGenerate.ClassName}}.{nameof(WithHashTagHelper)}");
                          return this;
                      }
                  """);
        }

        if (Contains(mixins, MixinTypes.UnsafeInline))
        {
            sb.AppendLine(
                $$"""

                      /// <summary>
                      /// Allows the use of inline resources, such as inline &lt;scripT&gt; elements, javascript : URLs,
                      /// inline event handlers, and inline &lt;style&gt; elements
                      /// WARNING: This source is insecure - you should not use this directive if at all possible
                      /// </summary>
                      /// <returns>The CSP builder for method chaining</returns>
                      public {{toGenerate.ClassName}} UnsafeInline()
                      {
                          Sources.Add("'unsafe-inline'");
                          return this;
                      }
                  """);
        }

        if (Contains(mixins, MixinTypes.UnsafeHashes))
        {
            sb.AppendLine(
                $$"""

                      /// <summary>
                      /// Allows enabling specific inline event handlers. If you only need to allow inline
                      /// event handlers and not inline &lt;script&gt; elements or javascript: URLs,
                      /// this is a safer method than using the unsafe-inline expression.
                      /// WARNING: This source is insecure - you should not use this directive if at all possible
                      /// </summary>
                      /// <returns>The CSP builder for method chaining</returns>
                      public {{toGenerate.ClassName}} UnsafeHashes()
                      {
                          Sources.Add("'unsafe-hashes'");
                          return this;
                      }
                  """);
        }

        if (Contains(mixins, MixinTypes.UnsafeEval))
        {
            sb.AppendLine(
                $$"""

                      /// <summary>
                      /// Allows the use of eval() and similar methods for creating code from strings.
                      /// WARNING: This source is insecure - you should not use this directive if at all possible
                      /// </summary>
                      /// <returns>The CSP builder for method chaining</returns>
                      public {{toGenerate.ClassName}} UnsafeEval()
                      {
                          Sources.Add("'unsafe-eval'");
                          return this;
                      }
                  """);
        }

        if (Contains(mixins, MixinTypes.WasmUnsafeEval))
        {
            sb.AppendLine(
                $$"""

                      /// <summary>
                      /// Allows the loading and execution of WebAssembly modules without the need to also
                      /// allow unsafe JavaScript execution via 'unsafe-eval'.
                      /// WARNING: This source is insecure - you should not use this directive if at all possible
                      /// </summary>
                      /// <returns>The CSP builder for method chaining</returns>
                      public {{toGenerate.ClassName}} WasmUnsafeEval()
                      {
                          Sources.Add("'wasm-unsafe-eval'");
                          return this;
                      }
                  """);
        }

        if (Contains(mixins, MixinTypes.StrictDynamic))
        {
            sb.AppendLine(
                $$"""

                      /// <summary>
                      /// The strict-dynamic source expression specifies that the trust explicitly given to a
                      /// script present in the markup, by accompanying it with a nonce or a hash, shall be
                      /// propagated to all the scripts loaded by that root script. At the same time, any
                      /// whitelist or source expressions such as 'self' or 'unsafe-inline' will be ignored.
                      /// </summary>
                      /// <returns>The CSP builder for method chaining</returns>
                      public {{toGenerate.ClassName}} StrictDynamic()
                      {
                          Sources.Add("'strict-dynamic'");
                          return this;
                      }
                  """);
        }

        if (Contains(mixins, MixinTypes.InlineSpeculationRules))
        {
            sb.AppendLine(
                //lang=csharp
                $$"""

                      
                      /// <summary>
                      /// Allows the browser to load inline &lt;script&gt; elements that have a type attribute of speculationrules.
                      /// </summary>
                      /// <seealso href="https://developer.mozilla.org/en-US/docs/Web/API/Speculation_Rules_API"/>
                      /// <returns>The CSP builder for method chaining</returns>
                      public {{toGenerate.ClassName}} InlineSpeculationRules()
                      {
                          Sources.Add("'inline-speculation-rules'");
                          return this;
                      }
                  """);
        }

        if (Contains(mixins, MixinTypes.Nonce))
        {
            sb.AppendLine(
                $$"""

                      /// <summary>
                      /// A whitelist for specific inline scripts using a cryptographic nonce (number used once).
                      /// The server generates a unique nonce value for each request. Specifying a nonce makes a
                      /// modern browser ignore 'unsafe-inline' which could still be set for older browsers
                      /// without nonce support.
                      /// </summary>
                      /// <returns>The CSP builder for method chaining</returns>
                      public {{toGenerate.ClassName}} WithNonce()
                      {
                          // The format parameter will be replaced with the nonce for each request
                          SourceBuilders.Add(
                              ctx => $"'nonce-{ctx.GetNonce()}'",
                              $"{{toGenerate.NameSpace}}.{{toGenerate.ClassName}}.{nameof(WithNonce)}");
                          return this;
                      }
                  """);
        }

        if (Contains(mixins, MixinTypes.ReportSample))
        {
            sb.AppendLine(
                //lang=csharp
                $$"""

                      /// <summary>
                      /// Requires a sample of the violating code to be included in the violation report
                      /// </summary>
                      /// <returns>The CSP builder for method chaining</returns>
                      public {{toGenerate.ClassName}} ReportSample()
                      {
                          MustReportSample = true;
                          Sources.Add("'report-sample'");
                          return this;
                      }
                  """);
        }

        sb.AppendLine("}");
        var content = sb.ToString();
        return (content, $"{toGenerate.NameSpace}.{toGenerate.ClassName}.g.cs");
    }

    private static bool Contains(MixinTypes value, MixinTypes flag)
        => (value & flag) == flag;
}