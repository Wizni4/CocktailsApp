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

namespace CocktailsApp.Application.Cocktails
{
    public class GetCocktailByIdQueryHandler(IUnitOfWork unitOfWork, IMapper autoMapper) : IQueryHandler<GetCocktailByIdQuery, CocktailDTO>
    {
        private readonly IUnitOfWork _unitOfWork = unitOfWork;
        private readonly IMapper _autoMapper = autoMapper;
        public async Task<CocktailDTO> Handle(GetCocktailByIdQuery query)
        {
            // Get the cocktail matching the specification
            var cocktail = await _unitOfWork.Set<Cocktail>().ReadAsync(query.Specification, query.Include);

            // Convert the domain object to DTO
            var cocktailDTO = _autoMapper.Map<CocktailDTO>(cocktail);

            return cocktailDTO;
        }
    }
}
