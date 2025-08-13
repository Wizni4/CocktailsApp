// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using CocktailsApp.Domain.SeedWork;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CocktailsApp.Domain.IngredientPricingAggregate
{
    public sealed record IngredientPricingCreatedEvent(
        Guid IngredientPricingId,
        decimal Cost,
        decimal Price,
        Guid ActorId
    ) : DomainEvent(IngredientPricingId, typeof(IngredientPricing), ActorId);
}
