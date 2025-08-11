/*
 * Domain namespaces
 */
using CocktailsApp.Domain.SeedWork;
/*
 * Framework namespaces
 */

namespace CocktailsApp.Domain.ClubAggregate
{
    public class ClubByIdSpecification(Guid id) : ByIdSpecification<Club>(id)
    {
    }
}
