using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.Diagnostics;
using System.Collections.Immutable;
using System.Linq;

namespace CocktailsApp.Analyzers.Analyzers
{
    [DiagnosticAnalyzer(LanguageNames.CSharp)]
    public class BuilderAnalyzer : DiagnosticAnalyzer
    {
        private static readonly DiagnosticDescriptor s_rule_implementation = new(
            id: "DDD0200",
            title: "Builder should implement IBuilder<T>",
            messageFormat: "Builder '{0}' should implement IBuilder<T>",
            category: "DDD Rules",
            DiagnosticSeverity.Warning,
            isEnabledByDefault: true
        );

        private static readonly DiagnosticDescriptor s_rule_class_name = new(
            id: "DDD0201",
            title: "Class implementing IBuilder<T> should end with 'Builder' suffix",
            messageFormat: "Class '{0}' should end with 'Builder' suffix",
            category: "DDD Rules",
            DiagnosticSeverity.Warning,
            isEnabledByDefault: true
        );

        private static readonly DiagnosticDescriptor s_rule_file_location = new(
            id: "DDD0202",
            title: "Builder should be located in the 'Builders' folder",
            messageFormat: "Builder '{0}' should be located in the 'Builders' folder",
            category: "DDD Rules",
            DiagnosticSeverity.Warning,
            isEnabledByDefault: true
        );

        public override ImmutableArray<DiagnosticDescriptor> SupportedDiagnostics => [s_rule_implementation, s_rule_class_name, s_rule_file_location];

        public override void Initialize(AnalysisContext context)
        {
            context.EnableConcurrentExecution();
            context.ConfigureGeneratedCodeAnalysis(GeneratedCodeAnalysisFlags.None);
            context.RegisterSymbolAction(AnalyzeNamedType, SymbolKind.NamedType);
        }

        private void AnalyzeNamedType(SymbolAnalysisContext context)
        {
            var namedTypeSymbol = (INamedTypeSymbol)context.Symbol;
            var builderInterface = context.Compilation.GetTypeByMetadataName("CocktailsApp.Domain.SeedWork.IBuilder`1");

            if (builderInterface == null)
                return;

            var filePath = namedTypeSymbol.Locations[0].SourceTree?.FilePath;

            // Check if the class implements IBuilder<T>
            if ((namedTypeSymbol.Name.EndsWith("Builder") || filePath.Contains("Builders")) && !filePath.Contains("SeedWork") &&
                !namedTypeSymbol.Interfaces.Any(i => SymbolEqualityComparer.Default.Equals(i.OriginalDefinition, builderInterface)))
            {
                var diagnostic = Diagnostic.Create(s_rule_implementation, namedTypeSymbol.Locations[0], namedTypeSymbol.Name);
                context.ReportDiagnostic(diagnostic);
            }

            // Check if the class name ends with 'Builder'
            if ((namedTypeSymbol.Interfaces.Any(i => SymbolEqualityComparer.Default.Equals(i.OriginalDefinition, builderInterface)) || filePath.Contains("Builders")) && !filePath.Contains("SeedWork") &&
                !namedTypeSymbol.Name.EndsWith("Builder"))
            {
                var diagnostic = Diagnostic.Create(s_rule_class_name, namedTypeSymbol.Locations[0], namedTypeSymbol.Name);
                context.ReportDiagnostic(diagnostic);
            }

            // Check if the file is located in the 'Builders' folder
            if ((namedTypeSymbol.Interfaces.Any(i => SymbolEqualityComparer.Default.Equals(i.OriginalDefinition, builderInterface)) || namedTypeSymbol.Name.EndsWith("Builder")) && !filePath.Contains("SeedWork") &&
                filePath != null && !filePath.Contains("Builders"))
            {
                var diagnostic = Diagnostic.Create(s_rule_file_location, namedTypeSymbol.Locations[0], namedTypeSymbol.Name);
                context.ReportDiagnostic(diagnostic);
            }
        }
    }
}
