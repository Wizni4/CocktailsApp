
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

namespace CocktailsApp.Application.Cocktail
{
    public class GetAllCocktailsQuery : IQuery<Domain.CocktailAggregate.Cocktail, CocktailDTO>
    {
        public ISpecification<Domain.CocktailAggregate.Cocktail> Specification => throw new NotImplementedException();

        public Func<IIncludable<Domain.CocktailAggregate.Cocktail>, IIncludable>? Include => throw new NotImplementedException();
    }
}
