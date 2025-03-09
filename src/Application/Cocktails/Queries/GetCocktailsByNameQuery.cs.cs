
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
    public class GetCocktailsByNameQuery(string cocktailName) : IQuery<Cocktail>
    {
        private readonly string _cocktailName = cocktailName;
        public ISpecification<Cocktail> Specification { get { return new CocktailByNameSpecification(_cocktailName); } }

        public Func<IIncludable<Cocktail>, IIncludable>? Include { get { return opt => opt.Include(c => c.Ingredients).ThenInclude(ci => ci.Ingredient); } }
    }
}
