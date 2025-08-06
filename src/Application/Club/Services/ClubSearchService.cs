// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using AutoMapper;

using CocktailsApp.Application.SeedWork;

using Microsoft.Extensions.Options;

using DomainClub = CocktailsApp.Domain.ClubAggregate.Club;

namespace CocktailsApp.Application.Club
{
    public class ClubSearchService(
        IMapper autoMapper,
        IUnitOfWork unitOfWork,
        IOptions<SearchSettingsDTO> options
    ) : SearchService<ClubDTO, SearchClubQuery>(options), IClubSearchService
    {
        private readonly IMapper _autoMapper = autoMapper;
        private readonly IUnitOfWork _unitOfWork = unitOfWork;

        public override async Task<IEnumerable<ClubDTO>> GetSearchResultsAsync(SearchClubQuery query)
        {
            var searchLimit = _options.Value.Limit;

            var clubs = await _unitOfWork.Set<DomainClub>().ReadRangeAsync(
                new ClubByTermSpecification(query.Term, query.UserId),
                limit: searchLimit);

            return _autoMapper.Map<IEnumerable<ClubDTO>>(clubs);
        }
    }
}
