using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.Diagnostics;

using System.Collections.Immutable;
using System.Linq;

namespace CocktailsApp.Analyzers.Analyzers
{
    [DiagnosticAnalyzer(LanguageNames.CSharp)]
    public class DomainServiceAnalyzer : DiagnosticAnalyzer
    {
        private static readonly DiagnosticDescriptor s_rule = new(
            id: "DDD0600",
            title: "Domain service should be stateless",
            messageFormat: "Domain service '{0}' contains instance field '{1}' which may lead to statefulness",
            category: "DDD Rules",
            DiagnosticSeverity.Info,
            isEnabledByDefault: true
        );

        private static readonly DiagnosticDescriptor s_infrastructureRule = new(
            id: "DDD0601",
            title: "Domain services should not access infrastructure directly",
            messageFormat: "Domain service '{0}' accesses infrastructure directly through field '{1}'",
            category: "DDD Rules",
            DiagnosticSeverity.Warning,
            isEnabledByDefault: true
        );

        private static readonly DiagnosticDescriptor s_serviceLayerDependencyRule = new(
            id: "DDD0602",
            title: "Application services should depend on domain services",
            messageFormat: "Application service '{0}' should not depend directly on infrastructure",
            category: "DDD Rules",
            DiagnosticSeverity.Warning,
            isEnabledByDefault: true
        );

        public override ImmutableArray<DiagnosticDescriptor> SupportedDiagnostics => [s_rule, s_infrastructureRule, s_serviceLayerDependencyRule];

        public override void Initialize(AnalysisContext context)
        {
            context.EnableConcurrentExecution();
            context.ConfigureGeneratedCodeAnalysis(GeneratedCodeAnalysisFlags.None);
            context.RegisterSymbolAction(AnalyzeNamedType, SymbolKind.NamedType);
        }

        private void AnalyzeNamedType(SymbolAnalysisContext context)
        {
            var namedTypeSymbol = (INamedTypeSymbol)context.Symbol;
            var isDomainService = namedTypeSymbol.AllInterfaces.Any(i => i.Name == "IDomainService") ||
                                   namedTypeSymbol.Name.EndsWith("Service");
            var isApplicationService = namedTypeSymbol.Name.EndsWith("AppService");

            if (!isDomainService && !isApplicationService)
                return;

            // Report any instance field as a potential state holder for domain services.
            if (isDomainService)
            {
                foreach (var field in namedTypeSymbol.GetMembers().OfType<IFieldSymbol>())
                {
                    if (!field.IsStatic)
                    {
                        var diagnostic = Diagnostic.Create(s_rule, field.Locations[0], namedTypeSymbol.Name, field.Name);
                        context.ReportDiagnostic(diagnostic);
                    }

                    // Check for direct infrastructure access in domain services.
                    if (field.Type.Name != "IUnitOfWork" && (field.Type.Name.EndsWith("DbContext") || field.Type.Name.EndsWith("HttpClient")))
                    {
                        var infrastructureDiagnostic = Diagnostic.Create(s_infrastructureRule, field.Locations[0], namedTypeSymbol.Name, field.Name);
                        context.ReportDiagnostic(infrastructureDiagnostic);
                    }
                }
            }

            // Check for direct infrastructure dependencies in application services.
            if (isApplicationService)
            {
                foreach (var field in namedTypeSymbol.GetMembers().OfType<IFieldSymbol>())
                {
                    if (field.Type.Name.EndsWith("Repository") || field.Type.Name.EndsWith("DbContext"))
                    {
                        var diagnostic = Diagnostic.Create(s_serviceLayerDependencyRule, field.Locations[0], namedTypeSymbol.Name);
                        context.ReportDiagnostic(diagnostic);
                    }
                }
            }
        }
    }
}
