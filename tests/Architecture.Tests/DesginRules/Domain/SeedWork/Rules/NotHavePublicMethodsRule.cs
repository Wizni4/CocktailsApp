using CocktailsApp.Architecture.Tests.DesignRules.SeedWork;

namespace CocktailsApp.Architecture.Tests.DesignRules.Domain
{
    public class NotHavePublicMethodsRule : TypeDefinitionRule
    {
        public NotHavePublicMethodsRule()
            : base(type =>
                type.Methods
                    .Where(m => !m.IsGetter && !m.IsSetter)
                    .All(m => m.IsAssembly || m.IsPrivate))
        {
        }
    }
}
