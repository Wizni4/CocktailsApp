using AutoMapper;

using CocktailsApp.API.Common;
using CocktailsApp.Application.Clubs;
using CocktailsApp.Application.Common;
using CocktailsApp.Domain.Clubs;

namespace CocktailsApp.API.Clubs
{
    public class ClubsApiProfile : Profile
    {
        public ClubsApiProfile(IImageUrlProvider images)
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
            CreateMap<ClubDetails, ClubDetailsResponse>()
                .ForMember(dest => dest.ImageUrl, opt => opt.MapFrom(src => images.GetUrl(ImageSubject.Club, src.ImageId, ImageVariant.Small)));
            CreateMap<ClubRoleView, ClubRoleResponse>();
            CreateMap<ClubMemberView, ClubMemberResponse>()
                .ForMember(dest => dest.ImageUrl, opt => opt.MapFrom(src => images.GetUrl(ImageSubject.User, src.ImageId, ImageVariant.Small)));
            CreateMap<ClubCocktailView, ClubCocktailResponse>()
                .ForMember(dest => dest.ImageUrl, opt => opt.MapFrom(src => images.GetUrl(ImageSubject.Cocktail, src.ImageId, ImageVariant.Small)));

            // -- List item
            CreateMap<ClubListItem, ClubListItemResponse>()
                .ForMember(dest => dest.ImageUrl, opt => opt.MapFrom(src => images.GetUrl(ImageSubject.Club, src.ImageId, ImageVariant.Small)));

            // -- Club Menu
            CreateMap<ClubMenu, ClubMenuResponse>();
            CreateMap<MenuCocktailItem, MenuCocktailItemResponse>()
                .ForMember(dest => dest.ImageUrl, opt => opt.MapFrom(src => images.GetUrl(ImageSubject.Club, src.ImageId, ImageVariant.Small)));
        }

    }
}
