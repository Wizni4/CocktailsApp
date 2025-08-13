// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using CocktailsApp.Application.SeedWork;

using System.Collections.ObjectModel;


namespace CocktailsApp.Application.Cocktail
{
    public class SearchCocktailQueryHandler(
        ICocktailsSearchService cocktailsSearchService
    ) : IQueryHandler<SearchCocktailQuery, ReadOnlyCollection<CocktailDTO>>
    {
        private readonly ICocktailsSearchService _cocktailsSearchService = cocktailsSearchService;

        public Task<ReadOnlyCollection<CocktailDTO>> Handle(SearchCocktailQuery request, CancellationToken cancellationToken)
        {
            return _cocktailsSearchService.GetSearchResultsAsync(request, cancellationToken);
        }
    }
}
