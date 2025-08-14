// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using CocktailsApp.Domain.Common;


namespace CocktailsApp.Domain.StockAggregate
{
    public sealed record TransactionAddedEvent(
        Guid StockId,
        decimal Quantity,
        string Description,
        StockTransactionType TransactionType,
        Guid ActorId
    ) : DomainEvent(StockId, typeof(Stock), ActorId);
}
