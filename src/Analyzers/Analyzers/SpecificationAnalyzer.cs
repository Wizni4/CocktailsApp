using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.Diagnostics;

using System.Collections.Immutable;
using System.Linq;

namespace CocktailsApp.Analyzers.Analyzers
{
    [DiagnosticAnalyzer(LanguageNames.CSharp)]
    public class SpecificationPatternAnalyzer : DiagnosticAnalyzer
    {
        private static readonly DiagnosticDescriptor s_rule = new(
            id: "DDD0900",
            title: "Specifications should implement ISpecification<T>",
            messageFormat: "Specification '{0}' should implement ISpecification<T>",
            category: "DDD Rules",
            DiagnosticSeverity.Warning,
            isEnabledByDefault: true
        );

        public override ImmutableArray<DiagnosticDescriptor> SupportedDiagnostics => [s_rule];

        public override void Initialize(AnalysisContext context)
        {
            context.EnableConcurrentExecution();
            context.ConfigureGeneratedCodeAnalysis(GeneratedCodeAnalysisFlags.None);
            context.RegisterSymbolAction(AnalyzeNamedType, SymbolKind.NamedType);
        }

        private void AnalyzeNamedType(SymbolAnalysisContext context)
        {
            var namedTypeSymbol = (INamedTypeSymbol)context.Symbol;
            var specificationInterface = context.Compilation.GetTypeByMetadataName("CocktailsApp.Domain.SeedWork.ISpecification`1");

            var filePath = namedTypeSymbol.Locations[0].SourceTree?.FilePath;
            if (specificationInterface == null || !namedTypeSymbol.Name.EndsWith("Specification") || filePath.Contains("SeedWork"))
                return;

            // Check if the specification implements ISpecification<T> directly or indirectly
            if (!namedTypeSymbol.Interfaces.Any(i => SymbolEqualityComparer.Default.Equals(i.OriginalDefinition, specificationInterface)) &&
                !namedTypeSymbol.AllInterfaces.Any(i => SymbolEqualityComparer.Default.Equals(i.OriginalDefinition, specificationInterface)))
            {
                var diagnostic = Diagnostic.Create(s_rule, namedTypeSymbol.Locations[0], namedTypeSymbol.Name);
                context.ReportDiagnostic(diagnostic);
            }
        }
    }
}
