/*
 * Domain namespaces
 */
using Domain.CocktailAggregate;

/*
 * Application namespaces
 */
using Application.SeedWork;
using Domain.SeedWork;
/*
 * Framework namespaces
 */

namespace Application.Cocktails
{
    public class GetCocktailsQuery : IQuery<Cocktail>
    {
        public ISpecification<Cocktail> Specification => throw new NotImplementedException();

        public Func<IIncludable<Cocktail>, IIncludable>? Include
        {
            get
            {
                return opt => opt.Include(c => c.Ingredients).ThenInclude(ci => ci.Ingredient);
            }
        }
    }
}
