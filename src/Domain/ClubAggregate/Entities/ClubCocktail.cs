/*
 * Domain namespaces
 */
using CocktailsApp.Domain.SeedWork;

/*
 * Framework namespaces
 */

namespace CocktailsApp.Domain.ClubAggregate
{
    public class ClubCocktail : Entity
    {
        public Guid CocktailId { get; }
        internal ClubCocktail(Guid cocktailId)
        {
            CocktailId = cocktailId;
        }
    }
}
