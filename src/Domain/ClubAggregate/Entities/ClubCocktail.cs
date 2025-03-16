/*
 * Domain namespaces
 */
using CocktailsApp.Domain.SeedWork;
using CocktailsApp.Domain.UserAggregate;

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
            if (cocktailId == Guid.Empty)
                throw new ArgumentNullException(nameof(cocktailId), "CocktailId cannot be null.");
            CocktailId = cocktailId;
        }
    }
}
