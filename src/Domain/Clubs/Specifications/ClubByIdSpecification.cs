/*
 * Domain namespaces
 */
using CocktailsApp.Domain.Common;

/*
 * Framework namespaces
 */

namespace CocktailsApp.Domain.Clubs
{
    public class ClubByIdSpecification(Guid id) : ByIdSpecification<Club>(id)
    {
    }
}
