// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using CocktailsApp.Application.Ingredients;
using CocktailsApp.Infrastructure.SeedWork;


namespace CocktailsApp.Infrastructure.IngredientAggregate
{
    public class IngredientReader(EFReadDbContext dbContext)
        : EFQueryReader<IngredientRead, IngredientDTO>(dbContext), IIngredientReader;
}
