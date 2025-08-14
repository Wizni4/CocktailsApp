using CocktailsApp.Application.Common;
using CocktailsApp.Domain.Clubs;

namespace CocktailsApp.Application.Clubs
{
    public sealed class LoadClubWithMembersCommandSpecification(Guid clubId) : ClubCommandSpecification(clubId)
    {
        public override IEnumerable<ILoad<Club>> Graph => [.. base.Graph, ClubLoadGraphs.Members];
    }
}
