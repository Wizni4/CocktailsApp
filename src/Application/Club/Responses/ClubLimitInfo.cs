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
    public record ClubLimitInfo(int NumberOfOwnedClubs, int MaxNumberOfOwnedClubs, bool CanCreateClub)
    {
    }
}
