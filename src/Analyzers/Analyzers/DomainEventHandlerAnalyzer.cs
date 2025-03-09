using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.Diagnostics;

using System.Collections.Immutable;
using System.Linq;

[DiagnosticAnalyzer(LanguageNames.CSharp)]
public class DomainEventHandlerAnalyzer : DiagnosticAnalyzer
{
    private static readonly DiagnosticDescriptor s_rule = new(
        id: "DDD0500",
        title: "Domain event handlers should implement IDomainEventHandler<T>",
        messageFormat: "Domain event handler '{0}' should implement IDomainEventHandler<T>",
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
        var handlerInterface = context.Compilation.GetTypeByMetadataName("CocktailsApp.Domain.SeedWork.IDomainEventHandler`1");

        if (handlerInterface == null || !namedTypeSymbol.Name.EndsWith("EventHandler"))
            return;

        if (!namedTypeSymbol.Interfaces.Any(i => SymbolEqualityComparer.Default.Equals(i.OriginalDefinition, handlerInterface)))
        {
            var diagnostic = Diagnostic.Create(s_rule, namedTypeSymbol.Locations[0], namedTypeSymbol.Name);
            context.ReportDiagnostic(diagnostic);
        }
    }
}
