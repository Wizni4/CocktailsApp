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
    public class GetCocktailsByNameQueryHandler(IUnitOfWork unitOfWork, IMapper autoMapper) : GetCocktailsQueryHandler<GetCocktailsByNameQuery>(unitOfWork, autoMapper)
    {
    }
}
