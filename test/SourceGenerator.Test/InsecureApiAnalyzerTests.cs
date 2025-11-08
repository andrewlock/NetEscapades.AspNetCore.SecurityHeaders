using Microsoft.CodeAnalysis.CSharp.Testing;
using Microsoft.CodeAnalysis.Testing;
using NetEscapades.AspNetCore.SecurityHeaders.Analyzers;

namespace SourceGenerator.Test;

public class InsecureApiAnalyzerTests
{
    [Test]
    public async Task EmptySource()
    {
        var test = string.Empty;
        await CSharpAnalyzerVerifier<InsecureApiAnalyzer, DefaultVerifier>.VerifyAnalyzerAsync(test);
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
        await CSharpAnalyzerVerifier<InsecureApiAnalyzer, DefaultVerifier>.VerifyAnalyzerAsync(test);
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
                [InsecureApi("This is deprecated")]
                public static void TestApp(){}
            }

            // customer usage
            public class MyTest()
            {
                public void Main()
                {
                    {|NEASPSH002:TestExtensions.TestApp()|};
                }
            }
            """);
        await CSharpAnalyzerVerifier<InsecureApiAnalyzer, DefaultVerifier>.VerifyAnalyzerAsync(test);
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
                [InsecureApi("This is deprecated")]
                public static void TestApp(this MyTest test){}
            }

            // customer usage
            public class MyTest()
            {
                public void Main()
                {
                    var t = new MyTest();
                    {|NEASPSH002:t.TestApp()|};
                }
            }
            """);
        await CSharpAnalyzerVerifier<InsecureApiAnalyzer, DefaultVerifier>.VerifyAnalyzerAsync(test);
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

                                                            //{{TestHelpers.InsecureApiAttribute}}
                                                            """;
}