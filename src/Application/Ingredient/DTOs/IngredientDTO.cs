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

using System.Collections.ObjectModel;

namespace CocktailsApp.Application.Ingredient
{
    public sealed class IngredientDTO : EntityDTO, IImageDTO
    {
        public Guid Id { get; set; } = default;
        public string Name { get; set; } = default!;
        public string Type { get; set; } = default!;
        public bool IsAlcoholic { get; set; } = default;
        public string? ImageId { get; set; } = null;
        public List<AllergenDTO> Allergens { get; set; } = default!;
    }
}
