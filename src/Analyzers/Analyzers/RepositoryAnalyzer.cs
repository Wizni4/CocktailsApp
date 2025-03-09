using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.Diagnostics;

using System.Collections.Immutable;
using System.Linq;

namespace CocktailsApp.Analyzers.Analyzers
{
    [DiagnosticAnalyzer(LanguageNames.CSharp)]
    public class RepositoryInterfaceAnalyzer : DiagnosticAnalyzer
    {
        private static readonly DiagnosticDescriptor s_rule = new(
            id: "DDD0800",
            title: "Repositories should implement IRepository<T>",
            messageFormat: "Repository '{0}' should implement IRepository<T>",
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
            var repositoryInterface = context.Compilation.GetTypeByMetadataName("CocktailsApp.Domain.SeedWork.IRepository`1");

            var filePath = namedTypeSymbol.Locations[0].SourceTree?.FilePath;
            if (repositoryInterface == null || !namedTypeSymbol.Name.EndsWith("Repository") || filePath.Contains("SeedWork"))
                return;

            // Check if the repository interface implements IRepository<T>
            if (!namedTypeSymbol.Interfaces.Any(i => SymbolEqualityComparer.Default.Equals(i.OriginalDefinition, repositoryInterface)))
            {
                var diagnostic = Diagnostic.Create(s_rule, namedTypeSymbol.Locations[0], namedTypeSymbol.Name);
                context.ReportDiagnostic(diagnostic);
            }
        }
    }
}
