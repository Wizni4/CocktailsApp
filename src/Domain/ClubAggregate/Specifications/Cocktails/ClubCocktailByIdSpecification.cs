/*
 * Domain namespaces
 */
using CocktailsApp.Domain.SeedWork;

using System.Linq.Expressions;

/*
 * Framework namespaces
 */

namespace CocktailsApp.Domain.ClubAggregate
{
    public class ClubCocktailByIdSpecification(Guid cocktailId) : Specification<ClubCocktail>
    {
        private readonly Guid _cocktailId = cocktailId;
        public override Expression<Func<ClubCocktail, bool>> SpecExpression
        {
            get
            {
                return cocktail => cocktail.Id == _cocktailId || cocktail.CocktailId == _cocktailId;
            }
        }
    }
}
