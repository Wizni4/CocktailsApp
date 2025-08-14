/*
 * Framework namespaces
 */
using AutoMapper;

using CocktailsApp.Application.Clubs;

/*
 * Application namespaces
 */
using CocktailsApp.Application.SeedWork;

using System.Collections.ObjectModel;

/*
 * Domain namespaces
 */

namespace CocktailsApp.Application.Users
{
    public record GetUserClubsQuery(Guid UserId) : IQuery<IEnumerable<ClubDTO>>;
}
