// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.
using AutoMapper;

using CocktailsApp.Application.SeedWork;
using CocktailsApp.Application.Shared;

using Microsoft.Extensions.Options;

using System.Collections.ObjectModel;


namespace CocktailsApp.Application.Club
{
    public class ClubSearchService(
        IClubReader clubReader,
        IOptions<SearchSettingsDTO> options,
        IMapper autoMapper
    ) : SearchService<ClubDTO, SearchClubQuery>(options), IClubSearchService
    {
        private readonly IClubReader _clubReader = clubReader;
        private readonly IMapper _autoMapper = autoMapper;

        public override async Task<ReadOnlyCollection<ClubDTO>> GetSearchResultsAsync(SearchClubQuery query, CancellationToken cancellationToken)
        {
            var searchLimit = _options.Value.Limit;

            return (await _clubReader.ListAsync(
                new SearchClubQuerySpecification(searchLimit, query.Term, query.UserId, _autoMapper),
                cancellationToken)).ToList().AsReadOnly();
        }
    }
}
