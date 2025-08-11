/*
 * Framework namespaces
 */
using AutoMapper;

using CocktailsApp.Application.SeedWork;
using CocktailsApp.Domain.ClubAggregate;

using DomainUser = CocktailsApp.Domain.UserAggregate.User;
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
    public class ClubApplicationProfile : Profile
    {
        public ClubApplicationProfile()
        {
            // Domain to DTO
            CreateMap<DomainClub, ClubDTO>();
            CreateMap<DomainClubCocktail, ClubCocktailDTO>();
            CreateMap<DomainClubMember, ClubMemberDTO>();
            CreateMap<Pair<ClubMember, DomainUser>, ClubMemberDTO>()
                .IncludeMembers(p => p.Left)
                .ForMember(d => d.User, o => o.MapFrom(p => p.Right));
            CreateMap<DomainClubRole, ClubRoleDTO>()
                .ForMember(dest => dest.Permissions, opt => opt.MapFrom(src => src.Permissions.Select(p => p.Permission)));
        }
    }
}
