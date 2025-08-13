// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using CocktailsApp.Application.Cocktail;
using CocktailsApp.Application.SeedWork;


namespace CocktailsApp.Application.Order
{
    public sealed class OrderItemDTO : EntityDTO
    {
        public CocktailDTO Cocktail { get; set; } = default!;
        public decimal Quantity { get; set; } = default!;
    }
}
