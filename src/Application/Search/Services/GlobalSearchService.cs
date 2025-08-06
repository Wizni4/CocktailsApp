/*
*Domain namespaces
*/
/*
 * Application namespaces
 */
using AutoMapper;

using CocktailsApp.Application.Club;
using CocktailsApp.Application.Cocktail;
using CocktailsApp.Application.Ingredient;
using CocktailsApp.Application.SeedWork;
using CocktailsApp.Application.User;

using Microsoft.Extensions.Options;

using DomainClub = CocktailsApp.Domain.ClubAggregate.Club;
using DomainCocktail = CocktailsApp.Domain.CocktailAggregate.Cocktail;
using DomainIngredient = CocktailsApp.Domain.IngredientAggregate.Ingredient;
using DomainUser = CocktailsApp.Domain.UserAggregate.User;
/*
 * Framework namespaces
 */


namespace CocktailsApp.Application.Search
{
    public class GlobalSearchService(
        IUnitOfWork unitOfWork,
        IMapper autoMapper,
        IOptions<SearchSettingsDTO> options
    ) : SearchService<GlobalSearchResultDTO, GlobalSearchQuery>(options), IGlobalSearchService
    {
        private readonly IMapper _autoMapper = autoMapper;
        private readonly IUnitOfWork _unitOfWork = unitOfWork;
        public override async Task<IEnumerable<GlobalSearchResultDTO>> GetSearchResultsAsync(GlobalSearchQuery query)
        {
            // Get the search limit
            var searchLimit = _options.Value.Limit;

            // Get cocktails by name
            var cocktails = _unitOfWork.Set<DomainCocktail>()
                .ReadRangeAsync(new CocktailByTermSpecification(query.Term), limit: searchLimit);

            // Get clubs by name
            var clubs = _unitOfWork.Set<DomainClub>()
                .ReadRangeAsync(new ClubByTermSpecification(query.Term, query.UserId), limit: searchLimit);

            // Get ingredients by name
            var ingredients = _unitOfWork.Set<DomainIngredient>()
                .ReadRangeAsync(new IngredientByTermSpecification(query.Term), limit: searchLimit);

            // Get users by Username
            var users = _unitOfWork.Set<DomainUser>()
                .ReadRangeAsync(new UserByTermSpecification(query.Term), limit: searchLimit);

            // Wait queries to complete
            await Task.WhenAll(
                cocktails,
                ingredients,
                users,
                clubs);

            // Map result and filter by score
            var results = _autoMapper.Map<IEnumerable<GlobalSearchResultDTO>>(await clubs)
                .Concat(_autoMapper.Map<IEnumerable<GlobalSearchResultDTO>>(await cocktails))
                .Concat(_autoMapper.Map<IEnumerable<GlobalSearchResultDTO>>(await ingredients))
                .Concat(_autoMapper.Map<IEnumerable<GlobalSearchResultDTO>>(await users))
                .Select(r =>
                {
                    r.Relevance = GetRelevanceScore(query.Term, r.Name);
                    return r;
                })
                .OrderByDescending(r => r.Relevance)
                .ThenBy(r => r.Name)
                .Take(searchLimit);

            return results;
        }
    }
}
