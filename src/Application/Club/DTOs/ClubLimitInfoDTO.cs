/*
 * Domain namespaces
 */
/*
 * Framework namespaces
 */
/*
 * Application namespaces
 */

namespace CocktailsApp.Application.Club
{
    public record ClubLimitInfoDTO(int NumberOfOwnedClubs, int MaxNumberOfOwnedClubs, bool CanCreateClub)
    {
    }
}
