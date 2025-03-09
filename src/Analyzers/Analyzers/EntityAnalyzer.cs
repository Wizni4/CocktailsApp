using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.Diagnostics;

using System.Collections.Immutable;
using System.Linq;

namespace CocktailsApp.Analyzers.Analyzers
{
    [DiagnosticAnalyzer(LanguageNames.CSharp)]
    public class EntityAnalyzer : DiagnosticAnalyzer
    {
        private static readonly DiagnosticDescriptor s_inheritanceRule = new(
            id: "DDD0700",
            title: "Entities must inherit from Entity or ValueObject",
            messageFormat: "Class '{0}' should inherit from 'Entity' or 'ValueObject'",
            category: "DDD Rules",
            DiagnosticSeverity.Warning,
            isEnabledByDefault: true
        );

        private static readonly DiagnosticDescriptor s_publicMethodRule = new(
            id: "DDD0701",
            title: "Entities should not have public methods unless they are aggregate roots",
            messageFormat: "The entity '{0}' contains a public method but does not implement IAggregateRoot",
            category: "DDD Rules",
            DiagnosticSeverity.Warning,
            isEnabledByDefault: true
        );

        public override ImmutableArray<DiagnosticDescriptor> SupportedDiagnostics => [s_inheritanceRule, s_publicMethodRule];

        public override void Initialize(AnalysisContext context)
        {
            context.EnableConcurrentExecution();
            context.ConfigureGeneratedCodeAnalysis(GeneratedCodeAnalysisFlags.Analyze);
            context.RegisterSymbolAction(AnalyzeNamedType, SymbolKind.NamedType);
            context.RegisterSyntaxNodeAction(AnalyzeNode, SyntaxKind.ClassDeclaration);
        }

        private void AnalyzeNamedType(SymbolAnalysisContext context)
        {
            var namedTypeSymbol = (INamedTypeSymbol)context.Symbol;
            var entityBase = context.Compilation.GetTypeByMetadataName("CocktailsApp.Domain.SeedWork.Entity");
            var valueObjectBase = context.Compilation.GetTypeByMetadataName("CocktailsApp.Domain.SeedWork.ValueObject");

            if (entityBase == null || valueObjectBase == null)
                return;

            // Check if the class is in the "Entities" folder and not in the "Seedwork" folder
            var filePath = namedTypeSymbol.Locations[0].SourceTree?.FilePath;
            if (string.IsNullOrEmpty(filePath) || !filePath.Contains("Entities") || filePath.Contains("SeedWork"))
                return;

            if (!InheritsFrom(namedTypeSymbol, entityBase) && !InheritsFrom(namedTypeSymbol, valueObjectBase))
            {
                var diagnostic = Diagnostic.Create(s_inheritanceRule, namedTypeSymbol.Locations[0], namedTypeSymbol.Name);
                context.ReportDiagnostic(diagnostic);
            }
        }

        private static void AnalyzeNode(SyntaxNodeAnalysisContext context)
        {
            var classDeclaration = (ClassDeclarationSyntax)context.Node;
            var semanticModel = context.SemanticModel;

            if (semanticModel.GetDeclaredSymbol(classDeclaration) is not INamedTypeSymbol classSymbol)
                return;

            var baseTypes = classSymbol.BaseType?.OriginalDefinition.Name == "Entity";
            var implementsIAggregateRoot = classSymbol.Interfaces.Any(i => i.Name == "IAggregateRoot");

            if (baseTypes && !implementsIAggregateRoot)
            {
                foreach (var member in classSymbol.GetMembers())
                {
                    if (member.Kind == SymbolKind.Method && member.DeclaredAccessibility == Accessibility.Public)
                    {
                        // Skip property accessors
                        if (member is IMethodSymbol methodSymbol &&
                            (methodSymbol.MethodKind == MethodKind.PropertyGet || methodSymbol.MethodKind == MethodKind.PropertySet))
                        {
                            continue;
                        }

                        var diagnostic = Diagnostic.Create(s_publicMethodRule, member.Locations[0], member.Name);
                        context.ReportDiagnostic(diagnostic);
                    }
                }
            }
        }

        private bool InheritsFrom(INamedTypeSymbol symbol, INamedTypeSymbol baseType)
        {
            for (var currentBase = symbol.BaseType; currentBase != null; currentBase = currentBase.BaseType)
            {
                if (SymbolEqualityComparer.Default.Equals(currentBase, baseType))
                    return true;
            }
            return false;
        }
    }
}
