/*
 * Domain namespaces
 */
using CocktailsApp.Domain.SeedWork;
/*
 * Framework namespaces
 */

namespace CocktailsApp.Domain.ClubAggregate
{
    public class Permission : ValueObject
    {
        public ClubAction Action { get; }
        internal Permission(ClubAction action)
        {
            Action = action;
        }
    }
}
