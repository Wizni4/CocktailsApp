// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using AutoMapper;

using CocktailsApp.Application.SeedWork;
using CocktailsApp.Application.Shared;

using Microsoft.Extensions.Options;


namespace CocktailsApp.Application.Ingredient
{
    public class IngredientSearchService(
        IMapper autoMapper,
        IIngredientRepository ingredientRepository,
        IOptions<SearchSettingsDTO> options
    ) : SearchService<IngredientDTO, SearchIngredientQuery>(options), IIngredientSearchService
    {
        private readonly IMapper _autoMapper = autoMapper;
        private readonly IIngredientRepository _ingredientRepository = ingredientRepository;
        public override async Task<IEnumerable<IngredientDTO>> GetSearchResultsAsync(SearchIngredientQuery query)
        {
            var searchLimit = _options.Value.Limit;

            var ingredients = await _ingredientRepository.ReadRangeAsync(
                new IngredientByTermSpecification(query.Term), limit: searchLimit);

            return _autoMapper.Map<IEnumerable<IngredientDTO>>(ingredients);
        }
    }
}
