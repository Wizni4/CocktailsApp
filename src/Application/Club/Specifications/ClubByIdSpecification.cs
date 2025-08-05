/*
 * Domain namespaces
 */
using CocktailsApp.Domain.SeedWork;

using DomainClub = CocktailsApp.Domain.ClubAggregate.Club;
/*
 * Framework namespaces
 */

namespace CocktailsApp.Application.Club
{
    public class ClubByIdSpecification(Guid id) : ByIdSpecification<DomainClub>(id)
    {
    }
}
