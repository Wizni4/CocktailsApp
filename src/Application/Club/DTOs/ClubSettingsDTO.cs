/*
 * Domain namespaces
 */
using CocktailsApp.Application.SeedWork;
/*
 * Framework namespaces
 */

namespace CocktailsApp.Application.Club
{
    public sealed class ClubSettingsDTO: EntityDTO
    {
        public int MaxOwnedClubs { get; set; }
    }
}
