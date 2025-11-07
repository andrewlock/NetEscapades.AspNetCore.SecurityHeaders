using System.Text;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.Text;
using NetEscapades.AspNetCore.SecurityHeaders.Headers.ContentSecurityPolicy;

namespace SourceGenerator;

[Generator]
public class ContentSecurityPolicyGenerator : IIncrementalGenerator
{
    public void Initialize(IncrementalGeneratorInitializationContext context)
    {
        IncrementalValuesProvider<MixinToGenerate?> typesToGenerate = context.SyntaxProvider
            .ForAttributeWithMetadataName(Constants.CspMixinAttributeFullName,
                predicate: static (node, _) => node is ClassDeclarationSyntax,
                transform: GetTypeToGenerate)
            .Where(static m => m is { Mixins: > 0 })
            .WithTrackingName(TrackingNames.Results);

        context.RegisterSourceOutput(typesToGenerate,
            static (context, typeToGenerate) =>
            {
                var (result, filename) = SourceGenerationHelper.GeneratePartials(typeToGenerate!);
                context.AddSource(filename, SourceText.From(result, Encoding.UTF8));
            });
    }

    static MixinToGenerate? GetTypeToGenerate(GeneratorAttributeSyntaxContext context, CancellationToken ct)
    {
        INamedTypeSymbol? typeSymbol = context.TargetSymbol as INamedTypeSymbol;
        if (typeSymbol is null)
        {
            // nothing to do if this type isn't available
            return null;
        }

        ct.ThrowIfCancellationRequested();

        MixinTypes mixinTypes;

        foreach (AttributeData attributeData in typeSymbol.GetAttributes())
        {
            if (attributeData.AttributeClass?.Name != "CspMixinAttribute" ||
                attributeData.AttributeClass.ToDisplayString() != Constants.CspMixinAttributeFullName)
            {
                continue;
            }

            if (attributeData.ConstructorArguments is { IsEmpty: false } args 
                && args is [{ Kind: TypedConstantKind.Enum, Value: int intValue }])
            {
                mixinTypes = (MixinTypes)intValue; 
                goto done;
            }

            // error 
            return null;
        }

        // if we haven't got this, then abandon
        return null;
        
        done:

        var name = typeSymbol.Name;
        var nameSpace = typeSymbol.ContainingNamespace.ToString();

        return new MixinToGenerate(nameSpace, name, mixinTypes);
    }

    internal record MixinToGenerate(string NameSpace, string ClassName, MixinTypes Mixins);

}