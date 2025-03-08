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
    public class GetAllCocktailsQueryHandler(IUnitOfWork unitOfWork, IMapper autoMapper) : IQueryHandler<GetAllCocktailsQuery, List<CocktailDTO>>
    {
        private readonly IUnitOfWork _unitOfWork = unitOfWork;
        private readonly IMapper _autoMapper = autoMapper;
        public async Task<List<CocktailDTO>> Handle(GetAllCocktailsQuery query)
        {
            // Get the list of cocktails including sub-objects like ingredients
            var cocktails = await _unitOfWork.Set<Cocktail>().ReadAllAsync(query.Include);

            // Convert the domain objects to DTO
            var cocktailsDTO = _autoMapper.Map<IEnumerable<CocktailDTO>>(cocktails);

            return [.. cocktailsDTO];
        }
    }
}
