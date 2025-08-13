// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using AutoMapper;

using CocktailsApp.Application.SeedWork;

using System.Collections.ObjectModel;

using DomainIngredient = CocktailsApp.Domain.IngredientAggregate.Ingredient;

namespace CocktailsApp.Application.Ingredient
{
    public class GetAllIngredientsQueryHandler(
        IIngredientReader ingredientReader,
        IMapper autoMapper
    ) : IQueryHandler<GetAllIngredientsQuery, ReadOnlyCollection<IngredientDTO>>
    {
        private readonly IIngredientReader _ingredientReader = ingredientReader;
        private readonly IMapper _autoMapper = autoMapper;
        public async Task<ReadOnlyCollection<IngredientDTO>> Handle(GetAllIngredientsQuery request, CancellationToken cancellationToken)
        {
            return (await _ingredientReader.ListAsync(
                new AllIngredientsQuerySpecification(_autoMapper),
                cancellationToken)).ToList().AsReadOnly();
        }
    }
}
