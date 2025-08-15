using AutoMapper;

using CocktailsApp.API.Common;
using CocktailsApp.Application.Clubs;
using CocktailsApp.Application.Common;
using CocktailsApp.Domain.Clubs;

namespace CocktailsApp.API.Clubs
{
    public class ClubsApiProfile : Profile
    {
        public ClubsApiProfile()
        {
            // Requests
            // -- Address
            CreateMap<Application.Common.Address, AddressRequest>();

            // -- Members
            CreateMap<MemberRolesUpdateRequest, MemberRolesUpdateModel>();

            // -- Roles
            CreateMap<CreateRoleRequest, CreateRoleModel>()
                .ForMember(dest => dest.Permissions, opt => opt.MapFrom(src => src.Permissions == null ? null : src.Permissions.Select(p => p.ToEnum<PermissionType>())));
            CreateMap<UpdateRoleRequest, UpdateRoleModel>()
                .ForMember(dest => dest.Permissions, opt => opt.MapFrom(src => src.Permissions == null ? null : src.Permissions.Select(p => p.ToEnum<PermissionType>())));
            CreateMap<RolePermissionsUpdateRequest, RolePermissionsUpdateModel>()
                .ForMember(dest => dest.Permissions, opt => opt.MapFrom(src => src.Permissions == null ? null : src.Permissions.Select(p => p.ToEnum<PermissionType>())));


            // Responses
            // -- Details
            CreateMap<ClubDetails, ClubDetailsResponse>();
            CreateMap<ClubRoleView, ClubRoleResponse>();
            CreateMap<ClubMemberView, ClubMemberResponse>();
            CreateMap<ClubCocktailView, ClubCocktailResponse>();

            // -- List item
            CreateMap<ClubListItem, ClubListItemResponse>();

            // -- Club Menu
            CreateMap<ClubMenu, ClubMenuResponse>();
            CreateMap<MenuCocktailItem, MenuCocktailItemResponse>();
        }

    }
}
