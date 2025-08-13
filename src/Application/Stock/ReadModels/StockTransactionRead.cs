// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using CocktailsApp.Application.SeedWork;


namespace CocktailsApp.Application.Stock
{
    public class StockTransactionRead : ReadEntity
    {
        public Guid Id { get; set; } = default;
        public DateTime Date { get; set; } = default;
        public string Description { get; set; } = default!;
        public decimal Quantity { get; set; } = default;
        public string TransactionType { get; set; } = default!;
    }
}
