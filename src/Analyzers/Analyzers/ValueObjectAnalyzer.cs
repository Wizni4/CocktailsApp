using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.Diagnostics;

using System.Collections.Immutable;

namespace CocktailsApp.Analyzers.Analyzers
{
    [DiagnosticAnalyzer(LanguageNames.CSharp)]
    public class ValueObjectAnalyzer : DiagnosticAnalyzer
    {
        private static readonly DiagnosticDescriptor s_rule = new(
            id: "DDD1000",
            title: "Value object must be immutable",
            messageFormat: "Value object '{0}' contains mutable member '{1}'",
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

            // Check if the type inherits from the 'ValueObject' base class.
            var valueObjectBase = context.Compilation.GetTypeByMetadataName("CocktailsApp.Domain.SeedWork.ValueObject");
            if (valueObjectBase == null || !InheritsFrom(namedTypeSymbol, valueObjectBase))
                return;

            // Check properties for any setters.
            foreach (var property in namedTypeSymbol.GetMembers().OfType<IPropertySymbol>())
            {
                if (property.DeclaredAccessibility == Accessibility.Public && property.SetMethod != null)
                {
                    var diagnostic = Diagnostic.Create(s_rule, property.Locations[0], namedTypeSymbol.Name, property.Name);
                    context.ReportDiagnostic(diagnostic);
                }
            }

            // Check public fields that are not readonly.
            foreach (var field in namedTypeSymbol.GetMembers().OfType<IFieldSymbol>())
            {
                if (field.DeclaredAccessibility == Accessibility.Public && !field.IsReadOnly && !field.IsConst)
                {
                    var diagnostic = Diagnostic.Create(s_rule, field.Locations[0], namedTypeSymbol.Name, field.Name);
                    context.ReportDiagnostic(diagnostic);
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
