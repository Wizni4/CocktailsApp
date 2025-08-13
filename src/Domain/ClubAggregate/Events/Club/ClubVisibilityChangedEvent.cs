// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.
using CocktailsApp.Domain.SeedWork;

namespace CocktailsApp.Domain.ClubAggregate
{
    public sealed record ClubVisibilityChangedEvent(
        Guid ClubId,
        ClubVisibility Visibility,
        Guid ActorId
    ) : DomainEvent(ClubId, typeof(Club), ActorId);
}
