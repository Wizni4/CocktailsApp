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
using CocktailsApp.Application.Shared;
using CocktailsApp.Application.User;

using Microsoft.Extensions.Options;

using System.Collections.ObjectModel;
/*
 * Framework namespaces
 */


namespace CocktailsApp.Application.Search
{
    public class GlobalSearchService(
        IClubReader clubReader,
        IIngredientReader ingredientReader,
        ICocktailReader cocktailReader,
        IUserReader userReader,
        IMapper autoMapper,
        IOptions<SearchSettingsDTO> options
    ) : SearchService<GlobalSearchResultDTO, GlobalSearchQuery>(options), IGlobalSearchService
    {
        private readonly IClubReader _clubReader = clubReader;
        private readonly IIngredientReader _ingredientReader = ingredientReader;
        private readonly ICocktailReader _cocktailReader = cocktailReader;
        private readonly IUserReader _userReader = userReader;
        private readonly IMapper _autoMapper = autoMapper;
        public override async Task<ReadOnlyCollection<GlobalSearchResultDTO>> GetSearchResultsAsync(GlobalSearchQuery query, CancellationToken cancellationToken)
        {
            // Get the search limit
            var searchLimit = _options.Value.Limit;

            // Get cocktails by name
            var cocktailDTOs = _cocktailReader.ListAsync(
                new SearchCocktailQuerySpecification(searchLimit, query.Term, _autoMapper),
                cancellationToken);

            // Get clubs by name
            var clubDTOs = _clubReader.ListAsync(
                new SearchClubQuerySpecification(searchLimit, query.Term, query.UserId, _autoMapper),
                cancellationToken);

            // Get ingredients by name
            var ingredientDTOs = _ingredientReader.ListAsync(
                new SearchIngredientQuerySpecification(searchLimit, query.Term, _autoMapper),
                cancellationToken);

            // Get users by Username
            var userDTOs = _userReader.ListAsync(
                new SearchUserQuerySpecification(searchLimit, query.Term, _autoMapper),
                cancellationToken);

            // Wait queries to complete
            await Task.WhenAll(
                cocktailDTOs,
                ingredientDTOs,
                userDTOs,
                clubDTOs);

            // Map result and filter by score
            var results = _autoMapper.Map<IEnumerable<GlobalSearchResultDTO>>(await clubDTOs)
                .Concat(_autoMapper.Map<IEnumerable<GlobalSearchResultDTO>>(await cocktailDTOs))
                .Concat(_autoMapper.Map<IEnumerable<GlobalSearchResultDTO>>(await ingredientDTOs))
                .Concat(_autoMapper.Map<IEnumerable<GlobalSearchResultDTO>>(await userDTOs))
                .Select(r =>
                {
                    r.Relevance = GetRelevanceScore(query.Term, r.Name);
                    return r;
                })
                .OrderByDescending(r => r.Relevance)
                .ThenBy(r => r.Name)
                .Take(searchLimit)
                .ToList()
                .AsReadOnly();

            return results;
        }
    }
}
