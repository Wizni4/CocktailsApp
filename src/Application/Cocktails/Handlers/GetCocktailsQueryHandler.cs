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
    public abstract class GetCocktailsQueryHandler<TQuery>(IUnitOfWork unitOfWork, IMapper autoMapper) : IQueryHandler<TQuery, List<CocktailDTO>> where TQuery : IQuery<Cocktail>
    {
        private readonly IUnitOfWork _unitOfWork = unitOfWork;
        private readonly IMapper _autoMapper = autoMapper;
        public async Task<List<CocktailDTO>> Handle(TQuery query)
        {
            // Get the list of cocktails matching the specification
            var cocktails = await _unitOfWork.Set<Cocktail>().ReadRangeAsync(query.Specification, query.Include);

            // Convert the domain objects to DTO
            var cocktailsDTO = _autoMapper.Map<IEnumerable<CocktailDTO>>(cocktails);

            return [.. cocktailsDTO];
        }
    }
}
