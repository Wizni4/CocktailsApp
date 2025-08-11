// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using DomainClub = CocktailsApp.Domain.ClubAggregate.Club;
using CocktailsApp.Application.SeedWork;
using CocktailsApp.Domain.ClubAggregate;


namespace CocktailsApp.Application.Club
{
    public interface IClubRoleQueries : IChildQueryRepository<DomainClub, ClubRole, ClubRoleDTO>
    {
    }
}
