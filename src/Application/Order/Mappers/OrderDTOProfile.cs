// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using AutoMapper;

using CocktailsApp.Application.Cocktail;
using CocktailsApp.Application.User;

namespace CocktailsApp.Application.Order
{
    public class OrderDTOProfile : Profile
    {
        public OrderDTOProfile()
        {
            // -- Order
            CreateMap<OrderRead, OrderDTO>()
                .ForMember(dest => dest.Customer, opt => opt.MapFrom(src => src));
            CreateMap<OrderRead, UserDTO>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.UserId));

            // -- OrderItem
            CreateMap<OrderItemRead, OrderItemDTO>()
                .ForMember(dest => dest.Cocktail, opt => opt.MapFrom(src => src));
            CreateMap<OrderItemRead, CocktailDTO>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.CocktailId));
        }
    }
}
