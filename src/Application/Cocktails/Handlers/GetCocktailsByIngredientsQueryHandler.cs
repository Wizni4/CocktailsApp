/*
 * Domain namespaces
 */
using Domain.SeedWork;

/*
 * Application namespaces
 */

/*
 * Framework namespaces
 */
using AutoMapper;

namespace Application.Cocktails
{
    public class GetCocktailsByIngredientsQueryHandler(IUnitOfWork unitOfWork, IMapper autoMapper) : GetCocktailsQueryHandler<GetCocktailsByIngredientsQuery>(unitOfWork, autoMapper)
    {
    }
}
