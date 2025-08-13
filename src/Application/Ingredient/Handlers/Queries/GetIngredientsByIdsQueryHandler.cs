// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using AutoMapper;

using CocktailsApp.Application.SeedWork;

using System.Collections.ObjectModel;

using DomainIngredient = CocktailsApp.Domain.IngredientAggregate.Ingredient;

namespace CocktailsApp.Application.Ingredient
{
    public class GetIngredientsByIdsQueryHandler(
        IIngredientReader ingredientReader,
        IMapper autoMapper
    ) : IQueryHandler<GetIngredientsByIdsQuery, ReadOnlyCollection<IngredientDTO>>
    {
        private readonly IIngredientReader _ingredientReader = ingredientReader;
        private readonly IMapper _autoMapper = autoMapper;
        public async Task<ReadOnlyCollection<IngredientDTO>> Handle(GetIngredientsByIdsQuery request, CancellationToken cancellationToken)
        {
            return (await _ingredientReader.ListAsync(
                new IngredientsByIdsQuerySpecification(request.Ids, _autoMapper),
                cancellationToken)).ToList().AsReadOnly();
        }
    }
}
