// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using CocktailsApp.Domain.Ingredients;
using CocktailsApp.ReadStore.Persistence;
using CocktailsApp.ReadStore.Projections;

using Microsoft.EntityFrameworkCore;


namespace CocktailsApp.ReadStore.Ingredients
{
    public sealed class IngredientDeletedProjection(
        EFReadDbContext dbContext
    ) : IProjectionHandler<IngredientDeletedEvent>
    {
        private readonly EFReadDbContext _dbContext = dbContext;

        public async Task HandleAsync(IngredientDeletedEvent @event, CancellationToken cancellationToken)
        {
            var ingredient = await _dbContext.Set<IngredientRead>()
                .FirstOrDefaultAsync(i => i.IngredientId == @event.IngredientId);
            if (ingredient != null) _dbContext.Remove(ingredient);

            await _dbContext.SaveChangesAsync(cancellationToken);
        }
    }
}
