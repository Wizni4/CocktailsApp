// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using AutoMapper;

using CocktailsApp.Application.SeedWork;
using CocktailsApp.Application.Shared;

using Microsoft.Extensions.Options;

using System.Collections.ObjectModel;


namespace CocktailsApp.Application.Ingredient
{
    public class IngredientSearchService(
        IIngredientReader ingredientReader,
        IOptions<SearchSettingsDTO> options,
        IMapper autoMapper
    ) : SearchService<IngredientDTO, SearchIngredientQuery>(options), IIngredientSearchService
    {
        private readonly IIngredientReader _ingredientReader = ingredientReader;
        private readonly IMapper _autoMapper = autoMapper;
        public override async Task<ReadOnlyCollection<IngredientDTO>> GetSearchResultsAsync(SearchIngredientQuery query, CancellationToken cancellationToken)
        {
            var searchLimit = _options.Value.Limit;

            return (await _ingredientReader.ListAsync(
                new SearchIngredientQuerySpecification(searchLimit, query.Term, _autoMapper),
                cancellationToken)).ToList().AsReadOnly();
        }
    }
}
