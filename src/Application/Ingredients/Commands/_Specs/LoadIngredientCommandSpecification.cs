
using CocktailsApp.Application.Common;
using CocktailsApp.Domain.Common;
using CocktailsApp.Domain.Ingredients;

namespace CocktailsApp.Application.Ingredients
{
    public class LoadIngredientCommandSpecification(
        Guid ingredientId
    ) : ICommandSpecification<Ingredient>
    {
        private readonly Guid _ingredientId = ingredientId;
        public ISpecification<Ingredient>? Specification => new IngredientByIdSpecification(_ingredientId);
        public IEnumerable<ILoad<Ingredient>> Graph => [IngredientLoadGraph.Default];
    }
}
