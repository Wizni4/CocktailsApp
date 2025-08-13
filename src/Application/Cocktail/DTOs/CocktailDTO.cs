/*
 * Domain namespaces
 */

/*
 * Application namespaces
 */
using CocktailsApp.Application.SeedWork;

using System.Collections.ObjectModel;
/*
 * Framework namespaces
 */

namespace CocktailsApp.Application.Cocktail
{
    public sealed class CocktailDTO: EntityDTO, IImageDTO
    {
        public Guid Id { get; set; } = default;
        public string Name { get; set; } = default!;
        public string? Description { get; set; } = null!;
        public string? ImageId { get; set; } = null!;
        public List<CocktailIngredientDTO> Ingredients { get; set; } = default!;
    }
}
