using System.Collections.Immutable;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.Diagnostics;
using Microsoft.CodeAnalysis.Operations;

namespace NetEscapades.AspNetCore.SecurityHeaders.Analyzers;

[DiagnosticAnalyzer(LanguageNames.CSharp)]
public class DeprecatedApiAnalyzer : DiagnosticAnalyzer
{
    public const string DiagnosticId = "NEASPSH001";
    public static readonly DiagnosticDescriptor Rule = new(
#pragma warning disable RS2008 // Enable Analyzer Release Tracking
        id: DiagnosticId,
#pragma warning restore RS2008
        title: "API is deprecated",
        messageFormat: "This API is deprecated and may be removed in a future version. {0}{1}.",
        category: "Usage",
        defaultSeverity: DiagnosticSeverity.Info,
        isEnabledByDefault: true);

    public override ImmutableArray<DiagnosticDescriptor> SupportedDiagnostics
        => ImmutableArray.Create(Rule);

    public override void Initialize(AnalysisContext context)
    {
        context.ConfigureGeneratedCodeAnalysis(GeneratedCodeAnalysisFlags.None);
        context.EnableConcurrentExecution();

        context.RegisterCompilationStartAction(compilationStartContext =>
        {
            var deprecatedAttributeSymbol = compilationStartContext.Compilation.GetTypeByMetadataName(
                "NetEscapades.AspNetCore.SecurityHeaders.Helpers.DeprecatedAttribute");
            if (deprecatedAttributeSymbol is null)
            {
                return;
            }

            // Invocation of methods (including extension methods)
            // can extend to more operations if necessary 
            compilationStartContext.RegisterOperationAction(operationContext =>
            {
                var invocation = (IInvocationOperation)operationContext.Operation;
                var symbol = invocation.TargetMethod;
                if (TryGetDeprecatedData(symbol, deprecatedAttributeSymbol, out var message, out var url))
                {
                    var suffix = string.IsNullOrEmpty(url) ? string.Empty : $" See: {url}";
                    operationContext.ReportDiagnostic(Diagnostic.Create(Rule, invocation.Syntax.GetLocation(), message, suffix));
                }
            }, OperationKind.Invocation);
        });
    }

    private static bool TryGetDeprecatedData(ISymbol symbol, INamedTypeSymbol deprecatedAttributeSymbol, out string message, out string url)
    {
        message = string.Empty;
        url = string.Empty;

        foreach (var attribute in symbol.GetAttributes())
        {
            if (SymbolEqualityComparer.Default.Equals(attribute.AttributeClass, deprecatedAttributeSymbol))
            {
                // Constructor argument 0 is the Message string
                if (!attribute.ConstructorArguments.IsDefaultOrEmpty && attribute.ConstructorArguments[0].Value is string m)
                {
                    message = m;
                }

                // Optional named argument Url
                foreach (var namedArg in attribute.NamedArguments)
                {
                    if (namedArg is { Key: "Url", Value.Value: string u })
                    {
                        url = u;
                    }
                }

                return true;
            }
        }

        return false;
    }
}