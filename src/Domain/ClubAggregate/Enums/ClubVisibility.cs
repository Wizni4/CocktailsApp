/*
 * Domain namespaces
 */

/*
 * Framework namespaces
 */

namespace CocktailsApp.Domain.ClubAggregate
{
    /// <summary>
    /// Specifies the visibility of a <see cref="Club"/>.
    /// </summary>
    public enum ClubVisibility
    {
        /// <summary>
        /// The <see cref="Club"/> is visible to all users.
        /// </summary>
        Public,

        /// <summary>
        /// The <see cref="Club"/> is visible only to its members.
        /// </summary>
        Private,
    }
}
