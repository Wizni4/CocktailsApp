// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.
using CocktailsApp.Application.SeedWork;
using CocktailsApp.Application.Shared;
using Microsoft.Extensions.Options;


namespace CocktailsApp.Application.Club
{
    public class ClubSearchService(
        IClubQueries clubQueries,
        IOptions<SearchSettingsDTO> options
    ) : SearchService<ClubDTO, SearchClubQuery>(options), IClubSearchService
    {
        private readonly IClubQueries _clubQueries = clubQueries;

        public override Task<IEnumerable<ClubDTO>> GetSearchResultsAsync(SearchClubQuery query)
        {
            var searchLimit = _options.Value.Limit;

            return _clubQueries.ReadRangeAsync(
                new SearchClubQuerySpecification(query.Term, query.UserId));
        }
    }
}
