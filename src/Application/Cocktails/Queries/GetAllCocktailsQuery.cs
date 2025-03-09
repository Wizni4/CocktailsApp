
/*
 * Application namespaces
 */
using Application.SeedWork;
/*
 * Framework namespaces
 */

/*
 * Domain namespaces
 */
using Domain.CocktailAggregate;
using Domain.SeedWork;


namespace Application.Cocktails
{
    public class GetAllCocktailsQuery : IQuery<Cocktail>
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
