/*
 * Domain namespaces
 */
using CocktailsApp.Domain.SeedWork;

/*
 * Framework namespaces
 */

namespace CocktailsApp.Domain.ClubAggregate
{
    /// <summary>
    /// Represents a cocktail associated to a <see cref="Club"/>.
    /// </summary>
    public class ClubCocktail : Entity
    {
        /// <summary>
        /// Gets the unique identifier of the cocktail.
        /// </summary>
        public Guid CocktailId { get; }

        /// <summary>
        /// Creates a new instance of the <see cref="ClubCocktail"/>.
        /// </summary>
        /// <param name="cocktailId">The unique identifier of the cocktail.</param>
        /// <exception cref="ArgumentNullException">
        /// Thrown when <paramref name="cocktailId"/> is <see cref="Guid.Empty"/>.
        /// </exception>
        internal ClubCocktail(Guid cocktailId)
        {
            if (cocktailId == Guid.Empty)
                throw new ArgumentException("CocktailId cannot be null.");
            CocktailId = cocktailId;
        }
    }
}
