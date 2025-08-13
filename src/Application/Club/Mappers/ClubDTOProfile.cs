/*
 * Framework namespaces
 */
using AutoMapper;

using CocktailsApp.Application.Cocktail;
using CocktailsApp.Application.Ingredient;
using CocktailsApp.Application.Shared;
using CocktailsApp.Application.User;

/*
 * Application namespaces
 */
/*
 * Domain namespaces
 */

namespace CocktailsApp.Application.Club
{
    public class ClubDTOProfile : Profile
    {
        public ClubDTOProfile()
        {
            // Read model to DTO
            // -- Club
            CreateMap<ClubRead, ClubDTO>();

            // -- Address
            CreateMap<ClubAddressRead, AddressDTO>();

            // -- Cocktail
            CreateMap<ClubCocktailRead, ClubCocktailDTO>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.ClubCocktailId))
                .ForMember(dest => dest.Cocktail, opt => opt.MapFrom(src => src));
            CreateMap<ClubCocktailRead, CocktailDTO>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.CocktailId));
            CreateMap<ClubCocktailIngredientRead, CocktailIngredientDTO>()
                .ForMember(dest => dest.Ingredient, opt => opt.MapFrom(src => src));
            CreateMap<ClubCocktailIngredientRead, IngredientDTO>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.IngredientId))
                .ForMember(dest => dest.Allergens, opt => opt.Ignore());

            // -- Members
            CreateMap<ClubMemberRead, ClubMemberDTO>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.ClubMemberId))
                .ForMember(dest => dest.User, opt => opt.MapFrom(src => src));
            CreateMap<ClubMemberRead, UserDTO>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.UserId));

            // -- Roles
            CreateMap<ClubRoleRead, ClubRoleDTO>();
        }
    }
}
