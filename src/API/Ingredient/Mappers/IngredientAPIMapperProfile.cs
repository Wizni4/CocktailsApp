// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using AutoMapper;

using CocktailsApp.Application.Ingredient;

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CocktailsApp.API.Ingredient
{
    public class IngredientAPIMapperProfile : Profile
    {
        public IngredientAPIMapperProfile()
        {
            CreateMap<IngredientDTO, IngredientResponse>()
                .ForMember(dest => dest.Type, opt => opt.MapFrom(src => src.Type.ToString()))
                .ForMember(dest => dest.Allergens, opt => opt.MapFrom(src => src.Allergens.Select(a => a.Name)));
        }
    }
}
