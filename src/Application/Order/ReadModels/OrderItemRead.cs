// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using CocktailsApp.Application.SeedWork;

namespace CocktailsApp.Application.Order
{
    public class OrderItemRead : ReadEntity
    {
        public Guid CocktailId { get; set; } = default;
        public string Name { get; set; } = default!;
        public string? Descritpion { get; set; } = null;
        public string? ImageId { get; set; } = null;
        public decimal Quantity { get; set; } = default;
    }
}
