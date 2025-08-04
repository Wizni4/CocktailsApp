// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using AutoMapper;
using CocktailsApp.API.Extensions;
using CocktailsApp.Application.Club;
using CocktailsApp.Domain.ClubAggregate;

namespace CocktailsApp.API.Club
{
    public class ClubAPIMapperProfile : Profile
    {
        public ClubAPIMapperProfile()
        {
            // Request
            CreateMap<AddMemberRequest, AddMemberModel>();
            CreateMap<CreateRoleRequest, CreateRoleModel>()
                .ForMember(dest => dest.Permissions, 
                opt => opt.MapFrom(src => (src.Permissions ?? Enumerable.Empty<string>())
                                            .Select(p => p.ToEnum<ClubPermissionType>()).AsEnumerable()));
            CreateMap<MemberRolesUpdateRequest, MemberRolesUpdateModel>();
                
            CreateMap<RolePermissionsUpdateRequest, RolePermissionsUpdateModel>()
                .ForMember(dest => dest.Permissions,
                opt => opt.MapFrom(src => src.Permissions.Select(p => p.ToEnum<ClubPermissionType>())));
            CreateMap<UpdateRoleRequest, UpdateRoleModel>()
                .ForMember(dest => dest.Permissions,
                opt => opt.MapFrom(src => (src.Permissions ?? Enumerable.Empty<string>())
                                            .Select(p => p.ToEnum<ClubPermissionType>()).AsEnumerable()));

            // Response
            CreateMap<ClubDTO, ClubResponse>();
            CreateMap<ClubCocktailDTO, ClubCocktailResponse>();
            CreateMap<ClubMemberDTO, ClubMemberResponse>();
            CreateMap<ClubRoleDTO, ClubRoleResponse>()
                .ForMember(dest => dest.Permissions, opt => opt.MapFrom(src => src.Permissions.Select(p => p.ToString())));
        }
    }
}
