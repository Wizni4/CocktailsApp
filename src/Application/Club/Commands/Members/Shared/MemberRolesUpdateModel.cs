/*
 * Framework namespaces
 */
/*
 * Application namespaces
 */
using CocktailsApp.Application.SeedWork;
/*
 * Domain namespaces
 */
using CocktailsApp.Domain.ClubAggregate;

namespace CocktailsApp.Application.Club
{
    public class MemberRolesUpdateModel
    {
        public required Guid Id { get; set; }
        public required IEnumerable<Guid> RoleIds { get; set; }
    }
}
