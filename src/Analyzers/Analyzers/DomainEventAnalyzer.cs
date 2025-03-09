using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.Diagnostics;

using System.Collections.Immutable;

[DiagnosticAnalyzer(LanguageNames.CSharp)]
public class DomainEventAnalyzer : DiagnosticAnalyzer
{
    private static readonly DiagnosticDescriptor s_rule = new(
        id: "DDD0400",
        title: "Domain events should implement DomainEvent",
        messageFormat: "Domain event '{0}' should implement DomainEvent",
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
        var domainEventInterface = context.Compilation.GetTypeByMetadataName("CocktailsApp.Domain.SeedWork.DomainEvent");

        var filePath = namedTypeSymbol.Locations[0].SourceTree?.FilePath;
        if (domainEventInterface == null || !namedTypeSymbol.Name.EndsWith("DomainEvent") || filePath.Contains("SeedWork"))
            return;

        if (!namedTypeSymbol.Interfaces.Contains(domainEventInterface))
        {
            var diagnostic = Diagnostic.Create(s_rule, namedTypeSymbol.Locations[0], namedTypeSymbol.Name);
            context.ReportDiagnostic(diagnostic);
        }
    }
}
