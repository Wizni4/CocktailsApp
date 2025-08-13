// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CocktailsApp.Infrastructure.SeedWork
{
    public sealed class ProjectionBus(
        EFReadDbContext dbContext,
        IEnumerable<IProjection> projections
    )
    {
        private readonly IEnumerable<IProjection> _projections = projections;
        private readonly EFReadDbContext _dbContext = dbContext;

        public async Task DispatchAsync(object evt, CancellationToken ct = default)
        {
            foreach (var projection in _projections)
                if (projection.CanHandle(evt.GetType()))
                    await projection.HandleAsync(evt, _dbContext, ct);
        }
    }
}
