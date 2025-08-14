// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.


using AutoMapper;

using CocktailsApp.Application.Clubs;
using CocktailsApp.Domain.Clubs;
using CocktailsApp.Domain.Shared;

namespace CocktailsApp.Infrastructure.ClubAggregate
{
    public class ClubInfraProfile : Profile
    {
        public ClubInfraProfile()
        {
            CreateMap<ClubCreatedEvent, ClubRead>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.ClubId));
            CreateMap<Address, ClubAddressRead>();
            CreateMap<ClubMemberAddedEvent, ClubMemberRead>();
            CreateMap<ClubRoleCreatedEvent, ClubRoleRead>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.RoleId));
        }
    }
}
