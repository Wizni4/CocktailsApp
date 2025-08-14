

using CocktailsApp.Architecture.Tests.DesignRules.SeedWork;

namespace CocktailsApp.Architecture.Tests.DesignRules.Domain
{
    public class NotImplementInterfacesRule : TypeDefinitionRule
    {
        private static readonly string[] s_allowedInterfaces = new[]
        {
            "CocktailsApp.Domain.Common.IAggregateRoot",
            "CocktailsApp.Domain.Common.IEntity"
        };

        public NotImplementInterfacesRule()
            : base(type => type.Interfaces.All(i => s_allowedInterfaces.Contains(i.InterfaceType.FullName)))
        {
        }
    }
}
