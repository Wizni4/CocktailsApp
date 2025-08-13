/*
 * Domain namespaces
 */
using CocktailsApp.Domain.ClubAggregate;

/*
* Framework namespaces
*/


namespace CocktailsApp.Infrastructure.ClubAggregate
{
    public class ClubMemberToClubRole
    {
        public required ClubMember ClubMember { get; set; }
        public required ClubRole ClubRole { get; set; }
    }
}
