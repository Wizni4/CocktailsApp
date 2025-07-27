/*
 * Application namespaces
 */
/*
 * Framework namespaces
 */
using AutoMapper;

using CocktailsApp.Application.SeedWork;
/*
 * Domain namespaces
 */
using CocktailsApp.Domain.CocktailAggregate;
using CocktailsApp.Domain.SeedWork;

namespace CocktailsApp.Application.Cocktail
{
    public class GetCocktailByIdQueryHandler(IUnitOfWork unitOfWork, IMapper autoMapper) 
        : IQueryHandler<GetCocktailByIdQuery, CocktailDTO>
    {
        private readonly IUnitOfWork _unitOfWork = unitOfWork;
        private readonly IMapper _autoMapper = autoMapper;

        public Task<CocktailDTO> Handle(GetCocktailByIdQuery request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
