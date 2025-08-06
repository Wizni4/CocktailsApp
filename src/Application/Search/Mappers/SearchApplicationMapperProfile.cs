// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using AutoMapper;

using DomainClub = CocktailsApp.Domain.ClubAggregate.Club;
using DomainCocktail = CocktailsApp.Domain.CocktailAggregate.Cocktail;
using DomainIngredient = CocktailsApp.Domain.IngredientAggregate.Ingredient;
using DomainUser = CocktailsApp.Domain.UserAggregate.User;

namespace CocktailsApp.Application.Search
{
    public class SearchApplicationMapperProfile : Profile
    {
        public SearchApplicationMapperProfile()
        {
            CreateMap<DomainClub, GlobalSearchResultDTO>()
                .ForMember(s => s.Type, opt => opt.MapFrom(c => c.GetType()));
            CreateMap<DomainCocktail, GlobalSearchResultDTO>()
                .ForMember(s => s.Type, opt => opt.MapFrom(c => c.GetType()));
            CreateMap<DomainIngredient, GlobalSearchResultDTO>()
                .ForMember(s => s.Type, opt => opt.MapFrom(c => c.GetType()));
            CreateMap<DomainUser, GlobalSearchResultDTO>()
                .ForMember(s => s.Type, opt => opt.MapFrom(c => c.GetType()))
                .ForMember(s => s.Name, opt => opt.MapFrom(c => c.Username));
        }
    }
}
