// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using CocktailsApp.Application.IngredientPricing;
using CocktailsApp.Domain.IngredientPricingAggregate;
using CocktailsApp.Infrastructure.SeedWork;


namespace CocktailsApp.Infrastructure.IngredientPricingAggregate
{
    public class IngredientPricingRepository(EFWriteDbContext dbContext)
        : EFRepository<IngredientPricing>(dbContext), IIngredientPricingRepository;
}
