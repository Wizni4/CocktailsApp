/*
 * Framework namespaces
 */
using AutoMapper;

using DomainClub = CocktailsApp.Domain.ClubAggregate.Club;
using DomainClubCocktail = CocktailsApp.Domain.ClubAggregate.ClubCocktail;
using DomainClubMember = CocktailsApp.Domain.ClubAggregate.ClubMember;
using DomainClubRole = CocktailsApp.Domain.ClubAggregate.ClubRole;
/*
 * Application namespaces
 */
/*
 * Domain namespaces
 */

namespace CocktailsApp.Application.Club
{
    public class ClubApplicationMapperProfile : Profile
    {
        public ClubApplicationMapperProfile()
        {
            // Domain to DTO
            CreateMap<DomainClub, ClubDTO>();
            CreateMap<DomainClubCocktail, ClubCocktailDTO>();
            CreateMap<DomainClubMember, ClubRoleDTO>();
            CreateMap<DomainClubRole, ClubRoleDTO>()
                .ForMember(dest => dest.Permissions, opt => opt.MapFrom(src => src.Permissions.Select(p => p.Permission)));
        }
    }
}
