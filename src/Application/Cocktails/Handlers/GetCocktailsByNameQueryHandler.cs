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
    public class GetCocktailsByNameQueryHandler(IUnitOfWork unitOfWork, IMapper autoMapper) : GetCocktailsQueryHandler<GetCocktailsByNameQuery>(unitOfWork, autoMapper)
    {
    }
}
