
using AutoMapper;

using CocktailsApp.Application.Auth;

namespace CocktailsApp.API.Identity
{
    public class IdentityApiProfile : Profile
    {
        public IdentityApiProfile()
        {
            // Response
            CreateMap<AuthTokens, SignInResponse>();
            CreateMap<AuthTokens, SignInResponse>();
        }
    }
}
