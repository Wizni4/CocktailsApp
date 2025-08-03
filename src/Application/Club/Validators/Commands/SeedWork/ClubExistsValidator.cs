/*
 * Domain namespaces
 */

/*
 * Application namespaces
 */

/*
 * Framework namespaces
 */
using CocktailsApp.Application.SeedWork;

using DomainClub = CocktailsApp.Domain.ClubAggregate.Club;


namespace CocktailsApp.Application.Club
{
    public class ClubExistsValidator(IRepository<DomainClub> clubRepository) : AggregateExistsValidator<DomainClub>(clubRepository)
    {
    }
}
