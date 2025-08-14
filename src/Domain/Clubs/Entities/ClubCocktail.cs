/*
 * Domain namespaces
 */
using CocktailsApp.Domain.Common;

/*
 * Framework namespaces
 */

namespace CocktailsApp.Domain.Clubs
{
    /// <summary>
    /// Represents a cocktail associated to a <see cref="Club"/>.
    /// </summary>
    public sealed class ClubCocktail : Entity
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
        internal ClubCocktail(Guid cocktailId, Guid createdBy) : base(createdBy)
        {
            if (cocktailId == Guid.Empty)
                throw new ArgumentException("CocktailId cannot be null.");
            CocktailId = cocktailId;
        }
    }
}
