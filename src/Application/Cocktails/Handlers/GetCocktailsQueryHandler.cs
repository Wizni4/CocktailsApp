/*
 * Application namespaces
 */
using Application.SeedWork;
/*
 * Framework namespaces
 */
using AutoMapper;
/*
 * Domain namespaces
 */
using Domain.CocktailAggregate;
using Domain.SeedWork;


namespace Application.Cocktails
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
