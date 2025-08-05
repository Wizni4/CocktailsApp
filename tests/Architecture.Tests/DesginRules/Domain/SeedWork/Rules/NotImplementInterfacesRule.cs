

using CocktailsApp.Architecture.Tests.DesignRules.SeedWork;

namespace CocktailsApp.Architecture.Tests.DesignRules.Domain
{
    public class NotImplementInterfacesRule : TypeDefinitionRule
    {
        private static readonly string[] s_allowedInterfaces = new[]
        {
            "CocktailsApp.Domain.SeedWork.IAggregateRoot",
            "CocktailsApp.Domain.SeedWork.IEntity"
        };

        public NotImplementInterfacesRule()
            : base(type => type.Interfaces.All(i => s_allowedInterfaces.Contains(i.InterfaceType.FullName)))
        {
        }
    }
}
