/*
 * Domain namespaces
 */

/*
 * Application namespaces
 */

/*
 * Framework namespaces
 */

using CocktailsApp.Application.SeedWork;
using CocktailsApp.Domain.IngredientAggregate;

namespace CocktailsApp.Application.Ingredient
{
    public class IngredientDTO : EntityDTO
    {
        public required List<AllergenDTO> Allergens { get; set; }
        public required string Name { get; set; }
        public IngredientType Type { get; set; }
        public required bool IsAlcoholic { get; set; }
    }
}
