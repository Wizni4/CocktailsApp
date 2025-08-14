// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using CocktailsApp.Application.Cocktails;
using CocktailsApp.Infrastructure.SeedWork;


namespace CocktailsApp.Infrastructure.CocktailAggregate
{
    public class CocktailReader(EFReadDbContext dbContext)
        : EFQueryReader<CocktailRead, CocktailDTO>(dbContext), ICocktailReader;
}
