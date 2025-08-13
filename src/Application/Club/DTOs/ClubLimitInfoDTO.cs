/*
 * Domain namespaces
 */
/*
 * Framework namespaces
 */
/*
 * Application namespaces
 */

using CocktailsApp.Application.SeedWork;

namespace CocktailsApp.Application.Club
{
    public sealed class ClubLimitInfoDTO: EntityDTO
    {
        public int NumberOfOwnedClubs { get; set; }
        public int MaxNumberOfOwnedClubs { get; set; }
        public bool CanCreateClub { get; set; }
    }
}
