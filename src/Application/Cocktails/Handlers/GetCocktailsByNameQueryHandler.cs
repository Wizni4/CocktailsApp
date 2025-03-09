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
    public class GetCocktailsByNameQueryHandler(IUnitOfWork unitOfWork, IMapper autoMapper) : GetCocktailsQueryHandler<GetCocktailsByNameQuery>(unitOfWork, autoMapper)
    {
    }
}
