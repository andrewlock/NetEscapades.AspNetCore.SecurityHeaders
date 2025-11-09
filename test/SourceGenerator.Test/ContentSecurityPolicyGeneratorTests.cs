using FluentAssertions;
using Microsoft.CodeAnalysis;
using TUnit.Assertions;
using TUnit.Core;
using VerifyTests;
using VerifyTUnit;

namespace SourceGenerator.Test;

public class ContentSecurityPolicyGeneratorTests
{
    private readonly IIncrementalGenerator _generator = new ContentSecurityPolicyGenerator();
    private VerifySettings Settings()
    {
        var settings = new VerifySettings();
        settings.UseDirectory("Snapshots");
        settings.DisableRequireUniquePrefix();

        return settings;
    }

    [Test]
    public Task CanGenerateCspMixinsInGlobalNamespace()
    {
        var input = $$"""
                      using System;
                      using System.Collections.Generic;
                      
                      #nullable enable

                      namespace NetEscapades.AspNetCore.SecurityHeaders.Headers.ContentSecurityPolicy
                      {
                          [CspMixin(MixinTypes.All)]
                          public partial class TestBuilder : CspDirectiveBuilder
                          {
                          }
                      }
                      {{Included.CspDirectiveBuilder}}
                      """;
        var (diagnostics, output) = TestHelpers.GetGeneratedOutput(_generator,
            new(TestHelpers.EmbeddedAttribute, TestHelpers.EmbeddedEnum, input));

        diagnostics.Should().BeEmpty();
        return Verifier.Verify(output, Settings());
    }

    private class Included
    {
        public static readonly string CspDirectiveBuilder =
            $$"""
              namespace NetEscapades.AspNetCore.SecurityHeaders.Headers.ContentSecurityPolicy
              {
                  using Microsoft.AspNetCore.Http;
                  public class CspDirectiveBuilder
                  {
                      public bool BlockResources { get; set; }
                      public bool MustReportSample { get; set; }
                      public List<string> Sources { get; } = new();
                      public SourceBuilderCollection SourceBuilders { get; } = new();
                  }
                  
                  public class SourceBuilderCollection
                  {
                      public void Add(Func<HttpContext, string> a, string b)
                      {
                      }
                  }
              }
              namespace Microsoft.AspNetCore.Http
              {
                  public class HttpContext
                  {
                      public string GetNonce() => string.Empty;
                      public string[] GetScriptCSPHashes() => new string[] { };
                  }
              }

              {{TestHelpers.InsecureApiAttribute}}
              """;
    }
}