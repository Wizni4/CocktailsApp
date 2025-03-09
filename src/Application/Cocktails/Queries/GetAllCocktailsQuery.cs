
/*
 * Application namespaces
 */
using CocktailsApp.Application.SeedWork;
/*
 * Framework namespaces
 */

/*
 * Domain namespaces
 */
using CocktailsApp.Domain.CocktailAggregate;
using CocktailsApp.Domain.SeedWork;


namespace CocktailsApp.Application.Cocktails
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
