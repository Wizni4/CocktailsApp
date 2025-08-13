// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using AutoMapper;

using CocktailsApp.Application.Ingredient;


namespace CocktailsApp.Application.IngredientPricing
{
    public class IngredientPricingDTOProfile : Profile
    {
        public IngredientPricingDTOProfile()
        {
            CreateMap<IngredientPricingRead, IngredientPricingDTO>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.IngredientPricingId))
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src));
            CreateMap<IngredientPricingRead, IngredientDTO>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.IngredientId));
        }
    }
}
