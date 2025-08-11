/*
 * Framework namespaces
 */
using AutoMapper;
/*
 * Application namespaces
 */
using CocktailsApp.Application.SeedWork;
/*
 * Domain namespaces
 */

namespace CocktailsApp.Application.Club
{
    public record GetClubByIdQuery(Guid ClubId) : ClubQuery<ClubDTO?>(ClubId);
}
