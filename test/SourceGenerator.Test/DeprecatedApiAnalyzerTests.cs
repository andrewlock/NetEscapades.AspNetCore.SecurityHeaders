using NetEscapades.AspNetCore.SecurityHeaders.Analyzers;
using Verifier = Microsoft.CodeAnalysis.CSharp.Testing.CSharpAnalyzerVerifier<
    NetEscapades.AspNetCore.SecurityHeaders.Analyzers.DeprecatedApiAnalyzer,
    Microsoft.CodeAnalysis.Testing.DefaultVerifier>;

namespace SourceGenerator.Test;

public class DeprecatedApiAnalyzerTests
{
    [Test]
    public async Task EmptySource()
    {
        var test = string.Empty;
        await Verifier.VerifyAnalyzerAsync(test);
    }

    [Test]
    public async Task UsageOfExtensionMethodWithoutAttribute()
    {
        var test = GetTestCode(
            /* lang=c# */
            """
            // library code
            public static class TestExtensions
            {
                public static void TestApp(this MyTest test){}
            }
            
            // customer usage
            public class MyTest()
            {
                public void Main()
                {
                    var t = new MyTest();
                    t.TestApp();
                }
            }
            """);
        await Verifier.VerifyAnalyzerAsync(test);
    }

    [Test]
    public async Task UsageOfMethodWithAttribute()
    {
        var test = GetTestCode(
            /* lang=c# */
            """
            // library code
            public static class TestExtensions
            {
                [Deprecated("This is deprecated")]
                public static void TestApp(){}
            }
            
            // customer usage
            public class MyTest()
            {
                public void Main()
                {
                    {|NEASPSH001:TestExtensions.TestApp()|};
                }
            }
            """);
        await Verifier.VerifyAnalyzerAsync(test);
    }

    [Test]
    public async Task UsageOfExtensionMethodWithAttribute()
    {
        var test = GetTestCode(
            /* lang=c# */
            """
            // library code
            public static class TestExtensions
            {
                [Deprecated("This is deprecated")]
                public static void TestApp(this MyTest test){}
            }
            
            // customer usage
            public class MyTest()
            {
                public void Main()
                {
                    var t = new MyTest();
                    {|NEASPSH001:t.TestApp()|};
                }
            }
            """);
        await Verifier.VerifyAnalyzerAsync(test);
    }

    private static string GetTestCode(string testCode) => $$"""
        using System;
        using System.Collections.Generic;
        using System.Linq;
        using System.Text;
        using System.Threading;
        using System.Threading.Tasks;
        using System.Diagnostics;
        using NetEscapades.AspNetCore.SecurityHeaders.Helpers;

        namespace ConsoleApplication1
        {
            {{testCode}}
        }

        //{{TestHelpers.DeprecatedAttribute}}
        """;
}