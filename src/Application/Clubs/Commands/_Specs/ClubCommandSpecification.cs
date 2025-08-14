using CocktailsApp.Application.Common;
using CocktailsApp.Domain.Clubs;
using CocktailsApp.Domain.Common;

namespace CocktailsApp.Application.Clubs
{
    public abstract class ClubCommandSpecification(Guid clubId) : ICommandSpecification<Club>
    {
        private readonly Guid _clubId = clubId;
        public virtual ISpecification<Club>? Specification => new ClubByIdSpecification(_clubId);
        public virtual IEnumerable<ILoad<Club>> Graph => [ClubLoadGraphs.MembersWithRoles, ClubLoadGraphs.Roles];
    }
}
