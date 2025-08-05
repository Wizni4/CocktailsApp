// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using AutoMapper;

using DomainUser = CocktailsApp.Domain.UserAggregate.User;
using DomainClub = CocktailsApp.Domain.ClubAggregate.Club;
using DomainCocktail = CocktailsApp.Domain.CocktailAggregate.Cocktail;

namespace CocktailsApp.Application.Search
{
    public class SearchApplicationMapperProfile : Profile
    {
        public SearchApplicationMapperProfile()
        {
            CreateMap<DomainClub, SearchResultDTO>()
                .ForMember(s => s.Type, opt => opt.MapFrom(c => c.GetType()));
            CreateMap<DomainCocktail, SearchResultDTO>()
                .ForMember(s => s.Type, opt => opt.MapFrom(c => c.GetType()));
            CreateMap<DomainUser, SearchResultDTO>()
                .ForMember(s => s.Type, opt => opt.MapFrom(c => c.GetType()))
                .ForMember(s => s.Name, opt => opt.MapFrom(c => c.Username));
        }
    }
}
