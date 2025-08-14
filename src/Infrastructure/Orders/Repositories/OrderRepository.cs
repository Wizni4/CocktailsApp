// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using CocktailsApp.Application.Orders;
using CocktailsApp.Domain.Orders;
using CocktailsApp.Infrastructure.Common;
using CocktailsApp.Infrastructure.Persistence;


namespace CocktailsApp.Infrastructure.Orders
{
    public class OrderRepository(
        EFWriteDbContext dbContext,
        IIncludesService<Order> includesService
    ) : EFRepository<Order>(dbContext, includesService), IOrderRepository;
}
