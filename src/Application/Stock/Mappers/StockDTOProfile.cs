// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using AutoMapper;

using CocktailsApp.Application.Ingredient;


namespace CocktailsApp.Application.Stock
{
    public class StockDTOProfile : Profile
    {
        public StockDTOProfile()
        {
            // -- Stock
            CreateMap<StockRead, StockDTO>()
                .ForMember(dest => dest.Ingredient, opt => opt.MapFrom(src => src));
            CreateMap<StockRead, IngredientDTO>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.IngredientId));

            // -- StockTransaction
            CreateMap<StockTransactionRead, StockTransactionDTO>();
        }
    }
}
