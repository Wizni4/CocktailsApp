// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using API.Models.Club.Responses;

using AutoMapper;

using CocktailsApp.Application.Club;
using CocktailsApp.Application.Cocktail;
using CocktailsApp.Application.Shared;
using CocktailsApp.Application.User;
using CocktailsApp.Domain.CocktailAggregate;
using CocktailsApp.Domain.Shared;

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
            CreateMap<ClubMemberDTO, ClubMemberResponse>();
            CreateMap<ClubRoleDTO, ClubRoleResponse>();

            // Shared
            CreateMap<AddressDTO, AddressResponse>();

            // User
            CreateMap<CocktailsApp.Domain.UserAggregate.User, UserDTO>();
        }
    }
}
