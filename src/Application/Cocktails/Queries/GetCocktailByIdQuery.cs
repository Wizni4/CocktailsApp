
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
    public class GetCocktailByIdQuery(Guid cocktailId) : IQuery<Cocktail>
    {
        private readonly Guid _cocktailId = cocktailId;
        public ISpecification<Cocktail> Specification { get { return new CocktailByIdSpecification(_cocktailId); } }

        public Func<IIncludable<Cocktail>, IIncludable>? Include { get { return opt => opt.Include(c => c.Ingredients).ThenInclude(ci => ci.Ingredient); } }
    }
}
