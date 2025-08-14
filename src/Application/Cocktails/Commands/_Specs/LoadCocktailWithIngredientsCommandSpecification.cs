

using CocktailsApp.Application.Common;
using CocktailsApp.Domain.Cocktails;
using CocktailsApp.Domain.Common;

namespace CocktailsApp.Application.Cocktails
{
    public sealed class LoadCocktailWithIngredientsCommandSpecification(
        Guid cocktailId
    ) : ICommandSpecification<Cocktail>
    {
        private readonly Guid _cocktailId = cocktailId;
        public ISpecification<Cocktail>? Specification => new CocktailByIdSpecification(_cocktailId);
        public IEnumerable<ILoad<Cocktail>> Graph => [CocktailLoadGraph.Ingredients];
    }
}
