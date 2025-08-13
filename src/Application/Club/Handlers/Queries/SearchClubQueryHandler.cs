// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using AutoMapper;

using CocktailsApp.Application.SeedWork;

using System.Collections.ObjectModel;

namespace CocktailsApp.Application.Club
{
    public class SearchClubQueryHandler(
        IClubSearchService clubSearchService
    ) : IQueryHandler<SearchClubQuery, ReadOnlyCollection<ClubDTO>>
    {
        private readonly IClubSearchService _clubSearchService = clubSearchService;
        public Task<ReadOnlyCollection<ClubDTO>> Handle(SearchClubQuery request, CancellationToken cancellationToken)
        {
            return _clubSearchService.GetSearchResultsAsync(request, cancellationToken);
        }
    }
}
