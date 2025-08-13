// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using AutoMapper;

using CocktailsApp.Application.Ingredient;

namespace CocktailsApp.Application.Cocktail
{
    public class CocktailDTOProfile : Profile
    {
        public CocktailDTOProfile()
        {
            // -- Cocktail
            CreateMap<CocktailRead, CocktailDTO>();

            // -- CocktailIngredient
            CreateMap<CocktailIngredientRead, CocktailIngredientDTO>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.CocktailIngredientId))
                .ForMember(dest => dest.Ingredient, opt => opt.MapFrom(src => src));
            CreateMap<CocktailIngredientRead, IngredientDTO>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.IngredientId))
                .ForMember(dest => dest.Allergens, opt => opt.MapFrom(src => (src.Allergens ?? new List<string>())
                                                                            .Select(a => new AllergenDTO() { Name=a})
                                                                            .ToList()
                                                                            .AsReadOnly()));
        }
    }
}
