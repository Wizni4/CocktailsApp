/*
 * Application namespaces
 */
/*
 * Framework namespaces
 */
using AutoMapper;

using CocktailsApp.Application.Club;
using CocktailsApp.Application.Cocktail;
using CocktailsApp.Application.Shared;
using CocktailsApp.Application.User;

/*
 * Domain namespaces
 */
using CocktailsApp.Domain.CocktailAggregate;
using CocktailsApp.Domain.Shared;

namespace CocktailsApp.Application.SeedWork.Mappers
{
    public class ApplicationMapperProfile : Profile
    {
        public ApplicationMapperProfile()
        {
            // Cocktail
            CreateMap<CocktailsApp.Domain.CocktailAggregate.Cocktail, CocktailDTO>();
            CreateMap<CocktailIngredient, CocktailIngredientDTO>();

            // Club
            CreateMap<CocktailsApp.Domain.ClubAggregate.Club, ClubDTO>();
            CreateMap<CocktailsApp.Domain.ClubAggregate.ClubCocktail, ClubCocktailDTO>();
            CreateMap<CocktailsApp.Domain.ClubAggregate.ClubMember, ClubMemberDTO>();
            CreateMap<CocktailsApp.Domain.ClubAggregate.ClubRole, ClubRoleDTO>();

            // Shared
            CreateMap<Address, AddressDTO>();
            CreateMap<AddressDTO, Address>();
            CreateMap<Ingredient, IngredientDTO>();

            // User
            CreateMap<CocktailsApp.Domain.UserAggregate.User, UserDTO>();
        }
    }
}
