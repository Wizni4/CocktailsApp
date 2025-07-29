// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using AutoMapper;
using CocktailsApp.API.Models.Club;
using CocktailsApp.Application.Club;
using CocktailsApp.Application.Shared;
using CocktailsApp.Application.User;

namespace CocktailsApp.API.Mappers
{
    public class APIMapperProfile : Profile
    {
        public APIMapperProfile()
        {
            // Cocktail

            // Club
            CreateMap<ClubDTO, ClubResponse>();
            CreateMap<ClubCocktailDTO, ClubCocktailResponse>();
            CreateMap<ClubMemberDTO, ClubMemberResponse>()
                .ForMember(m => m.Permissions, opt => opt.MapFrom(mDTO => mDTO.Permissions.Select(p => p.Permission.ToString())));
            CreateMap<ClubRoleDTO, ClubRoleResponse>()
                .ForMember(r => r.Permissions, opt => opt.MapFrom(rDTO => rDTO.Permissions.Select(p => p.Permission.ToString())));

            // Shared
            CreateMap<Models.Shared.Address, AddressDTO>();
            CreateMap<AddressDTO, Models.Shared.Address>();

            // User
            CreateMap<CocktailsApp.Domain.UserAggregate.User, UserDTO>();
        }
    }
}
