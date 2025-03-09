using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.Diagnostics;

using System.Collections.Immutable;
using System.Linq;

namespace CocktailsApp.Analyzers.Analyzers
{
    [DiagnosticAnalyzer(LanguageNames.CSharp)]
    public class AggregateRootAnalyzer : DiagnosticAnalyzer
    {
        private static readonly DiagnosticDescriptor s_rule = new(
            id: "DDD0100",
            title: "Only aggregate roots should reference repositories",
            messageFormat: "Class '{0}' is not an aggregate root but references a repository",
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
            var isAggregateRoot = namedTypeSymbol.AllInterfaces.Any(i => i.Name == "IAggregateRoot");
            var repositoryInterface = context.Compilation.GetTypeByMetadataName("CocktailsApp.Domain.SeedWork.IRepository`1");

            if (repositoryInterface == null)
                return;

            foreach (var field in namedTypeSymbol.GetMembers().OfType<IFieldSymbol>())
            {
                if (IsRepository(field.Type, repositoryInterface) && !isAggregateRoot)
                {
                    var diagnostic = Diagnostic.Create(s_rule, field.Locations[0], namedTypeSymbol.Name);
                    context.ReportDiagnostic(diagnostic);
                }
            }
        }

        private bool IsRepository(ITypeSymbol typeSymbol, INamedTypeSymbol repositoryInterface)
        {
            // Check if the type implements IRepository<T> directly or indirectly
            if (typeSymbol is INamedTypeSymbol namedTypeSymbol)
            {
                if (namedTypeSymbol.ConstructedFrom.Equals(repositoryInterface, SymbolEqualityComparer.Default))
                {
                    return true;
                }

                foreach (var interfaceType in namedTypeSymbol.Interfaces)
                {
                    if (interfaceType.ConstructedFrom.Equals(repositoryInterface, SymbolEqualityComparer.Default))
                    {
                        return true;
                    }
                }
            }
            return false;
        }
    }
}
