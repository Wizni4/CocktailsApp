// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.
using CocktailsApp.Application.Clubs;
using CocktailsApp.Infrastructure.SeedWork;

namespace CocktailsApp.Infrastructure.ClubAggregate
{
    public class ClubCocktailReader(
        EFReadDbContext dbContext
    ) : EFQueryReader<ClubRead, ClubCocktailDTO>(dbContext), IClubCocktailReader;
}
