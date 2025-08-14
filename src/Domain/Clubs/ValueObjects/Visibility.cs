/*
 * Domain namespaces
 */

/*
 * Framework namespaces
 */

namespace CocktailsApp.Domain.Clubs
{
    /// <summary>
    /// Specifies the visibility of a <see cref="Club"/>.
    /// </summary>
    public enum Visibility
    {
        /// <summary>
        /// The <see cref="Club"/> is visible to all users.
        /// </summary>
        Public = 1,

        /// <summary>
        /// The <see cref="Club"/> is visible only to its members.
        /// </summary>
        Private = 0,
    }
}
