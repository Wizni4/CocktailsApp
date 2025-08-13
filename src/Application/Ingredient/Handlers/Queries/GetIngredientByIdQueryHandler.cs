// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using AutoMapper;

using CocktailsApp.Application.SeedWork;


namespace CocktailsApp.Application.Ingredient
{
    public class GetIngredientByIdQueryHandler(
        IIngredientReader ingredientReader,
        IMapper autoMapper
    ) : IQueryHandler<GetIngredientByIdQuery, IngredientDTO?>
    {
        private readonly IIngredientReader _ingredientReader = ingredientReader;
        private readonly IMapper _autoMapper = autoMapper;

        public Task<IngredientDTO?> Handle(GetIngredientByIdQuery request, CancellationToken cancellationToken)
        {
            return _ingredientReader.FirstOrDefaultAsync(
                new IngredientByIdQuerySpecification(request.IngredientId, _autoMapper),
                cancellationToken);
        }
    }
}
