/*
 * Application namespaces
 */
/*
 * Framework namespaces
 */
using AutoMapper;

using DomainCocktail = CocktailsApp.Domain.CocktailAggregate.Cocktail;
using DomainUser = CocktailsApp.Domain.UserAggregate.User;
using CocktailsApp.Application.Cocktail;
using CocktailsApp.Application.Shared;
using CocktailsApp.Application.User;

/*
 * Domain namespaces
 */
using CocktailsApp.Domain.CocktailAggregate;
using CocktailsApp.Domain.Shared;

namespace CocktailsApp.Application.SeedWork
{
    public class ApplicationMapperProfile : Profile
    {
        public ApplicationMapperProfile()
        {
            // Cocktail
            CreateMap<DomainCocktail, CocktailDTO>();
            CreateMap<CocktailIngredient, CocktailIngredientDTO>();

            // Shared
            CreateMap<Address, AddressDTO>();
            CreateMap<AddressDTO, Address>();
            CreateMap<Ingredient, IngredientDTO>();

            // User
            CreateMap<DomainUser, UserDTO>();
        }
    }
}
