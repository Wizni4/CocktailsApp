/*
 * Application namespaces
 */

/*
 * Framework namespaces
 */
using AutoMapper;
/*
 * Domain namespaces
 */
using CocktailsApp.Domain.SeedWork;

namespace CocktailsApp.Application.Cocktails
{
    public class GetCocktailsByIngredientsQueryHandler(IUnitOfWork unitOfWork, IMapper autoMapper) : GetCocktailsQueryHandler<GetCocktailsByIngredientsQuery>(unitOfWork, autoMapper)
    {
    }
}
