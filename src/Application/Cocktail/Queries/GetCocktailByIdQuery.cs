
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

using MediatR;


namespace CocktailsApp.Application.Cocktail
{
    public class GetCocktailByIdQuery(Guid cocktailId) : IQuery<CocktailDTO>
    {
        private readonly Guid _cocktailId = cocktailId;
        public ISpecification<CocktailDTO> Specification => throw new NotImplementedException();

        public Func<IIncludable<CocktailDTO>, IIncludable>? Include => throw new NotImplementedException();
    }
}
