// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using CocktailsApp.Application.Prices;
using CocktailsApp.Domain.Prices;
using CocktailsApp.Infrastructure.SeedWork;


namespace CocktailsApp.Infrastructure.IngredientPricingAggregate
{
    public class IngredientPricingRepository(EFWriteDbContext dbContext)
        : EFRepository<Pricing>(dbContext), PricingRepository;
}
