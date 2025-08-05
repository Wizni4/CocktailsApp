/*
*Domain namespaces
*/
/*
 * Application namespaces
 */
using AutoMapper;

using CocktailsApp.Application.SeedWork;

using Microsoft.Extensions.Options;

using DomainClub = CocktailsApp.Domain.ClubAggregate.Club;
using DomainCocktail = CocktailsApp.Domain.CocktailAggregate.Cocktail;
using DomainUser = CocktailsApp.Domain.UserAggregate.User;
/*
 * Framework namespaces
 */


namespace CocktailsApp.Application.Search
{
    public class SearchService(
        IUnitOfWork unitOfWork,
        IMapper autoMapper,
        IOptions<SearchSettingsDTO> options
    ) : ISearchService
    {
        private readonly IOptions<SearchSettingsDTO> _options = options;
        private readonly IMapper _autoMapper = autoMapper;
        private readonly IUnitOfWork _unitOfWork = unitOfWork;

        public async Task<IEnumerable<SearchResultDTO>> GetSearchResultsAsync(SearchQuery query)
        {
            // Get the search limit
            var searchLimit = _options.Value.Limit;

            // Get cocktails by name
            var cocktails = _unitOfWork.Set<DomainCocktail>()
                .ReadRangeAsync(new CocktailByTermSpecification(query.Term), limit: searchLimit);

            // Get clubs by name
            var clubs = _unitOfWork.Set<DomainClub>()
                .ReadRangeAsync(new ClubByTermSpecification(query.Term, query.UserId), limit: searchLimit);

            // Get users by Username
            var users = _unitOfWork.Set<DomainUser>()
                .ReadRangeAsync(new UserByTermSpecification(query.Term), limit: searchLimit);

            // Wait queries to complete
            await Task.WhenAll(cocktails, users, clubs);
            var clubResult = await clubs;

            // Map result and filter by score
            var results = _autoMapper.Map<IEnumerable<SearchResultDTO>>(await clubs)
                .Concat(_autoMapper.Map<IEnumerable<SearchResultDTO>>(await cocktails))
                .Concat(_autoMapper.Map<IEnumerable<SearchResultDTO>>(await users))
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

        private int GetRelevanceScore(string term, string target)
        {
            var result = 0;
            if (string.IsNullOrWhiteSpace(term) || string.IsNullOrWhiteSpace(target))
                return result;

            term = term.ToLowerInvariant();
            target = target.ToLowerInvariant();

            if (target == term)
                result = 100;
            if (target.StartsWith(term))
                result = 80;
            if (target.Contains(term))
                result = 60;

            return 0;
        }
    }
}
