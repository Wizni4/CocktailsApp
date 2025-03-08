/*
 * Domain namespaces
 */
using Domain.SeedWork;
using Domain.CocktailAggregate;

/*
 * Application namespaces
 */
using Application.SeedWork;

/*
 * Framework namespaces
 */
using AutoMapper;

namespace Application.Cocktails
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
