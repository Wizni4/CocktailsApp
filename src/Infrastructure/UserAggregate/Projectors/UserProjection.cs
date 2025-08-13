// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using AutoMapper;

using CocktailsApp.Application.User;
using CocktailsApp.Domain.UserAggregate;
using CocktailsApp.Infrastructure.SeedWork;


namespace CocktailsApp.Infrastructure.UserAggregate
{
    public sealed class UserProjection : IProjection
    {
        public IProjection Inner { get; }

        public UserProjection(IMapper autoMapper)
        {
            Inner = new Projection<UserRead>(autoMapper)
                .CreateMapOn<UserCreatedEvent>(u => u.UserId);
        }

        public bool CanHandle(Type eventType) => Inner.CanHandle(eventType);

        public Task HandleAsync(object @event, EFReadDbContext db, CancellationToken ct)
            => Inner.HandleAsync(@event, db, ct);
    }
}
