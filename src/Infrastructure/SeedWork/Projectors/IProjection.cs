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
    public interface IProjection
    {
        IProjection Inner { get; }
        bool CanHandle(Type eventType);
        Task HandleAsync(object @event, EFReadDbContext db, CancellationToken cancellationToken);
    }
}
