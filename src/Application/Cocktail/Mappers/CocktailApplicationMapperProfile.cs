// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using AutoMapper;

using DomainCocktail = CocktailsApp.Domain.CocktailAggregate.Cocktail;
using DomainCocktailIngredient = CocktailsApp.Domain.CocktailAggregate.CocktailIngredient;

namespace CocktailsApp.Application.Cocktail
{
    public class CocktailApplicationMapperProfile : Profile
    {
        public CocktailApplicationMapperProfile()
        {
            CreateMap<DomainCocktail, CocktailDTO>();
            CreateMap<DomainCocktailIngredient, CocktailIngredientDTO>()
                .ForMember(dest => dest.Ingredient, opt => opt.MapFrom<IngredientResponseResolver>());
        }
    }
}
