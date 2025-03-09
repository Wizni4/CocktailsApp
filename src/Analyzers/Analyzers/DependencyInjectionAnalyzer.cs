using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.Diagnostics;

using System.Collections.Immutable;

namespace CocktailsApp.Analyzers.Analyzers
{
    [DiagnosticAnalyzer(LanguageNames.CSharp)]
    public class DependencyInjectionAnalyzer : DiagnosticAnalyzer
    {
        private static readonly DiagnosticDescriptor s_rule = new(
            id: "DDD0300",
            title: "Dependencies should be injected",
            messageFormat: "Dependency '{0}' should be injected instead of being instantiated directly",
            category: "DDD Rules",
            DiagnosticSeverity.Warning,
            isEnabledByDefault: true
        );

        public override ImmutableArray<DiagnosticDescriptor> SupportedDiagnostics => [s_rule];

        public override void Initialize(AnalysisContext context)
        {
            context.EnableConcurrentExecution();
            context.ConfigureGeneratedCodeAnalysis(GeneratedCodeAnalysisFlags.None);
            context.RegisterSyntaxNodeAction(AnalyzeNode, SyntaxKind.ObjectCreationExpression);
        }

        private void AnalyzeNode(SyntaxNodeAnalysisContext context)
        {
            var objectCreation = (ObjectCreationExpressionSyntax)context.Node;
            var semanticModel = context.SemanticModel;
            var typeInfo = semanticModel.GetTypeInfo(objectCreation);

            if (typeInfo.Type is INamedTypeSymbol typeSymbol)
            {
                // Check if the type being instantiated is a service, repository, or other dependency
                if (typeSymbol.Name.EndsWith("Service") || typeSymbol.Name.EndsWith("Repository") ||
                    typeSymbol.Name.EndsWith("UnitOfWork") || typeSymbol.Name.EndsWith("Handler"))
                {
                    var diagnostic = Diagnostic.Create(s_rule, objectCreation.GetLocation(), typeSymbol.Name);
                    context.ReportDiagnostic(diagnostic);
                }
            }
        }
    }
}
