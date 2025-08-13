

using System.Collections.ObjectModel;

namespace CocktailsApp.Application.Club
{
    public record GetClubCocktailsByIdsQuery(
        Guid ClubId,
        IEnumerable<Guid> CocktailIds
    ) : ClubQuery<ReadOnlyCollection<ClubCocktailDTO>>(ClubId);
}
