// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using CocktailsApp.Application.SeedWork;
using CocktailsApp.Application.User;

using System.Collections.ObjectModel;


namespace CocktailsApp.Application.Order
{
    public sealed class OrderDTO: EntityDTO
    {
        public Guid OrderId { get; set; } = default;
        public Guid ClubId { get; set; } = default;
        public UserDTO Customer { get; set;} = default!;
        public List<OrderItemDTO> Items{ get; set;} = default!;
        public DateTime OrderDate { get; set; } = default;
    }
}
