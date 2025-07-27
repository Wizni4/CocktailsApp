/*
 * Domain namespaces
 */

/*
 * Application namespaces
 */
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
    public class ClubCocktailDTO: EntityDTO
    {
        /// <summary>
        /// Gets the unique identifier of the cocktail.
        /// </summary>
        public Guid CocktailId { get; }
    }
}
