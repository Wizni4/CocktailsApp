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
using Domain.SeedWork;

namespace Application.Cocktails
{
    public class GetCocktailsByIngredientsQueryHandler(IUnitOfWork unitOfWork, IMapper autoMapper) : GetCocktailsQueryHandler<GetCocktailsByIngredientsQuery>(unitOfWork, autoMapper)
    {
    }
}
