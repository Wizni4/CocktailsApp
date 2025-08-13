// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using AutoMapper;

using CocktailsApp.API.Ingredient;
using CocktailsApp.API.SeedWork;
using CocktailsApp.Application.Cocktail;
using CocktailsApp.Domain.Shared;


namespace CocktailsApp.API.Cocktail
{
    public class CocktailResponseProfile : Profile
    {
        public CocktailResponseProfile()
        {
            CreateMap<IngredientModelRequest, IngredientModel>()
                .ForMember(dest => dest.Unit, opt => opt.MapFrom(src => src.Unit.ToEnum<UnitOfMeasure>()));
            CreateMap<CocktailDTO, CocktailResponse>();
            CreateMap<CocktailIngredientDTO, CocktailIngredientResponse>()
                .ForMember(dest => dest.Unit, opt => opt.MapFrom(src => src.Unit.ToString()));
        }
    }
}
