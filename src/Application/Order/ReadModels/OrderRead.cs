// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using CocktailsApp.Application.SeedWork;


namespace CocktailsApp.Application.Order
{
    public class OrderRead : ReadEntity
    {
        public Guid OrderId { get; set; } = default;
        public Guid ClubId { get; set; } = default;
        public Guid UserId { get; set; } = default;
        public string Username { get; set; } = default!;
        public List<OrderItemRead> Items { get; set; } = [];
        public DateTime OrderDate { get; set; } = default;
    }
}
