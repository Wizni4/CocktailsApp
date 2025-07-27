
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
    public class GetAllCocktailsQuery : IQuery<CocktailDTO>
    {
        public ISpecification<CocktailDTO> Specification => throw new NotImplementedException();

        public Func<IIncludable<CocktailDTO>, IIncludable>? Include => throw new NotImplementedException();
    }
}
