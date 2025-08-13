// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using AutoMapper;

using CocktailsApp.Application.User;
using CocktailsApp.Infrastructure.SeedWork;


namespace CocktailsApp.Infrastructure.UserAggregate
{
    public sealed class UserCrossAggregateProjection : IProjection
    {
        public IProjection Inner { get; }

        public UserCrossAggregateProjection(IMapper autoMapper)
        {
            Inner = new Projection<UserRead>(autoMapper);
        }

        public bool CanHandle(Type eventType) => Inner.CanHandle(eventType);

        public Task HandleAsync(object @event, EFReadDbContext db, CancellationToken ct)
            => Inner.HandleAsync(@event, db, ct);
    }
}
