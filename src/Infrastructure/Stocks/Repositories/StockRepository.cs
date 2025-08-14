// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using CocktailsApp.Application.Stocks;
using CocktailsApp.Domain.Stocks;
using CocktailsApp.Infrastructure.Common;
using CocktailsApp.Infrastructure.Persistence;


namespace CocktailsApp.Infrastructure.Stocks
{
    public class StockRepository(
        EFWriteDbContext dbContext,
        IIncludesService<Stock> includesService
    ) : EFRepository<Stock>(dbContext, includesService), IStockRepository;
}
