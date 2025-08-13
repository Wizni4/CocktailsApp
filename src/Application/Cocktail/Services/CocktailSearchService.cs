// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using AutoMapper;

using CocktailsApp.Application.SeedWork;
using CocktailsApp.Application.Shared;

using Microsoft.Extensions.Options;

using System.Collections.ObjectModel;


namespace CocktailsApp.Application.Cocktail
{
    public class CocktailSearchService(
        ICocktailReader cocktailReader,
        IOptions<SearchSettingsDTO> option,
        IMapper autoMapper
    ) : SearchService<CocktailDTO, SearchCocktailQuery>(option), ICocktailsSearchService
    {
        private readonly ICocktailReader _cocktailReader = cocktailReader;
        private readonly IMapper _autoMapper = autoMapper;

        public override async Task<ReadOnlyCollection<CocktailDTO>> GetSearchResultsAsync(SearchCocktailQuery query, CancellationToken cancellationToken)
        {
            var searchLimit = _options.Value.Limit;

            return (await _cocktailReader.ListAsync(
                new SearchCocktailQuerySpecification(searchLimit, query.Term, _autoMapper),
                cancellationToken)).ToList().AsReadOnly();
        }
    }
}
