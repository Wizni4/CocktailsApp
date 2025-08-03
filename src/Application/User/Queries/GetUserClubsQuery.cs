/*
 * Framework namespaces
 */
using AutoMapper;

using CocktailsApp.Application.Club;

/*
 * Application namespaces
 */
using CocktailsApp.Application.SeedWork;

/*
 * Domain namespaces
 */

namespace CocktailsApp.Application.User
{
    public record GetUserClubsQuery(Guid UserId) : IQuery<IEnumerable<ClubDTO>>;
}
