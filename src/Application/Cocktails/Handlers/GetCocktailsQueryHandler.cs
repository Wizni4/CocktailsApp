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
    public class GetCocktailsQueryHandler(IUnitOfWork unitOfWork, IMapper autoMapper) : IQueryHandler<GetCocktailsQuery, List<CocktailDTO>>
    {
        private readonly IUnitOfWork _unitOfWork = unitOfWork;
        private readonly IMapper _autoMapper = autoMapper;
        public async Task<List<CocktailDTO>> Handle(GetCocktailsQuery query)
        {
            // Get the list of cocktails including sub-objects like ingredients
            var cocktails = await _unitOfWork.Set<Cocktail>().ReadAllAsync(query.Include);

            // Convert the domain objects to DTO
            var cocktailsDTO = _autoMapper.Map<IEnumerable<CocktailDTO>>(cocktails);

            return [.. cocktailsDTO];
        }
    }
}
