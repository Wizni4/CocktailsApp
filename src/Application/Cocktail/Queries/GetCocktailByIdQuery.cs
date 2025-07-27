
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
    public class GetCocktailByIdQuery(Guid cocktailId) : IQuery<Domain.CocktailAggregate.Cocktail, CocktailDTO>
    {
        private readonly Guid _cocktailId = cocktailId;
        public ISpecification<Domain.CocktailAggregate.Cocktail> Specification => throw new NotImplementedException();

        public Func<IIncludable<Domain.CocktailAggregate.Cocktail>, IIncludable>? Include => throw new NotImplementedException();
    }
}
