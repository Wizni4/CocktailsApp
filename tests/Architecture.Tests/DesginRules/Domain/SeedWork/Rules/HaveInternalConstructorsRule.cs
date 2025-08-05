using CocktailsApp.Architecture.Tests.DesignRules.SeedWork;

namespace CocktailsApp.Architecture.Tests.DesignRules.Domain
{
    public class HaveInternalConstructorsRule : TypeDefinitionRule
    {
        public HaveInternalConstructorsRule()
            : base(type =>
                type.Methods
                    .Where(m => m.IsConstructor)
                    .All(m => m.IsAssembly || m.IsPrivate))
        { }
    }
}
