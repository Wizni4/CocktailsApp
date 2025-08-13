/*
 * Domain namespaces
 */

/*
 * Application namespaces
 */
using CocktailsApp.Application.Cocktail;
using CocktailsApp.Application.SeedWork;
/*
 * Framework namespaces
 */

namespace CocktailsApp.Application.Club
{
    /// <summary>
    /// Data Transfer Object (DTO) representing a cocktail of a club?
    /// Inherits from <see cref="EntityDTO"/> to include common entity identification.
    /// </summary>
    public sealed class ClubCocktailDTO: EntityDTO
    {
        public Guid Id { get; set; } = default;
        public CocktailDTO Cocktail { get; set; } = default!;
    }
}
